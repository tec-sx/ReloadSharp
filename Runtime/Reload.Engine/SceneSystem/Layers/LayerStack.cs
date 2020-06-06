namespace Reload.Engine.SceneSystem.Layers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The scene's layer stack.
    /// </summary>
    public class LayerStack
    {
        private int _layerInsertIndex;
        private readonly List<Layer> _layers;
        private readonly Scene _scene;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LayerStack(Scene scene)
        {
            _layers = new List<Layer>(8);
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

            _layers.Insert(_layerInsertIndex++, layer);
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

            _layers.Add(overlay);
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
            _layers.Remove(layer);
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
            _layers.Remove(overlay);
        }

        /// <summary>
        /// Update all _layers
        /// </summary>
        public void Update(double deltaTime)
        {
            for (var i = 0; i < _layers.Count; i++)
            {
                _layers[i].Update(deltaTime);
            }
        }

        public void Draw(double deltaTime)
        {
            for (var i = 0; i < _layers.Count; i++)
            {
                _layers[i].Draw(deltaTime);
            }
        }

        /// <summary>
        /// Detach all _layers and clears layer stack.
        /// </summary>
        public void ClearStack()
        {
            _layers.ForEach(layer => layer.OnDetach());
            _layers.Clear();
            _layerInsertIndex = 0;
        }
    }
}