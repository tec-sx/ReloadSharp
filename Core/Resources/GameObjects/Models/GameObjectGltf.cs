namespace Core.Resources.GameObjects.Models
{
    using SharpGLTF.Schema2;

    public class GameObjectGltf : IGameObject
    {
        private ModelRoot _model;

        public GameObjectGltf(ModelRoot model)
        {
            _model = model;
        }

        public void Render()
        {
        }

        public void Update()
        {

        }
    }
}
