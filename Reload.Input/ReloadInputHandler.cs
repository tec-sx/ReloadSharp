using Reload.Core;
using Silk.NET.Input.Common;
using System;
using System.Collections.Generic;

namespace Reload.Input
{
    public class ReloadInputHandler
    {
        public event Action<Command> FireCommand;

        private Dictionary<Key, Command> commands;
        private Dictionary<Key, StateType> stateMap;
        private Dictionary<Key, ActionType> actionMap;

        public ReloadInputHandler()
        {
            commands = new Dictionary<Key, Command>(16);
        }

    }
}
