using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Reload.Editor.Scenes;
using Reload.Platform.Graphics.OpenGl;
using SpaceVIL;
using SpaceVIL.Core;

namespace Reload.Editor
{
    public class OpenGlLayer : Prototype, IOpenGLLayer
    {
        private const double FramesPerSecond = 60.0f;
        
        private GlContext _glContext;
        private readonly GameEditor _gameEditor;
        private readonly Stopwatch _renderStopwatch;
        private readonly Stopwatch _updateStopwatch;
        private readonly Stopwatch _lifetimeStopwatch;
        private readonly double _renderPeriod;
        private readonly double _updatePeriod;

        private MainViewport _mainViewport;
        
        private bool _isInitialized;
        
        public OpenGlLayer()
        {
            _gameEditor = new GameEditor(new string[] { });
            _renderStopwatch = new Stopwatch();
            _updateStopwatch = new Stopwatch();
            _lifetimeStopwatch = new Stopwatch();
            _renderPeriod = 1 / FramesPerSecond;
            _updatePeriod = 1 / FramesPerSecond;
        }
        
        public void Initialize()
        {
            var spaceAss = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "Lib/SpaceVIL.dll"));
            var getProcMethod = spaceAss
                .GetType("A.b")?
                .GetMethod(
                "B", 
                BindingFlags.NonPublic | BindingFlags.Static,
                null,
                new [] {typeof(string)},
                null);

            if (getProcMethod == null)
            {
                throw new ApplicationException("Can't access Glfw getProcAddress method.");
            }
            
            var getProcAddress = 
                (Func<string, IntPtr>) Delegate.CreateDelegate(typeof(Func<string, IntPtr>), getProcMethod);
            
            _glContext = new GlContext(getProcAddress);
            
            SetBackground(0, 75, 75);
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            
            _gameEditor.Initialize(GetWidth(), GetHeight());
            _isInitialized = true;
            
            _mainViewport = _gameEditor.SceneMachine.ActiveScene as MainViewport;

            _renderStopwatch.Start();
            _updateStopwatch.Start();
            _lifetimeStopwatch.Start();
        }

        public bool IsInitialized() => _isInitialized;

        public void OnResize(System.Drawing.Size size) => _mainViewport.OnResize(size);
        public void Draw()
        {
            var updateDelta = _updateStopwatch.Elapsed.TotalSeconds;
            if (updateDelta > _updatePeriod)
            {
                _gameEditor.Update(updateDelta);
                _updateStopwatch.Restart();
            }
            
            var renderDelta = _renderStopwatch.Elapsed.TotalSeconds;
            if (renderDelta >= _renderPeriod || WindowManager.GetVSyncValue() > 0)
            {
                _gameEditor.Render(renderDelta);
                _renderStopwatch.Restart();
            }
        }

        public void Free()
        {
            _gameEditor.ShutDown();
        }

        public void Dispose()
        {
            _gameEditor.Dispose();
        }
    }
}