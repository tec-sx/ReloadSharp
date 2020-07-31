using System.Drawing;
using Reload.Editor.Properties;
using Reload.Editor.Factories;
using SpaceVIL;
using SpaceVIL.Common;

namespace Reload.Editor
{
    public class MainWindow : ActiveWindow
    {
        private OpenGlViewport _viewport;

        internal ListBox ItemList = new ListBox();
        internal TextArea ItemText = new TextArea();
        internal ButtonCore BtnGenerate;
        internal ButtonCore BtnSave;
        internal SpinItem NumberCount;

        public MainWindow(OpenGlViewport viewport)
        {
            _viewport = viewport;
            EventOnStart += OnStart;
        }

        public override void InitWindow()
        {
            string windowTitle = $"{Resources.Name} - v.{Resources.Version}";
            
            int displayWidth = DisplayService.GetDisplayWidth();
            int displayHeight = DisplayService.GetDisplayHeight();
            SetParameters(nameof(MainWindow), windowTitle, (int)(displayWidth * 0.5), (int)(displayHeight * 0.5), true);
            SetMinSize((int)(displayWidth * 0.5), (int)(displayHeight * 0.5));
            SetBackground(32, 34, 37);
            
            IsMaximized = true;
            IsCentered = true;
            IsTransparent = true;
        }

        public void OnStart()
        {
            HorizontalStack toolbar = ItemFactory.GetToolbar();
            ItemFactory.TopMargin = toolbar.GetHeight();

            VerticalStack layout = ItemFactory.GetStandardLayout();
            VerticalSplitArea verticalSplit = ItemFactory.CreateVerticalSplitArea();

            AddItems(toolbar, layout);
            layout.AddItems(verticalSplit);

            verticalSplit.AssignRightItem(_viewport);
        }
    }
}