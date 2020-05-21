namespace ReloadGame.Scenes.Commands
{
    using Reload.Core;
    using Reload.Core.Commands;

    public class JumpCommand : ActionCommand
    {
        public JumpCommand()
        { }

        public override void Execute(Actor actor) => actor.Jump();
    }

    public class WalkCommand : StateCommand
    {
        public WalkCommand()
        {
        }

        public override void Execute(Actor actor, bool state) => actor.Walk(state);
    }

    public class RunCommand : StateCommand
    {
        public RunCommand()
        {
        }

        public override void Execute(Actor actor, bool state) => actor.Run(state);
    }
}
