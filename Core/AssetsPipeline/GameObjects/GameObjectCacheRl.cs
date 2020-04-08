namespace Core.Resources.GameObjects
{
    using System.Collections.Generic;
    using Raylib_cs;
    using RaylibModel = Raylib_cs.Model;
    using Models;

    public class GameObjectCacheRl : IGameObjectCache
    {
        private readonly Dictionary<string, RaylibModel> _modelsDictionary;

        public GameObjectCacheRl()
        {
            _modelsDictionary = new Dictionary<string, RaylibModel>();
        }

        public void Dispose()
        {
            foreach (var (key, value) in _modelsDictionary)
            {
                Raylib.UnloadModel(value);
                _modelsDictionary.Remove(key);
            }
        }

        public IGameObject GetGameObject(string fullPath)
        {
            RaylibModel raylibModel;

            if (!_modelsDictionary.TryGetValue(fullPath, out raylibModel))
            {
                raylibModel = Raylib.LoadModel(fullPath);
                _modelsDictionary.Add(fullPath, raylibModel);
            }

            return new GameObjectRl(raylibModel);
        }
    }
}
