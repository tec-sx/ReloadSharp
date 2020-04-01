namespace Core.Resources.GameObjects
{
    using SharpGLTF.Schema2;

    public class GameObject_Gltf : IGameObject
    {
        private ModelRoot _model;

        public GameObject_Gltf(ModelRoot model)
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
