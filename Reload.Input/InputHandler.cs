﻿namespace Reload.Input
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

        private Dictionary<string, InputContext> _inputContexts;

        private readonly Stack<InputContext> _activeContexts;

        public InputHandler()
        {
            _inputContexts = new Dictionary<string, InputContext>(16);
            _activeContexts = new Stack<InputContext>(4);
        }

        public void Initialize(IReadOnlyList<IKeyboard> keyboards, IReadOnlyList<IMouse> mice)
        {
            foreach (var keyboard in keyboards)
            {
                keyboard.KeyDown += HandleKeyDown;
                keyboard.KeyUp += HandleKeyUp;
            }
        }

        public void LoadContexts(Dictionary<string, InputContext> contexts) => _inputContexts = contexts;

        public void Update()
        {
        }

        public void PushActiveContext(string name)
        {
            if (_inputContexts.TryGetValue(name, out var context))
            {
                _activeContexts.Push(context);
            }
        }

        public void PopActiveContext()
        {
            _activeContexts.Pop();
        }

        private void HandleKeyDown(IKeyboard keyboard, Key key, int arg)
        {
            if (_activeContexts.Peek().KeyCommands.TryGetValue((keyboard.Index, key), out var command))
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
            if (!_activeContexts.Peek().KeyCommands.TryGetValue((keyboard.Index, key), out var command))
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
