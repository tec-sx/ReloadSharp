namespace Reload.AssetPipeline.GameObjects
{
    using Models;
    using SharpGLTF.Schema2;
    using System.Collections.Generic;

    public class GameObjectCache : IGameObjectCache
    {
        private readonly Dictionary<string, ModelRoot> _modelsDictionary;

        public GameObjectCache()
        {
            _modelsDictionary = new Dictionary<string, ModelRoot>();
        }

        public void CleanUp()
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
