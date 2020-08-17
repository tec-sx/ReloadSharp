using Reload.Core.Commands;
using Silk.NET.Input.Common;
using System.Collections.Generic;

namespace Reload.Input
{
    public class InputHandler
    {
        private Dictionary<string, InputMappingContext> _bindingContexts;

        private readonly Stack<InputMappingContext> _activeBindingContexts;

        private Queue<ActionPressCommand> _actionPressCommandQueue;
        private Queue<ActionReleaseCommand> _actionReleaseCommandQueue;
        private List<StateCommand> _stateCommandList;
        private Queue<RangeCommand> _rangeCommandQueue;

        public InputHandler()
        {
            _bindingContexts = new Dictionary<string, InputMappingContext>(16);
            _activeBindingContexts = new Stack<InputMappingContext>(4);

            _actionPressCommandQueue = new Queue<ActionPressCommand>(64);
            _actionReleaseCommandQueue = new Queue<ActionReleaseCommand>(64);
            _stateCommandList = new List<StateCommand>(64);
            _rangeCommandQueue = new Queue<RangeCommand>(64);
        }

        public void Attach(IInputContext context)
        {
            foreach (var keyboard in context.Keyboards)
            {
                keyboard.KeyDown += HandleKeyDown;
                keyboard.KeyUp += HandleKeyUp;
            }

            foreach (var mouse in context.Mice)
            {
                mouse.Scroll += HandleMouseScroll;
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
            _actionPressCommandQueue.Clear();
            _activeBindingContexts.Clear();
            _bindingContexts.Clear();
        }

        public void Update(double deltaTime)
        {
            for (int i = 0; i < _stateCommandList.Count; i++)
            {
                _stateCommandList[i].Execute(deltaTime);
            }

            while (_actionPressCommandQueue.TryDequeue(out var actioCommand))
            {
                actioCommand.Execute(deltaTime);
            }

            while (_rangeCommandQueue.TryDequeue(out var rangeCommand))
            {
                rangeCommand.Execute(deltaTime);
            }
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

            if (context.KeyActionPressCommands.TryGetValue((keyboard.Index, key), out ActionPressCommand actionCommand))
            {
                _actionPressCommandQueue.Enqueue(actionCommand);
                return;
            }

            if (context.KeyStateCommands.TryGetValue((keyboard.Index, key), out StateCommand stateCommand))
            {
                stateCommand.CurrentState = StateType.Pressed;
                _stateCommandList.Add(stateCommand);
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
                _actionReleaseCommandQueue.Enqueue(actionCommand);
                return;
            }

            if (context.KeyStateCommands.TryGetValue((keyboard.Index, key), out var stateCommand))
            {
                stateCommand.CurrentState = StateType.Released;
                _stateCommandList.Remove(stateCommand);
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

        public void HandleMouseScroll(IMouse mouse, ScrollWheel scroll)
        {
            if (_activeBindingContexts.Count == 0)
            {
                return;
            }

            var context = _activeBindingContexts.Peek();

            if (context.MouseScrollRangeCommands.TryGetValue(mouse.Index, out var rangeCommand))
            {
                rangeCommand.Value = scroll.Y;
                _rangeCommandQueue.Enqueue(rangeCommand);
            }
        }
    }
}
