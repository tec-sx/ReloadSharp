using System;
using System.Collections.Generic;

namespace Engine.Scene.Layers
{
    /// <summary>
    /// The scene's layer stack.
    /// </summary>
    public class LayerStack
    {
        private int layerInsertIndex;
        private readonly List<LayerBase> layers;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LayerStack()
        {
            layers = new List<LayerBase>();
            layerInsertIndex = 0;
        }

        /// <summary>
        /// Push new layer to the first half of the layers list;
        /// </summary>
        /// <param name="layer"></param>
        public void PushLayer(LayerBase layer)
        {
            if (layer == null)
            {
                throw new NullReferenceException(
                    Properties.Resources.LayerNullParameterExceptionMessage);
            }

            layers.Insert(layerInsertIndex++, layer);
        }

        /// <summary>
        /// Push overlay to the second half of the stack
        /// </summary>
        /// <param name="overlay"></param>
        public void PushOverlay(LayerBase overlay)
        {
            if (overlay == null)
            {
                throw new NullReferenceException(
                    Properties.Resources.OverlayNullParameterExceptionMessage);
            }

            layers.Add(overlay);
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

            layers.Remove(layer);
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
            layers.Remove(overlay);
        }

        /// <summary>
        /// Update all layers
        /// </summary>
        public void Update() => layers.ForEach(layer => layer.OnUpdate());

        /// <summary>
        /// Handle events for all layers
        /// </summary>
        public void HandleEvent() => layers.ForEach(layer => layer.OnEvent());

        /// <summary>
        /// Detach all layers and clears layer stack.
        /// </summary>
        public void ClearStack()
        {
            layers.ForEach(layer => layer.OnDetach());
            layers.Clear();
        }
    }
}