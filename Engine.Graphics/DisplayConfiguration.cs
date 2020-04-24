namespace Engine.Graphics
{
    using System.Drawing;
    using System.Numerics;

    public struct DisplayConfiguration
    {
        public Point  Resolution;
        public int    RefreshRate;
        public int    TargetFps;
        public bool   InFullScreen;
        public bool   EnableVSync;
        public bool   EnableVulkan;
        public string WindowTitle;
    }
}
