namespace Reload.Input
{
    using Reload.Core.Commands;
    using System.Collections.Generic;

    public struct MappedInput
    {
        public HashSet<Command> Actions { get; set; }
        public HashSet<Command> States { get; set; }
        public Dictionary<Command, int> Ranges { get; set; }

        public void EatAction(Command command) => Actions.Remove(command);
        public void EatState(Command command) => States.Remove(command);
        public void EatRange(Command command) => Ranges.Remove(command);
    }
}
