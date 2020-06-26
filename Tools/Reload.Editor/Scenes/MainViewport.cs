namespace Reload.Editor.Scenes
{
    using Reload.Configuration;
    using Reload.Editor.Scenes.Layers.Components;
    using Reload.Engine.SceneSystem;
    using Reload.Rendering;
    using Reload.Rendering.Camera;
    using Reload.Rendering.Structures;
    using Silk.NET.OpenGL;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Numerics;

    public class MainViewport : Scene
    {
        private OrtographicCamera _orthoCamera;

        private VertexBuffer _squareVB;
        private BufferLayout _squareBufferLayout;
        private IndexBuffer _squareIB;
        private VertexArray _squareVA;
        private ShaderProgram _squareShader;

        private Texture2D _squareTexture;

        private float _squareScale;
        private Vector3 _squarePosition;
        private Vector3 _squareRotation;

        public override void OnEnter()
        {
            float[] squareVertices =
{
                -0.75f, -0.75f, 0.0f, /* Texture */ 0.0f, 0.0f,
                 0.75f, -0.75f, 0.0f, /* Texture */ 1.0f, 0.0f,
                 0.75f,  0.75f, 0.0f, /* Texture */ 1.0f, 1.0f,
                -0.75f,  0.75f, 0.0f, /* Texture */ 0.0f, 1.0f,
            };

            uint[] squareIndices = { 0, 1, 2, 2, 3, 0 };

            _squareBufferLayout = new BufferLayout
            {
                new BufferElement(ShaderDataType.Float3, "position"),
                new BufferElement(ShaderDataType.Float2, "texCoord")
            };

            _squareVB = VertexBuffer.Create(squareVertices);
            _squareVB.SetLayout(_squareBufferLayout);
            _squareIB = IndexBuffer.Create(squareIndices);

            _squareVA = VertexArray.Create();
            _squareVA.AddVertexBuffer(_squareVB);
            _squareVA.SetIndexBuffer(_squareIB);

            var squareShaders = new Dictionary<ShaderType, string>
            {
                { ShaderType.VertexShader, "square.vert" },
                { ShaderType.FragmentShader, "square.frag" }
            };

            _squareShader = ShaderProgram.Create(squareShaders, null);

            _squareTexture = Texture2D.CreateFromFile(Path.Combine(ContentPaths.Textures, "test.png"));

            _squareShader.SetUniform("u_texture", 0);

            _orthoCamera = new OrtographicCamera(16, 9);
            
            _squareScale = 3.0f;
            _squarePosition = Vector3.Zero;
            _squareRotation = Vector3.Zero;

            RightAsideComponent.SizeValueChanged += (value) => _squareScale = value;
            RightAsideComponent.PositionChanged += (value) => _squarePosition = value;
            RightAsideComponent.RotationChanged += (value) => _squareRotation = value;
        }

        public override void OnLeave()
        {
        }

        public override void OnRender(double deltaTime)
        {
            RenderCommand.SetClearColor(Color.AliceBlue);
            RenderCommand.Clear();

            Renderer.BeginScene(_orthoCamera);
            {
                Matrix4x4 transform = Matrix4x4.CreateScale(_squareScale)
                                    * Matrix4x4.CreateTranslation(_squarePosition)
                                    * Matrix4x4.CreateRotationX(_squareRotation.X)
                                    * Matrix4x4.CreateRotationY(_squareRotation.Y)
                                    * Matrix4x4.CreateRotationZ(_squareRotation.Z);

                _squareTexture.Bind();
                Renderer.Submit(_squareShader, _squareVA, transform);
            }
            Renderer.EndScene();
        }

        public override void OnUpdate(double deltaTime)
        {
        }
    }
}
