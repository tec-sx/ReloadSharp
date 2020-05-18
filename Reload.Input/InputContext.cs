namespace Reload.Input
{
    using Reload.Core.Commands;
    using Silk.NET.Input.Common;
    using System;
    using System.Collections.Generic;

    public class InputContext
    {
        public Guid Uid { get; set; }
        internal Dictionary<Key, Command> KeyboardCommands { get; private set; }
        internal Dictionary<MouseButton, Command> MouseButtonCommands { get; private set; }
        internal Dictionary<ScrollWheel, Command> MouseScrollCommands { get; private set; }

        public InputContext()
        {
            KeyboardCommands = new Dictionary<Key, Command>(16);
            MouseButtonCommands = new Dictionary<MouseButton, Command>(16);
            MouseScrollCommands = new Dictionary<ScrollWheel, Command>(2);
        }

        public void MapKeyToCommand(Key key, Command command)
        {
            KeyboardCommands.Add(key, command);
        }

        public void MapMouseButtonToCommand(MouseButton button, Command command)
        {
            MouseButtonCommands.Add(button, command);
        }

        public void MapMouseScrollToCommand(ScrollWheel scroll, Command command)
        {
            MouseScrollCommands.Add(scroll, command);
        }
    }
}
