using System;

namespace Reload.Game.Scenes.Layers
{
    /// <summary>
    /// Layer base abstract class. Every layer
    /// must inherit this class.
    /// </summary>
    public abstract class Layer : IDrawable
    {
        public event Action DrawOrderChanged;
        public event Action VisibleChanged;

        public Scene Scene { get; set; }

        public bool Visible => throw new NotImplementedException();

        public int DrawOrder => throw new NotImplementedException();

        /// <summary>
        /// Override this method to add logic upon attaching the
        /// current layer to the scene.
        /// </summary>
        public abstract void OnAttach();

        /// <summary>
        /// Override this method to add logic before detaching/disposing the
        /// current layer from the scene.
        /// </summary>
        public abstract void OnDetach();

        /// <summary>
        /// Override this method to add update logic.
        /// </summary>
        public abstract void Update(double deltaTime);


        /// <summary>
        /// Override this method to draw the layer.
        /// </summary>
        /// <param name="deltaTime"></param>
        public abstract void Draw(double deltaTime);

        public bool BeginDraw()
        {
            throw new NotImplementedException();
        }

        public void EndDraw()
        {
            throw new NotImplementedException();
        }
    }
}