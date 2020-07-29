namespace Reload.Engine.SceneSystem.Layers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The scene's layer stack.
    /// </summary>
    public class LayerStack : List<Layer>
    {
        private int _layerInsertIndex;
        private readonly Scene _scene;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LayerStack(Scene scene)
            : base(8)
        {
            _scene = scene;
            _layerInsertIndex = 0;
        }

        /// <summary>
        /// Push new layer to the first half of the _layers list;
        /// </summary>
        /// <param name="layer"></param>
        public void PushLayer<T>() where T : Layer, new()
        {
            var layer = new T
            {
                Scene = _scene
            };

            if (layer == null)
            {
                throw new NullReferenceException(
                    Properties.Resources.LayerNullParameterExceptionMessage);
            }

            Insert(_layerInsertIndex++, layer);
            layer.OnAttach();
        }

        /// <summary>
        /// Push overlay to the second half of the stack
        /// </summary>
        /// <param name="overlay"></param>
        public void PushOverlay<T>() where T : Layer, new()
        {
            var overlay = new T
            {
                Scene = _scene
            };

            if (overlay == null)
            {
                throw new NullReferenceException(
                    Properties.Resources.OverlayNullParameterExceptionMessage);
            }

            Add(overlay);
            overlay.OnAttach();
        }

        /// <summary>
        /// Pop layer and shift layer insert index
        /// </summary>
        /// <param name="layer"></param>
        public void PopLayer(Layer layer)
        {
            if (layer == null)
            {
                throw new NullReferenceException(
                    Properties.Resources.LayerNullParameterExceptionMessage);
            }

            layer.OnDetach();
            Remove(layer);
            _layerInsertIndex--;
        }

        /// <summary>
        /// Pop overlay
        /// </summary>
        /// <param name="overlay"></param>
        public void PopOverlay(Layer overlay)
        {
            if (overlay == null)
            {
                throw new NullReferenceException(
                    Properties.Resources.OverlayNullParameterExceptionMessage);
            }

            overlay.OnDetach();
            Remove(overlay);
        }

        /// <summary>
        /// Update all _layers
        /// </summary>
        public void Update(double deltaTime)
        {
            for (var i = 0; i < Count; i++)
            {
                this[i].Update(deltaTime);
            }
        }

        public void Draw(double deltaTime)
        {
            for (var i = 0; i < Count; i++)
            {
                this[i].Draw(deltaTime);
            }
        }

        /// <summary>
        /// Detach all _layers and clears layer stack.
        /// </summary>
        public void ClearStack()
        {
            ForEach(layer => layer.OnDetach());
            Clear();
            _layerInsertIndex = 0;
        }
    }
}