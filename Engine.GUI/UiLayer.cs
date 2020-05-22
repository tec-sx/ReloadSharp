namespace Engine.GUI
{
    using Noesis;
    using NoesisApp;
    using System;

    public class UiLayer
    {
        private Noesis.View _view = null;

        public UiLayer()
        {

        }

        public void Initialize()
        {
#if DEBUG
            Log.SetLogCallback((level, channel, message) =>
            {
                if (channel == "")
                {
                    // [TRACE] [DEBUG] [INFO] [WARNING] [ERROR]
                    string[] prefixes = new string[] { "T", "D", "I", "W", "E" };
                    string prefix = (int)level < prefixes.Length ? prefixes[(int)level] : " ";
                    Console.WriteLine("[NOESIS/" + prefix + "] " + message);
                }
            });
#endif

            GUI.Init();
            GUI.SetXamlProvider(new LocalXamlProvider());
            GUI.SetTextureProvider(new LocalTextureProvider());
            GUI.SetFontProvider(new LocalFontProvider());

            FrameworkElement xaml = (FrameworkElement)GUI.LoadXaml("MenuTest.xaml");
            _view = GUI.CreateView(xaml);
            _view.SetSize(1280, 768);

            //_view.SetIsPPAAEnabled(true);

            // Renderer initialization with an OpenGL device
            //var device = new Render);
            //_view.Renderer.Init(device);
        }

        public void Update(double deltaTime)
        {
            _view.Update(deltaTime);

            // Offscreen rendering phase populates textures needed by the on-screen rendering
            _view.Renderer.UpdateRenderTree();
            _view.Renderer.RenderOffscreen();
        }

        public void Render()
        {
            //_view.Renderer.Render();
        }

        public void OnReshape(int width, int height)
        {
            _view.SetSize(width, height);
        }

        public void OnMouseMove(int x, int y)
        {
            _view.MouseMove(x, y);
        }
    }
}
