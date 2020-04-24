
namespace Core.AssetsPipeline
{
    using System;
    using Textures.Models;
    using Audio.Models;
    using GameObjects.Models;

    public interface IAssetsManager : IDisposable
    {
        ITexture GetTexture(string file);
        IGameObject GetGameObject(string file);
        IMusic LoadMusic(string file);
        ISound LoadSound(string file);
    }
}