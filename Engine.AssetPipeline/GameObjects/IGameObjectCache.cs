namespace Engine.AssetPipeline.GameObjects
{
    using System;
    using Models;

    public interface IGameObjectCache : IDisposable
    {
        IGameObject GetGameObject(string fullPath);
    }
}
