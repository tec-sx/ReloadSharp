namespace ReloadGame.Scenes.Commands
{
    using Reload.Core;
    using Reload.Core.Commands;
    using Reload.Scene;
    using Reload.Scene.Enumerations;

    public class JumpCommand : ActionCommand<Actor>
    {
        public JumpCommand()
        { }

        public override void Execute(Actor actor) => actor.Jump();
    }

    public class WalkCommand : StateCommand<Actor>
    {
        public WalkCommand()
        {
        }

        public override void Execute(Actor actor, bool state) => actor.Walk(state);
    }

    public class RunCommand : StateCommand<Actor>
    {
        public RunCommand()
        {
        }

        public override void Execute(Actor actor, bool state) => actor.Run(state);
    }

    public class OpenMenuCommand : ActionCommand<SceneBase>
    {
        public override void Execute(SceneBase scene) => scene.ChangeSceneState(SceneState.ChangePrev);
    }
}
