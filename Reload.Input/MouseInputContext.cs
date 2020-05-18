namespace Reload.Input
{
    using Reload.Core.Commands;
    using Silk.NET.Input.Common;
    using System.Collections.Generic;

    public class MouseInputContext
    {
        private Dictionary<MouseButton, Command> _actionCommands;
        private Dictionary<MouseButton, Command> _stateCommands;
        private Dictionary<ScrollWheel, Command> _rangeCommands;

        public void MapButtonToAction(MouseButton button, Command command)
        {
            _actionCommands.Add(button, command);
        }

        public void MapButtonToState(MouseButton button, Command command)
        {
            _stateCommands.Add(button, command);
        }

        public void MapScrollToRange(ScrollWheel scroll, Command command)
        {
            _rangeCommands.Add(scroll, command);
        }
    }
}
