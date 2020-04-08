namespace Core.Resources.GameObjects
{
    using System;
    using Models;

    public interface IGameObjectCache : IDisposable
    {
        IGameObject GetGameObject(string fullPath);
    }
}
