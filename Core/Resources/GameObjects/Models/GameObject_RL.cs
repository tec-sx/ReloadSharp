namespace Core.Resources.GameObjects
{
    using System;
    using Raylib_cs;


    public class GameObject_RL : IGameObject
    {
        private readonly Model _model;

        public GameObject_RL(Model model)
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
