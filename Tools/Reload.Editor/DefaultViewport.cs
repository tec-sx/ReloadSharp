using System.Diagnostics;
using Reload.Editor.Scenes;
using Reload.Rendering;
using Reload.Scenes;
using SpaceVIL;
using SpaceVIL.Core;
using System.Collections.Generic;
using Reload.Core.Commands;
using Reload.Editor.Platform;
using Reload.Editor.Input;
using Reload.Editor.Extensions;
using Reload.Rendering.Camera;
using Reload.Core.Utils;
using System.Numerics;
using System;

namespace Reload.Editor
{
    internal class DefaultViewport : Viewport
    {
        private const double FramesPerSecond = 60.0f;

        private readonly OpenGl _openGl;
        private readonly SceneMachine _sceneMachine;
        private readonly InputManager _inputManager;

        private readonly Stopwatch _renderStopwatch;
        private readonly Stopwatch _updateStopwatch;
        private readonly Stopwatch _lifetimeStopwatch;
        private readonly double _renderPeriod;
        private readonly double _updatePeriod;

        private Queue<ActionPressCommand> _actionPressCommandQueue;
        private Queue<ActionReleaseCommand> _actionReleaseCommandQueue;
        private List<StateCommand> _stateCommandList;
        private Queue<RangeCommand> _rangeCommandQueue;

        private bool _isInitialized;

        public Scene ActiveScene { get; set; }
        
        public DefaultViewport(OpenGl openGl, SceneMachine sceneMachine, InputManager inputManager)
        {
            _openGl = openGl;
            _sceneMachine = sceneMachine;
            _inputManager = inputManager;

            _actionPressCommandQueue = new Queue<ActionPressCommand>(64);
            _actionReleaseCommandQueue = new Queue<ActionReleaseCommand>(64);
            _stateCommandList = new List<StateCommand>(64);
            _rangeCommandQueue = new Queue<RangeCommand>(64);
            _renderStopwatch = new Stopwatch();
            _updateStopwatch = new Stopwatch();
            _lifetimeStopwatch = new Stopwatch();

            _renderPeriod = 1 / FramesPerSecond;
            _updatePeriod = 1 / FramesPerSecond;
        }

        public override void InitElements()
        {
            base.InitElements();
            SetupViewportControls();
        }

        public override void Initialize()
        {
            _openGl.Initialize(this);
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);

            _renderStopwatch.Start();
            _updateStopwatch.Start();
            _lifetimeStopwatch.Start();

            _sceneMachine.AddSceneInViewport<DefaultScene>(this);
            _sceneMachine.Run();

            ActiveScene = _sceneMachine.ActiveScene;

            _isInitialized = true;
        }

        public override bool IsInitialized() => _isInitialized;

        public override void Draw()
        {
            if (GetWidth() == 0 || GetHeight() == 0)
            {
                return;
            }

            double updateDelta = _updateStopwatch.Elapsed.TotalSeconds;
            if (updateDelta > _updatePeriod)
            {
                for (int i = 0; i < _stateCommandList.Count; i++)
                {
                    _stateCommandList[i].Execute(updateDelta);
                }

                while (_actionPressCommandQueue.TryDequeue(out var actioCommand))
                {
                    actioCommand.Execute(updateDelta);
                }

                while (_rangeCommandQueue.TryDequeue(out var rangeCommand))
                {
                    rangeCommand.Execute(updateDelta);
                }

                _sceneMachine.UpdateActiveScene(updateDelta);
                _updateStopwatch.Restart();
            }

            _openGl.GenerateTexturedFrameBufferObject();
            _openGl.PrepareViewport();

            double renderDelta = _renderStopwatch.Elapsed.TotalSeconds;
            if (renderDelta >= _renderPeriod || WindowManager.GetVSyncValue() > 0)
            {
                _sceneMachine.RenderActiveScene(renderDelta);

                _openGl.UnbindFrameBufferObject();
                _openGl.GenerateTextureBuffers();
                _openGl.RenderToScreen();
                _openGl.CleanUpResources();

                _renderStopwatch.Restart();
            }

            Renderer.EndScene();
        }

        public void Resize()
        {
            if (!_isInitialized)
            {
                return;
            }

            //var size = new System.Drawing.Size(GetWidth(), GetHeight());
            
            //if (_activeOrthoCameraController != null)
            //{
            //    _activeOrthoCameraController.OnResize(size);
            //}
            //if (_activePersprctiveCameraController != null)
            //{
            //    var aspectRatio = (size.Width / size.Height);
            //    var perspectiveFov = Matrix4x4.CreatePerspectiveFieldOfView(ReloadMath.DegreesToRadiants(20.0f), aspectRatio, 0.1f, 10000.0f);

            //    _activePersprctiveCameraController.Camera.RecalculateProjectionViewMatrix(perspectiveFov);
            //}
        }

        public override void SetWidth(int width)
        {
            base.SetWidth(width);
            Resize();
        }

        public override void SetHeight(int height)
        {
            base.SetHeight(height);
            Resize();
        }

        public override void Free()
        {
           
        }

        public void Dispose()
        {
        }

        private void SetupViewportControls()
        {
            EventKeyPress += StartTranslation;
            EventKeyRelease += StopTranslation;

            //    EventScrollUp += (sender, args) =>
            //    {
            //        var command = activeScene.;
            //        command.Value = 1;
            //        _rangeCommandQueue.Enqueue(command);
            //    };

            //    EventScrollDown += (sender, args) =>
            //     {
            //         var command = activeScene.CameraController.Zoom;
            //         command.Value = -1;
            //         _rangeCommandQueue.Enqueue(command);
            //     };
        }

        public void StartTranslation(object sender, KeyArgs args)
        {
            var cameraController = _sceneMachine.ActiveScene.CameraController;

            if (args.Key == KeyCode.W)
            {
                cameraController.TranslateForward(true);
            }
            if (args.Key == KeyCode.S)
            {
                cameraController.TranslateBackward(true);
            }
            if (args.Key == KeyCode.Q)
            {
                cameraController.TranslateUp(true);
            }
            if (args.Key == KeyCode.Z)
            {
                cameraController.TranslateDown(true);
            }
            if (args.Key == KeyCode.A)
            {
                cameraController.TranslateLeft(true);
            }
            if (args.Key == KeyCode.D)
            {
                cameraController.TranslateRight(true);
            }
            if (args.Key == KeyCode.LeftBracket)
            {
                cameraController.RollLeft(true);
            }
            if (args.Key == KeyCode.RightBracket)
            {
                cameraController.RollRight(true);
            }
        }

        public void StopTranslation(object sender, KeyArgs args)
        {
            var cameraController = _sceneMachine.ActiveScene.CameraController;
            if (args.Key == KeyCode.W)
            {
                cameraController.TranslateForward(false);
            }
            if (args.Key == KeyCode.S)
            {
                cameraController.TranslateBackward(false);
            }
            if (args.Key == KeyCode.Q)
            {
                cameraController.TranslateUp(false);
            }
            if (args.Key == KeyCode.Z)
            {
                cameraController.TranslateDown(false);
            }
            if (args.Key == KeyCode.A)
            {
                cameraController.TranslateLeft(false);
            }
            if (args.Key == KeyCode.D)
            {
                cameraController.TranslateRight(false);
            }
            if (args.Key == KeyCode.LeftBracket)
            {
                cameraController.RollLeft(false);
            }
            if (args.Key == KeyCode.RightBracket)
            {
                cameraController.RollRight(false);
            }
        }
    }
}