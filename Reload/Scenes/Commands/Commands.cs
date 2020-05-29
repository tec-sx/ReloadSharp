namespace ReloadGame.Scenes.Commands
{
    using Reload.Core.Common;
    using Reload.Core.Common.Commands;
    using Reload.Game.Scenes;
    using Reload.Game.Scenes.Enumerations;

    public class JumpCommand : ActionPressCommand
    {
        Actor actor;

        public JumpCommand(Actor target)
        {
            actor = target;
        }

        public override void Execute() => actor.Jump();
    }

    public class WalkCommand : StateCommand
    {
        Actor actor;

        public WalkCommand(Actor target)
        {
            actor = target;
        }

        public override void Execute() => actor.Walk(CurrentState);
    }

    public class RunCommand : StateCommand
    {
        Actor actor;

        public RunCommand(Actor target)
        {
            actor = target;
        }

        public override void Execute() => actor.Run(CurrentState);
    }

    public class OpenMenuCommand : ActionPressCommand
    {
        Scene scene;
        public OpenMenuCommand(Scene target)
        {
            scene = target;
        }

        public override void Execute() => scene.ChangeSceneState(SceneState.ChangePrev);
    }
}
