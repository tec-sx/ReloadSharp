using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reload.Scenes;

namespace Reload.Rendering
{
    public struct SceneRendererOptions
    {
        public bool ShowGrid;
        public bool ShowBoundingBoxes;

        public SceneRendererOptions(bool showGrid, bool showBoundingBoxes)
        {
            ShowGrid = showGrid;
            ShowBoundingBoxes = showBoundingBoxes;
        }
    }

    public struct SceneRendererData
    {
        
    }
    
    public static class SceneRenderer
    {
        public static void Init()
        {
            
        }

        public static void SetViewportSize(uint width, uint height)
        {
            
        }

        public static void BeginScene(Scene scene)
        {
            
        }

        public static void EndScene()
        {
            
        }

        public static void SubmitEntity(Entity entity)
        {
            
        }
    }
}
