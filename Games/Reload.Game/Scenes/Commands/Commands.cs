namespace Reload.Game.Scenes.Commands
{
    using Reload.Core.Commands;
    using Reload.Engine.SceneSystem;
    using Reload.Gameplay.Entities;
    using Reload.Engine.SceneSystem.Enumerations;
    using Reload.Rendering.Camera;
    using System.Numerics;

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

    public class MoveCameraUpCommand : StateCommand
    {
        private OrtographicCamera _camera;

        public MoveCameraUpCommand(OrtographicCamera camera)
        {
            _camera = camera;
        }

        public override void Execute()
        {
            if (CurrentState == StateType.Pressed)
            {
                _camera.Position += new Vector3(0.1f, 0.0f, 0.1f);
            }
        }
    }

    public class MoveCameraDownCommand : StateCommand
    {
        private OrtographicCamera _camera;

        public MoveCameraDownCommand(OrtographicCamera camera)
        {
            _camera = camera;
        }

        public override void Execute()
        {
            if (CurrentState == StateType.Pressed)
            {
                _camera.Position -= new Vector3(0.1f, 0.0f, 0.1f);
            }
        }
    }
}
