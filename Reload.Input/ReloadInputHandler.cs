namespace Reload.Input
{
    using Reload.Core.Commands;
    using Silk.NET.Input.Common;
    using System;
    using System.Collections.Generic;

    public class ReloadInputHandler
    {
        public event Action<Command> FireActionCommand;

        public event Action<Command, bool> FireStateCommand;

        public event Action<Command, int> FireRangeCommand;

        private readonly Dictionary<(int, Key), Command> _keyCommands;

        public ReloadInputHandler()
        {
            _keyCommands = new Dictionary<(int, Key), Command>(16);
        }

        public void Initialize(IReadOnlyList<IKeyboard> keyboards, IReadOnlyList<IMouse> mice)
        {
            foreach (var keyboard in keyboards)
            {
                keyboard.KeyDown += HandleKeyDown;
                keyboard.KeyUp += HandleKeyUp;
            }
        }

        public void Update()
        {
        }

        private void HandleKeyDown(IKeyboard keyboard, Key key, int arg)
        {
            if (!_keyCommands.TryGetValue((keyboard.Index, key), out var command))
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
            if (!_keyCommands.TryGetValue((keyboard.Index, key), out var command))
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

        //public void RegisterKeyCommand(int keyboardIndex, Key key, Command command)
        //{
        //    _keyCommands.Add((keyboardIndex, key), command);
        //}
    }
}
