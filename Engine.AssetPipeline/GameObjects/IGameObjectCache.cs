namespace Engine.AssetPipeline.GameObjects
{
    using System;
    using Models;

    public interface IGameObjectCache
    {
        IGameObject GetGameObject(string fullPath);
        void CleanUp();
    }
}
