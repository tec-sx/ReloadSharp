namespace ReloadGame.Scenes.Commands
{
    using Reload.Core;
    using Reload.Core.Commands;
    using Reload.Scene;
    using Reload.Scene.Enumerations;

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
        SceneBase scene;
        public OpenMenuCommand(SceneBase target)
        {
            scene = target;
        }

        public override void Execute() => scene.ChangeSceneState(SceneState.ChangePrev);
    }
}
