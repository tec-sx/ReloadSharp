namespace Reload.Input
{
    using Reload.Core.Commands;
    using Silk.NET.Input.Common;
    using System;
    using System.Collections.Generic;

    public class InputContext
    {
        public Guid Uid { get; set; }
        internal Dictionary<(int, Key), Command> KeyCommands { get; private set; }
        internal Dictionary<(int, MouseButton), Command> MouseButtonCommands { get; private set; }
        internal Dictionary<ScrollWheel, Command> MouseScrollCommands { get; private set; }

        public InputContext()
        {
            KeyCommands = new Dictionary<(int, Key), Command>(16);
            MouseButtonCommands = new Dictionary<(int, MouseButton), Command>(16);
            MouseScrollCommands = new Dictionary<ScrollWheel, Command>(2);
        }

        public void MapKeyToCommand(int keyboardId, Key key, Command command)
        {
            KeyCommands.Add((keyboardId, key), command);
        }

        public void MapMouseButtonToCommand(int mouseId, MouseButton button, Command command)
        {
            MouseButtonCommands.Add((mouseId, button), command);
        }

        public void MapMouseScrollToCommand(ScrollWheel scroll, Command command)
        {
            MouseScrollCommands.Add(scroll, command);
        }
    }
}
