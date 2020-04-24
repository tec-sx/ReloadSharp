namespace Core.AssetsPipeline.GameObjects
{
    using System;
    using Models;
    
    public interface IGameObjectCache : IDisposable
    {
        IGameObject GetGameObject(string fullPath);
    }
}
