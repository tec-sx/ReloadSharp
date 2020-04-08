namespace Core.Resources.GameObjects.Models
{
    using System;
    using Raylib_cs;


    public class GameObjectRl : IGameObject
    {
        private readonly Model _model;

        public GameObjectRl(Model model)
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
