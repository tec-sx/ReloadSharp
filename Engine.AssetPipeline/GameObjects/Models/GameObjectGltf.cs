namespace Engine.AssetPipeline.GameObjects.Models
{
    using SharpGLTF.Schema2;

    public class GameObjectGltf : IGameObject
    {
        private readonly ModelRoot _model;

        public GameObjectGltf(ModelRoot model)
        {
            _model = model;
        }

        public void Draw()
        {
        }

        public void Update()
        {

        }
    }
}
