using SpaceVIL;

namespace Reload.Tools.Common.GUI
{
    public class MainWindow : ActiveWindow
    {
        private readonly string _windowName;
        private readonly OpenGlLayer _openGlLayer;
        private bool _usingOpenGlLayer;
        
        public MainWindow(string windowName)
        {
            _windowName = windowName;
        }

        public MainWindow(string windowName, OpenGlLayer openGlLayer)
            : this(windowName)
        {
            _openGlLayer = openGlLayer;
            _usingOpenGlLayer = true;
        }
        
        public override void InitWindow()
        {

        }
    }
}