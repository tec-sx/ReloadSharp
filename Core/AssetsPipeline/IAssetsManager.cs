namespace Core.Resources
{
    using System;
    using Audio;
    using GameObjects.Models;
    using Textures;

    public interface IAssetsManager : IDisposable
    {
        ITexture GetTexture(string file);
        IGameObject GetGameObject(string file);
        IMusic LoadMusic(string file);
        ISound LoadSound(string file);
    }
}