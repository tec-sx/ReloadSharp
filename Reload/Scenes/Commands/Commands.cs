namespace ReloadGame.Scenes.Commands
{
    using Reload.Core;

    public class MoveCommand : Command
    {
        public override CommandType Type => CommandType.State;
        public override void Execute(Actor actor)
        {

        }
    }

    public class JumpCommand : Command
    {
        public override CommandType Type => CommandType.Action;
        public override void Execute(Actor actor) => actor.Jump();
    }
}
