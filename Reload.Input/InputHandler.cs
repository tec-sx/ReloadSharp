namespace Reload.Input
{
    using Reload.Core.Commands;
    using Silk.NET.Input.Common;
    using System;
    using System.Collections.Generic;

    public class InputHandler
    {
        public event Action<Command> FireActionCommand;

        public event Action<Command, bool> FireStateCommand;

        public event Action<Command, int> FireRangeCommand;

        public event Action<char> FireTextInput;

        private Dictionary<string, InputMappingContext> _bindingContexts;

        private readonly Stack<InputMappingContext> _activeBindingContexts;

        public InputHandler()
        {
            _bindingContexts = new Dictionary<string, InputMappingContext>(16);
            _activeBindingContexts = new Stack<InputMappingContext>(4);
        }

        public void Initialize(IReadOnlyList<IKeyboard> keyboards, IReadOnlyList<IMouse> mice)
        {
            foreach (var keyboard in keyboards)
            {
                keyboard.KeyDown += HandleKeyDown;
                keyboard.KeyUp += HandleKeyUp;
            }
        }

        public void LoadContexts(Dictionary<string, InputMappingContext> contexts) => _bindingContexts = contexts;

        public void Update()
        {

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

        private void HandleKeyDown(IKeyboard keyboard, Key key, int arg)
        {
            if (_activeBindingContexts.Count == 0 || !_activeBindingContexts.Peek().KeyCommands.TryGetValue((keyboard.Index, key), out var command))
            {
                return;
            }

            switch (command.Type)
            {
                case InputType.ActionPress:
                    FireActionCommand?.Invoke(command);
                    break;
                case InputType.State:
                    FireStateCommand?.Invoke(command, true);
                    break;
                case InputType.Range:
                    FireRangeCommand?.Invoke(command, 1);
                    break;
                default:
                    return;
            };
        }

        private void HandleKeyUp(IKeyboard keyboard, Key key, int arg)
        {
            if (_activeBindingContexts.Count == 0 || !_activeBindingContexts.Peek().KeyCommands.TryGetValue((keyboard.Index, key), out var command))
            {
                return;
            }

            switch (command.Type)
            {
                case InputType.ActionRelease:
                    FireActionCommand?.Invoke(command);
                    break;
                case InputType.State:
                    FireStateCommand?.Invoke(command, false);
                    break;
                case InputType.Range:
                    FireRangeCommand?.Invoke(command, 0);
                    break;
                default:
                    return;
            };
        }

        public void HandleTextInput(IKeyboard keyboard, char character)
        {
            FireTextInput?.Invoke(character);
        }

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
