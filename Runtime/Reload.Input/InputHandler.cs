namespace Reload.Input
{
    using Reload.Core.Commands;
    using Silk.NET.Input.Common;
    using System.Collections.Generic;

    public class InputHandler
    {
        private Dictionary<string, InputMappingContext> _bindingContexts;

        private readonly Stack<InputMappingContext> _activeBindingContexts;

        private Queue<Command> _commandQueue;


        public InputHandler()
        {
            _bindingContexts = new Dictionary<string, InputMappingContext>(16);
            _activeBindingContexts = new Stack<InputMappingContext>(4);
            _commandQueue = new Queue<Command>(8);
        }

        public void Attach(IInputContext context)
        {
            foreach (var keyboard in context.Keyboards)
            {
                keyboard.KeyDown += HandleKeyDown;
                keyboard.KeyUp += HandleKeyUp;
            }
        }

        public void Detach(IInputContext context)
        {
            foreach (var keyboard in context.Keyboards)
            {
                keyboard.KeyChar -= HandleTextInput;
                keyboard.KeyDown -= HandleKeyDown;
                keyboard.KeyUp -= HandleKeyUp;
            }
        }

        public void LoadContexts(Dictionary<string, InputMappingContext> contexts) => _bindingContexts = contexts;
        public void ClearContexts()
        {
            _commandQueue.Clear();
            _activeBindingContexts.Clear();
            _bindingContexts.Clear();
        }

        public void Update()
        {
            Queue<Command> pressedCommand = new Queue<Command>(8);

            while (_commandQueue.Count != 0)
            {
                var command = _commandQueue.Dequeue();
                command.Execute();

                if (command is StateCommand && (command as StateCommand).CurrentState == StateType.Pressed)
                {
                    pressedCommand.Enqueue(command);
                }
            }

            _commandQueue = pressedCommand;
        }

        public void PushActiveContext(string name)
        {
            if (_bindingContexts.TryGetValue(name, out var context))
            {
                _activeBindingContexts.Push(context);
            }
        }

        public void PopActiveContext()
        {
            _activeBindingContexts.Pop();
        }

        private void HandleKeyDown(IKeyboard keyboard, Key key, int _)
        {
            if (_activeBindingContexts.Count == 0)
            {
                return;
            }

            var context = _activeBindingContexts.Peek();

            if (context.KeyActionPressCommands.TryGetValue((keyboard.Index, key), out var actionCommand))
            {
                _commandQueue.Enqueue(actionCommand);
                return;
            }

            if (context.KeyStateCommands.TryGetValue((keyboard.Index, key), out var stateCommand))
            {
                stateCommand.CurrentState = StateType.Pressed;
                _commandQueue.Enqueue(stateCommand);
                return;
            }
        }

        private void HandleKeyUp(IKeyboard keyboard, Key key, int arg)
        {
            if (_activeBindingContexts.Count == 0)
            {
                return;
            }

            var context = _activeBindingContexts.Peek();

            if (context.KeyActionReleaseCommands.TryGetValue((keyboard.Index, key), out var actionCommand))
            {
                _commandQueue.Enqueue(actionCommand);
                return;
            }

            if (context.KeyStateCommands.TryGetValue((keyboard.Index, key), out var stateCommand))
            {
                stateCommand.CurrentState = StateType.Released;
                _commandQueue.Enqueue(stateCommand);
                return;
            }
        }

        public void HandleTextInput(IKeyboard keyboard, char character)
        { }

        public void EnableTextInput(IKeyboard keyboard)
        {
            keyboard.KeyDown -= HandleKeyDown;
            keyboard.KeyUp -= HandleKeyUp;
            keyboard.KeyChar += HandleTextInput;
        }

        public void DisableTextInput(IKeyboard keyboard)
        {
            keyboard.KeyChar -= HandleTextInput;
            keyboard.KeyDown += HandleKeyDown;
            keyboard.KeyUp += HandleKeyUp;
        }
    }
}
