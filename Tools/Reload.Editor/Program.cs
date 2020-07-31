using SpaceVIL.Common;

namespace Reload.Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            GameEditor gameEditor = new GameEditor();
            
            gameEditor.Initialize();
            gameEditor.Start();
            gameEditor.ShutDown();
        }
    }
}
