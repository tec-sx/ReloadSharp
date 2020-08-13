using Reload.Scenes;
using Reload.Configuration;
using Reload.Core.Utils;
using Reload.Rendering;
using Reload.Rendering.Camera;
using Reload.Rendering.Buffers;
using SpaceVIL;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Linq;
using Silk.NET.OpenAL;
using Reload.Rendering.Model;
using Reload.Rendering.Shaders;
using Reload.AssetSystem.Loaders;

namespace Reload.Editor.Scenes
{
    public class DefaultScene : Scene, IViewportAttachable
    {
        private ShaderLibrary _shaderLibrary;

        private OrthographicCamera _orthoCamera;

        private VertexBuffer _squareVB;
        private BufferLayoutCollection _squareBufferLayout;
        private IndexBuffer _squareIB;

        private VertexArray _squareVA;
        //private ShaderProgram _squareShader;

        private VertexBuffer _gridVB;
        private BufferLayoutCollection _gridBufferLayout;
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
            Mesh mesh = MeshLoader.LoadFromFile("RedCube.gltf");
        

            //var position = meshPrimitive.GetVertexAccessor("POSITION")?.AsVector3Array();
            //var normals = meshPrimitive.GetVertexAccessor("NORMAL")?.AsVector3Array();
            //var tangents = meshPrimitive.GetVertexAccessor("TANGENT")?.AsVector4Array();

            //var color0 = meshPrimitive.GetVertexAccessor("COLOR_0")?.AsColorArray();
            //var texCoords0 = meshPrimitive.GetVertexAccessor("TEXCOORD_0")?.AsVector2Array();

            //var triangleSource = meshPrimitive.GetTriangleIndices()?.ToArray();


            //var vertexPosition = box?.GetVertices("POSITION").AsMatrix4x4Array();

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

            _gridBufferLayout = new BufferLayoutCollection
            {
                new BufferElement(ShaderDataType.Float3, "aPosition")
            };

            _gridVB = VertexBuffer.Create(gridVertices, _gridBufferLayout);
            _gridIB = IndexBuffer.Create(squareIndices);

            _gridVA = VertexArray.Create();
            _gridVA.AddVertexBuffer(_gridVB);
            _gridVA.SetIndexBuffer(_gridIB);

            //Square

            _squareBufferLayout = new BufferLayoutCollection
            {
                new BufferElement(ShaderDataType.Float3, "a_Position"),
                new BufferElement(ShaderDataType.Float2, "a_TexCoord")
            };

            _squareVB = VertexBuffer.Create(squareVertices, _squareBufferLayout);
            _squareIB = IndexBuffer.Create(squareIndices);

            _squareVA = VertexArray.Create();
            _squareVA.AddVertexBuffer(_squareVB);
            _squareVA.SetIndexBuffer(_squareIB);


            _shaderLibrary = new ShaderLibrary();

            var squareShader = _shaderLibrary.Load("main");
            var gridShader = _shaderLibrary.Load("grid");

            _squareTexture = Texture2D.CreateFromFile(Path.Combine(ContentPaths.Textures, "test.png"));
            _mexicoTexture = Texture2D.CreateFromFile(Path.Combine(ContentPaths.Textures, "download.png"));

            squareShader.SetInt("u_Texture", 0);

            float gridScale = 160.025f; 
            float gridSize = 10.025f;

            gridShader.SetVector3("Color", Vector3.Zero);
            gridShader.SetFloat("u_Scale", gridScale);
            gridShader.SetFloat("u_Res", gridSize);

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

            Renderer.BeginScene((PerspectiveCamera)CameraController.Camera);
            {
                RenderCommand.SetClearColor(Color.DarkGray);
                Matrix4x4 transform = Matrix4x4.CreateScale(_squareScale)
                                      * Matrix4x4.CreateTranslation(_squarePosition)
                                      * Matrix4x4.CreateRotationX(ReloadMath.DegreesToRadians(_squareRotation.X))
                                      * Matrix4x4.CreateRotationY(ReloadMath.DegreesToRadians(_squareRotation.Y))
                                      * Matrix4x4.CreateRotationZ(ReloadMath.DegreesToRadians(_squareRotation.Z));

                Matrix4x4 gridTransform = Matrix4x4.CreateRotationX(ReloadMath.DegreesToRadians(45.0f));

                _squareTexture.Bind();

                CameraController.UpdateCamera(deltaTime);

                Renderer.Submit(gridShader, _gridVA, gridTransform);
                Renderer.Submit(squareShader, _squareVA, transform);
            }
            Renderer.EndScene();

            Renderer2D.BeginScene(_orthoCamera);
            {
                Renderer2D.DrawQuad(
                    position: new Vector2(-0.5f, 0.0f),
                    size: new Vector2(0.5f, 0.5f),
                    rotation: 0.0f,
                    _mexicoTexture);

                Renderer2D.DrawQuad(
                    position: new Vector2(0.5f, 0.5f),
                    size: new Vector2(0.5f, 0.3f),
                    rotation: 45.0f,
                    Color.DarkRed);
            }
            Renderer2D.EndScene();
        }

        public override void OnUpdate(double deltaTime)
        {
        }

        public void CreateCameraController()
        {
            float aspectRatio = ParentViewport.GetWidth() / ParentViewport.GetHeight();

            Camera = new PerspectiveCamera(45.0f, aspectRatio, 0.01f, 10000.0f);
            Camera.InitLocalCoordinateSystem();
            Camera.SetPosition(1.0f, 2.0f, 5.0f);
            Camera.LookAt(0.0f, 0.0f, 0.0f);

            _orthoCamera = new OrthographicCamera(aspectRatio);

            CameraController = new CameraController(Camera);
        }
    }
}