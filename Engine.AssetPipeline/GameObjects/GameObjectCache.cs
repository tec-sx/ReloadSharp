namespace Engine.AssetPipeline.GameObjects
{
    using System.Collections.Generic;
    using Models;
    using SharpGLTF.Schema2;

    public class GameObjectCache : IGameObjectCache
    {
        private readonly Dictionary<string, ModelRoot> _modelsDictionary;

        public GameObjectCache()
        {
            _modelsDictionary = new Dictionary<string, ModelRoot>();
        }

        public void Dispose()
        {
            foreach ((string key, ModelRoot _) in _modelsDictionary)
            {
                _modelsDictionary.Remove(key);
            }
        }

        public IGameObject GetGameObject(string fullPath)
        {
            if (!_modelsDictionary.TryGetValue(fullPath, out var model))
            {
                model = ModelRoot.Load(fullPath);
                _modelsDictionary.Add(fullPath, model);
            }

            return new GameObjectGltf(model);
        }
    }
}
