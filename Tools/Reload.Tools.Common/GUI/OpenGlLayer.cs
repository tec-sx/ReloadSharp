using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;

namespace Reload.Tools.Common.GUI
{
    public abstract class OpenGlLayer : Prototype, IOpenGLLayer
    {
        public abstract void Initialize();
        public abstract bool IsInitialized();
        public abstract void Draw();
        public abstract void Free();
    }
}