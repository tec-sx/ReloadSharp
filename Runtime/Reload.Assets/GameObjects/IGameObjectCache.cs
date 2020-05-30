namespace Reload.Assets.GameObjects
{
    using Models;

    public interface IGameObjectCache
    {
        IGameObject GetGameObject(string fullPath);
        void CleanUp();
    }
}
