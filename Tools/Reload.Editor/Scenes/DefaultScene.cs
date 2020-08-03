using Reload.Scenes;
using Reload.Configuration;
using Reload.Core.Utils;
using Reload.Rendering;
using Reload.Rendering.Camera;
using Reload.Rendering.Structures;
using SpaceVIL;
using System.Drawing;
using System.IO;
using System.Numerics;

namespace Reload.Editor.Scenes
{
    public class DefaultScene : Scene, IViewportAttachable
    {
        private ShaderLibrary _shaderLibrary;

        private VertexBuffer _squareVB;
        private BufferLayout _squareBufferLayout;
        private IndexBuffer _squareIB;

        private VertexArray _squareVA;
        //private ShaderProgram _squareShader;

        private VertexBuffer _gridVB;
        private BufferLayout _gridBufferLayout;
        private IndexBuffer _gridIB;
        private VertexArray _gridVA;

        private Texture2D _squareTexture;
        private Texture2D _mexicoTexture;

        private float _squareScale;
        private Vector3 _squarePosition;
        private Vector3 _squareRotation;

        private Size _projectionSize;

        public Prototype ParentViewport { get; set; }

        public override void OnEnter()
        {
            float[] squareVertices =
            {
                -0.75f, -0.75f, 0.0f, /* Texture */ 0.0f, 0.0f,
                0.75f, -0.75f, 0.0f, /* Texture */ 1.0f, 0.0f,
                0.75f, 0.75f, 0.0f, /* Texture */ 1.0f, 1.0f,
                -0.75f, 0.75f, 0.0f, /* Texture */ 0.0f, 1.0f,
            };

            float[] gridVertices =
            {
                -1.0f, 1.0f, 0.0f, // Top Left
                -1.0f, -1.0f, 0.0f, // Bottom Left
                1.0f, -1.0f, 0.0f, // Bottom Right
                1.0f, 1.0f, 0.0f, // Top Right
            };

            uint[] squareIndices = {0, 1, 2, 2, 3, 0};

            // Grid

            _gridBufferLayout = new BufferLayout
            {
                new BufferElement(ShaderDataType.Float3, "aPosition")
            };

            _gridVB = VertexBuffer.Create(gridVertices);
            _gridVB.SetLayout(_gridBufferLayout);
            _gridIB = IndexBuffer.Create(squareIndices);

            _gridVA = VertexArray.Create();
            _gridVA.AddVertexBuffer(_gridVB);
            _gridVA.SetIndexBuffer(_gridIB);

            //Square

            _squareBufferLayout = new BufferLayout
            {
                new BufferElement(ShaderDataType.Float3, "a_position"),
                new BufferElement(ShaderDataType.Float2, "a_texCoord")
            };

            _squareVB = VertexBuffer.Create(squareVertices);
            _squareVB.SetLayout(_squareBufferLayout);
            _squareIB = IndexBuffer.Create(squareIndices);

            _squareVA = VertexArray.Create();
            _squareVA.AddVertexBuffer(_squareVB);
            _squareVA.SetIndexBuffer(_squareIB);


            _shaderLibrary = new ShaderLibrary();

            var squareShader = _shaderLibrary.Load("main");
            var gridShader = _shaderLibrary.Load("grid");

            _squareTexture = Texture2D.CreateFromFile(Path.Combine(ContentPaths.Textures, "test.png"));
            // _mexicoTexture = Texture2D.CreateFromFile(Path.Combine(ContentPaths.Textures, "download.png"));

            squareShader.SetUniform("u_texture", 0);

            gridShader.SetUniform("color", Vector3.Zero);


            // Create Camera
            CreateCameraController();

            // Map input contexts

            _squareScale = 1.0f;
            _squarePosition = Vector3.Zero;
            _squareRotation = Vector3.Zero;
        }

        public override void OnLeave()
        {
        }

        public override void OnRender(double deltaTime)
        {
            var squareShader = _shaderLibrary["main"];
            var gridShader = _shaderLibrary["grid"];

            Renderer.BeginScene(CameraController.Camera);
            {
                RenderCommand.SetClearColor(Color.Aquamarine);
                Matrix4x4 transform = Matrix4x4.CreateScale(_squareScale)
                                      * Matrix4x4.CreateTranslation(_squarePosition)
                                      * Matrix4x4.CreateRotationX(ReloadMath.DegreesToRadiants(_squareRotation.X))
                                      * Matrix4x4.CreateRotationY(ReloadMath.DegreesToRadiants(_squareRotation.Y))
                                      * Matrix4x4.CreateRotationZ(ReloadMath.DegreesToRadiants(_squareRotation.Z));

                Matrix4x4 gridTransform = Matrix4x4.CreateRotationX(ReloadMath.DegreesToRadiants(45.0f));

                _squareTexture.Bind();

                CameraController.UpdateCamera(deltaTime);

                Renderer.Submit(gridShader, _gridVA, gridTransform);
                Renderer.Submit(squareShader, _squareVA, transform);
            }
        }

        public override void OnUpdate(double deltaTime)
        {
        }

        public void CreateCameraController()
        {
            float aspectRatio = ParentViewport.GetWidth() / ParentViewport.GetHeight();

            Camera = new Camera(ReloadMath.DegreesToRadiants(45.0f), aspectRatio, 0.1f, 10000.0f);
            Camera.InitLocalCoordinateSystem();
            Camera.SetPosition(1.0f, 1.0f, 1.0f);
            Camera.LookAt(0.0f, 0.0f, -1.0f);

            CameraController = new CameraController(Camera);
        }
    }
}