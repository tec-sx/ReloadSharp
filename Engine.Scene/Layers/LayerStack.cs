using System;
using System.Collections.Generic;
using Reload.Core;
using Reload.Core.Collections;
using Reload.UI;

namespace Reload.Scene.Layers
{
    /// <summary>
    /// The scene's layer stack.
    /// </summary>
    public class LayerStack
    {
        private int layerInsertIndex;
        private readonly FastList<LayerBase> _layers;
        private readonly SceneBase _scene;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LayerStack(SceneBase scene)
        {
            _layers = new FastList<LayerBase>();
            _scene = scene;

            layerInsertIndex = 0;
        }

        /// <summary>
        /// Push new layer to the first half of the _layers list;
        /// </summary>
        /// <param name="layer"></param>
        public void PushLayer<T>() where T : LayerBase, new()
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

            _layers.Insert(layerInsertIndex++, layer);
            layer.OnAttach();
        }

        /// <summary>
        /// Push overlay to the second half of the stack
        /// </summary>
        /// <param name="overlay"></param>
        public void PushOverlay<T>() where T : LayerBase, new()
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
        public void PopLayer(LayerBase layer)
        {
            if (layer == null)
            {
                throw new NullReferenceException(
                    Properties.Resources.LayerNullParameterExceptionMessage);
            }

            layer.OnDetach();
            _layers.Remove(layer);
            layerInsertIndex--;
        }

        /// <summary>
        /// Pop overlay
        /// </summary>
        /// <param name="overlay"></param>
        public void PopOverlay(LayerBase overlay)
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
        }
    }
}