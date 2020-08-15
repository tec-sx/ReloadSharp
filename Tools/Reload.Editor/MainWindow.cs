using Reload.Editor.Factories;
using SpaceVIL;
using SpaceVIL.Common;
using Reload.Editor.UiElements;
using Reload.Core.Graphics;
using System;

namespace Reload.Editor
{

    /// <summary>
    /// The main program window.
    /// </summary>
    internal class MainWindow : ActiveWindow, IGameWindow
    {
        public DefaultViewport _viewport;

        /// <summary>
        /// Initializes a new instance of the program's <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="viewport">The viewport.</param>
        public MainWindow(DefaultViewport viewport)
        {
            _viewport = viewport;
            EventOnStart += OnStart;
        }

        public int Width { get => GetWidth(); set => SetWidth(value); }
        
        public int Height { get => GetHeight(); set => SetHeight(value); }

        public int PositionX { get => GetX(); set => SetX(value); }

        public int PositionY { get => GetY(); set => SetY(value); }

        public bool IsFullScreen { get; set; }

        public bool IsVsyncOn { get; set; }
        
        public string Name { get; init; }

        public WindowBackendType BackendType => WindowBackendType.Glfw;

        public IntPtr Handle => throw new NotImplementedException();

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Initializes the window.
        /// </summary>
        public override void InitWindow()
        {
            string windowTitle = $"{Properties.Resources.Name} - v.{Properties.Resources.Version}";
            
            int displayWidth = DisplayService.GetDisplayWidth();
            int displayHeight = DisplayService.GetDisplayHeight();
            SetParameters(nameof(MainWindow), windowTitle, (int)(displayWidth * 0.5), (int)(displayHeight * 0.5), true);
            SetMinSize((int)(displayWidth * 0.5), (int)(displayHeight * 0.5));
            SetBackground(32, 34, 37);
            
            IsMaximized = true;
            IsCentered = true;
            IsTransparent = true;
        }

        public void OnClose()
        {
            throw new System.NotImplementedException();
        }

        public void OnRender(double deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void OnStarting()
        {
            throw new System.NotImplementedException();
        }

        public void OnUpdate(double deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void ShutDown()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Handles the EventOnStart event.
        /// </summary>
        private void OnStart()
        {
            HorizontalStack toolbar = ItemFactory.GetToolbar();
            ItemFactory.TopMargin = toolbar.GetHeight();

            VerticalStack layout = ItemFactory.GetStandardLayout();
            HorizontalStack viewPortLayout = ItemFactory.CreateViewportLayout();
            VerticalSplitArea verticalSplit = ItemFactory.CreateVerticalSplitArea();

            var rightAside = new RightAside();
            //var slider = new HorizontalSlider();
            //slider.SetWidth(200);
            //rightAside.AddItem(slider);


            AddItems(toolbar, layout);
            layout.AddItems(viewPortLayout);

            viewPortLayout.AddItems(verticalSplit);

            rightAside.SetParent(viewPortLayout);

            verticalSplit.AssignRightItem(_viewport);

            _viewport.SetHeight(verticalSplit.GetHeight());
            _viewport.SetWidth(verticalSplit.GetWidth());
        }
    }
}