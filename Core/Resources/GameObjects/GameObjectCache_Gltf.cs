namespace Core.Resources.GameObjects
{
    using System.Collections.Generic;
    using SharpGLTF.Schema2;

    public class GameObjectCache_Gltf : IGameObjectCache
    {
        private readonly Dictionary<string, ModelRoot> _modelsDictionary;

        public GameObjectCache_Gltf()
        {
            _modelsDictionary = new Dictionary<string, ModelRoot>();
        }

        public void Dispose()
        {
            foreach (var (key, value) in _modelsDictionary)
            {
                _modelsDictionary.Remove(key);
            }
        }

        public IGameObject GetGameObject(string fullPath)
        {
            ModelRoot model;

            if (!_modelsDictionary.TryGetValue(fullPath, out model))
            {
                model = ModelRoot.Load(fullPath);
                _modelsDictionary.Add(fullPath, model);
            }

            return new GameObject_Gltf(model);
        }
    }
}
