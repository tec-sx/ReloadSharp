namespace Core.Resources.GameObjects
{
    using System;

    public interface IGameObjectCache : IDisposable
    {
        IGameObject GetGameObject(string fullPath);
    }
}
