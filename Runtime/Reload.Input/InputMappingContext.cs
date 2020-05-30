namespace Reload.Input
{
    using Reload.Core.Common.Commands;
    using Silk.NET.Input.Common;
    using System;
    using System.Collections.Generic;

    public class InputMappingContext
    {
        public Guid Uid { get; set; }
        internal Dictionary<(int, Key), ActionPressCommand> KeyActionPressCommands { get; private set; }
        internal Dictionary<(int, Key), ActionReleaseCommand> KeyActionReleaseCommands { get; private set; }
        internal Dictionary<(int, Key), StateCommand> KeyStateCommands { get; private set; }

        internal Dictionary<(int, MouseButton), ActionPressCommand> MouseActionPressCommands { get; private set; }
        internal Dictionary<(int, MouseButton), ActionReleaseCommand> MouseActionReleaseCommands { get; private set; }
        internal Dictionary<(int, MouseButton), StateCommand> MouseStateCommands { get; private set; }

        internal Dictionary<ScrollWheel, ActionPressCommand> MouseScrollActionPressCommands { get; private set; }
        internal Dictionary<ScrollWheel, ActionReleaseCommand> MouseScrollActionReleaseCommands { get; private set; }
        internal Dictionary<ScrollWheel, StateCommand> MouseScrollStateCommands { get; private set; }
        internal Dictionary<ScrollWheel, RangeCommand> MouseScrollRangeCommands { get; private set; }

        public InputMappingContext()
        {
            KeyActionPressCommands = new Dictionary<(int, Key), ActionPressCommand>(16);
            KeyActionReleaseCommands = new Dictionary<(int, Key), ActionReleaseCommand>(16);
            KeyStateCommands = new Dictionary<(int, Key), StateCommand>(16);

            MouseActionPressCommands = new Dictionary<(int, MouseButton), ActionPressCommand>(16);
            MouseActionReleaseCommands = new Dictionary<(int, MouseButton), ActionReleaseCommand>(16);
            MouseStateCommands = new Dictionary<(int, MouseButton), StateCommand>(16);

            MouseScrollActionPressCommands = new Dictionary<ScrollWheel, ActionPressCommand>(2);
            MouseScrollActionReleaseCommands = new Dictionary<ScrollWheel, ActionReleaseCommand>(2);
            MouseScrollStateCommands = new Dictionary<ScrollWheel, StateCommand>(2);
            MouseScrollRangeCommands = new Dictionary<ScrollWheel, RangeCommand>(2);
        }



        public void MapKeyToActionPress(int keyboardId, Key key, ActionPressCommand command)
        {

            KeyActionPressCommands.Add((keyboardId, key), command);
        }

        public void MapKeyToActionRelease(int keyboardId, Key key, ActionReleaseCommand command)
        {

            KeyActionReleaseCommands.Add((keyboardId, key), command);
        }

        public void MapKeyToState(int keyboardId, Key key, StateCommand command)
        {

            KeyStateCommands.Add((keyboardId, key), command);
        }



        public void MapMouseToActionPress(int mouseId, MouseButton button, ActionPressCommand command)
        {
            MouseActionPressCommands.Add((mouseId, button), command);
        }

        public void MapMouseToActionRelease(int mouseId, MouseButton button, ActionReleaseCommand command)
        {
            MouseActionReleaseCommands.Add((mouseId, button), command);
        }

        public void MapMouseState(int mouseId, MouseButton button, StateCommand command)
        {
            MouseStateCommands.Add((mouseId, button), command);
        }



        public void MapMouseScrollToActionPress(ScrollWheel scroll, ActionPressCommand command)
        {
            MouseScrollActionPressCommands.Add(scroll, command);
        }

        public void MapMouseScrollToActionRelease(ScrollWheel scroll, ActionReleaseCommand command)
        {
            MouseScrollActionReleaseCommands.Add(scroll, command);
        }

        public void MapMouseScrollToState(ScrollWheel scroll, StateCommand command)
        {
            MouseScrollStateCommands.Add(scroll, command);
        }

        public void MapMouseScrollToRange(ScrollWheel scroll, RangeCommand command)
        {
            MouseScrollRangeCommands.Add(scroll, command);
        }
    }
}
