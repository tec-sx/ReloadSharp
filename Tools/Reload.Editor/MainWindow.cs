using System.Drawing;
using Reload.Editor.Properties;
using Reload.Editor.Factories;
using SpaceVIL;
using SpaceVIL.Common;

namespace Reload.Editor
{
    internal class MainWindow : ActiveWindow
    {
        public DefaultViewport _viewport;

        public ListBox ItemList = new ListBox();
        public TextArea ItemText = new TextArea();
        public ButtonCore BtnGenerate;
        public ButtonCore BtnSave;
        public SpinItem NumberCount;

        public MainWindow(DefaultViewport viewport)
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

            _viewport.SetHeight(verticalSplit.GetHeight());
            _viewport.SetWidth(verticalSplit.GetWidth());
        }
    }
}