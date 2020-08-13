using System;
using Reload.Rendering.Camera;
using Reload.Rendering;
using Reload.Rendering.Model;


namespace Reload.Scenes
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
        public Scene ActiveScene;
        public SceneInfo SceneInfo;
    }

    public struct SceneInfo
    {
        public PerspectiveCamera Camera;
    }
    
    public static class SceneRenderer
    {
        public static void Init()
        {
            throw new NotImplementedException();
        }

        public static void SetViewportSize(uint width, uint height)
        {
            throw new NotImplementedException();
        }

        public static void BeginScene(Scene scene)
        {
            throw new NotImplementedException();
        }

        public static void EndScene()
        {
            throw new NotImplementedException();
        }

        public static void SubmitEntity(Entity entity)
        {
            throw new NotImplementedException();
        }

        public static (TextureCube, TextureCube) CreateEnvironmentMap(string filepath)
        {
            throw new NotImplementedException();
        }

        public static RenderPass GetFinalRenderPass()
        {
            throw new NotImplementedException();
        }

        public static Texture2D GetFinalColorBuffer()
        {
            throw new NotImplementedException();
        }

        private static void FlushDrawList()
        {
            throw new NotImplementedException();
        }

        public static void GeometryPass()
        {
            throw new NotImplementedException();
        }

        public static void CompositePass()
        {
            throw new NotImplementedException();
        }

        public static uint GetFinalColorBufferRendererID()
        {
            throw new NotImplementedException();
        }

        public static SceneRendererOptions GetOptions()
        {
            throw new NotImplementedException();
        }
        
    }
}
