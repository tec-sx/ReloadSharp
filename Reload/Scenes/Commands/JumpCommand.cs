namespace ReloadGame.Scenes.Commands
{
    using Reload.Core;

    public class JumpCommand : Command
    {
        public override void Execute(Actor actor)
        {
            actor.Jump();
        }
    }
}
