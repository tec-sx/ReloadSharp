namespace Reload.Input
{
    using Reload.Core.Commands;
    using Silk.NET.Input.Common;
    using System.Collections.Generic;

    public class KeyboardInputContext
    {
        private Dictionary<Key, Command> _actionCommands;
        private Dictionary<Key, Command> _stateCommands;

        public KeyboardInputContext()
        {
            _actionCommands = new Dictionary<Key, Command>(16);
            _stateCommands = new Dictionary<Key, Command>(16);
        }

        public void MapKeyToAction(Key key, Command command)
        {
            _actionCommands.Add(key, command);
        }

        public void MapKeyToState(Key key, Command command)
        {
            _stateCommands.Add(key, command);
        }
    }
}
