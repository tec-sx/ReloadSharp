using Core.Config;

namespace Core.Resources.Scene
{
    using System;
    using Raylib_cs;
    using System.Collections.Generic;

    public class SceneGraph
    {
        private ContentPath _contentPath;
        
        public SceneGraph(IConfiguration configuration)
        {
            _contentPath = configuration.ContentPath;
        }

        public ModelBase CreateModel<T>(string modelFileName) where T : ModelBase, new()
        {
            var model3d = Raylib.LoadModel(modelFileName);
            var model = new T
            {
                ParentScene = this
            };
            
            return model;
        }
        
        public void Dispose()
        {
        }

        public void Init()
        {
            
        }

        public void Render()
        {
            
        }
    }
}