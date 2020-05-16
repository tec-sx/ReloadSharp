namespace Reload.Input
{
    using Reload.Core;
    using Silk.NET.Input.Common;

    public struct KeyPress
    {
        public Key Key { get; }
        public Command Command { get; }

        public KeyPress(Key key, Command command)
        {
            Key = key;
            Command = command;
        }
    }
}
