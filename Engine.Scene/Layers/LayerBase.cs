namespace Engine.Scene.Layers
{
    using System;

    public abstract class LayerBase : IDisposable
    {
        public abstract void OnAttach();
        public abstract void OnDetach();
        public abstract void OnUpdate();
        public abstract void OnEvent();
        public abstract void Dispose();
    }
}