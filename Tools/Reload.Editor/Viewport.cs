using SpaceVIL;
using SpaceVIL.Core;

namespace Reload.Editor
{
    public abstract class Viewport : Prototype, IOpenGLLayer
    {
        public abstract void Draw();
        public abstract void Free();
        public abstract void Initialize();
        public abstract bool IsInitialized();
    }
}
