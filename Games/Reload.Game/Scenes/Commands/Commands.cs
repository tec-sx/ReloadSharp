﻿namespace Reload.Game.Scenes.Commands
{
    using Reload.Core.Commands;
    using Reload.Engine.SceneSystem;
    using Reload.Gameplay.Entities;
    using Reload.Engine.SceneSystem.Enumerations;
    using Reload.Rendering.Camera;
    using System.Numerics;
    using System;

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
        private Vector3 _position = Vector3.Zero;

        public MoveCameraUpCommand(OrtographicCamera camera)
        {
            _camera = camera;
        }

        public override void Execute()
        {
            _camera.Position.Y -= 0.01f;
        }
    }

    public class MoveCameraDownCommand : StateCommand
    {
        private OrtographicCamera _camera;
        private Vector3 _position = Vector3.Zero;

        public MoveCameraDownCommand(OrtographicCamera camera)
        {
            _camera = camera;
        }

        public override void Execute()
        {
            if (CurrentState == StateType.Pressed)
            {
                _camera.Position.Y += 0.01f;
            }
        }
    }

    public class RotateCameraLeft : StateCommand
    {
        private OrtographicCamera _camera;

        public RotateCameraLeft(OrtographicCamera camera)
        {
            _camera = camera;
        }

        public override void Execute()
        {
            _camera.Rotation -= 0.01f;
        }
    }

    public class RotateCameraRight : StateCommand
    {
        private OrtographicCamera _camera;

        public RotateCameraRight(OrtographicCamera camera)
        {
            _camera = camera;
        }

        public override void Execute()
        {
            if (CurrentState == StateType.Pressed)
            {
                _camera.Rotation += 0.01f;
            }
        }
    }
}
