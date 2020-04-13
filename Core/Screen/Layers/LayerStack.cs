using System;
using System.Collections.Generic;

namespace Core.Screen.Layers
{
    public class LayerStack
    {
        private int _layerInsertIndex;
        private readonly List<LayerBase> _layers;

        public LayerStack()
        {
            _layers = new List<LayerBase>();
            _layerInsertIndex = 0;
        }
        
        /// <summary>
        /// Push new layer to the first half of the layers list;
        /// </summary>
        /// <param name="layerBase"></param>
        public void PushLayer(LayerBase layerBase) => _layers.Insert(_layerInsertIndex++, layerBase);

        /// <summary>
        /// Push overlay to the second half of the stack
        /// </summary>
        /// <param name="overlay"></param>
        public void PushOverlay(LayerBase overlay) => _layers.Add(overlay);

        /// <summary>
        /// Pop layer and shift layer insert index
        /// </summary>
        /// <param name="layerBase"></param>
        public void PopLayer(LayerBase layerBase)
        {
            layerBase.OnDetach();
            _layers.Remove(layerBase);
            _layerInsertIndex--;
        }

        /// <summary>
        /// Update all layers
        /// </summary>
        public void Update() => _layers.ForEach(layer => layer.OnUpdate());
        
        /// <summary>
        /// Handle events for all layers
        /// </summary>
        public void HandleEvent() => _layers.ForEach(layer => layer.OnEvent());

        /// <summary>
        /// Pop overlay
        /// </summary>
        /// <param name="overlay"></param>
        public void PopOverlay(LayerBase overlay)
        {
            overlay.OnDetach();
            _layers.Remove(overlay);
        }

        public void DisposeStack()
        {
            _layers.ForEach(layer => layer.OnDetach());
            _layers.Clear();
        }
    }
}