namespace Reload.Game.Scenes
{
    using Reload.Engine.SceneSystem;
    using System.Collections.Generic;
    using Reload.Game.Characters;
    using Reload.Game.Scenes.Commands;
    using Reload.Input;
    using Silk.NET.Input.Common;
    using Reload.Rendering.Structures;
    using System;
    using Reload.Rendering;
    using Silk.NET.OpenGL;
    using System.Drawing;
    using Reload.Rendering.Camera;
    using System.Numerics;
    using System.Diagnostics;

    public class IntroScene : Scene
    {
        private Player player = new Player();

        private VertexArray _triangleVA;
        private ShaderProgram _triangleShader;

        private VertexArray _squareVA;
        private ShaderProgram _squareShader;

        private OrtographicCamera _camera;

        private float _camerSpeed;
        private Vector3 _cameraPosition;

        private float _cameraRotation;
        private float _cameraRotationSpeed;

        private Vector3 _squarePosition;
        private float _squareMoveSpeed;

        private float _triangleRotation;

        private Stopwatch _stopwatch;


        public override void OnEnter()
        {
            _camera = new OrtographicCamera(16f, 9f);
            _triangleVA = VertexArray.Create();

            float[] vertices =
            {
                -0.5f, -0.5f, 0.0f,     0.8f, 0.2f, 0.8f,
                0.5f, -0.5f, 0.0f,      0.2f, 0.3f, 0.8f,
                0.0f, 0.5f, 0.0f,       0.8f, 0.8f, 0.2f
            };

            var triangleVB = VertexBuffer.Create(new Span<float>(vertices));

            var layout = new BufferLayout
            {
                new BufferElement(ShaderDataType.Float3, "position"),
                new BufferElement(ShaderDataType.Float3, "color")
            };

            triangleVB.SetLayout(layout);
            _triangleVA.AddVertexBuffer(triangleVB);

            uint[] indices = { 0, 1, 2 };

            var triangleIB = IndexBuffer.Create(new Span<uint>(indices));
            _triangleVA.SetIndexBuffer(triangleIB);


            float[] squareVertices =
{
                -0.75f, -0.75f, 0.0f,
                 0.75f, -0.75f, 0.0f,
                 0.75f,  0.75f, 0.0f,
                -0.75f,  0.75f, 0.0f,
            };

            _squareVA = VertexArray.Create();

            var squareVB = VertexBuffer.Create(squareVertices);

            var squareVBLayout = new BufferLayout
            {
                new BufferElement(ShaderDataType.Float3, "position"),
            };

            squareVB.SetLayout(squareVBLayout);
            _squareVA.AddVertexBuffer(squareVB);

            uint[] squareIndices = { 0, 1, 2, 2, 3, 0 };

            var squareIB = IndexBuffer.Create(new Span<uint>(squareIndices));
            _squareVA.SetIndexBuffer(squareIB);


            var shaders = new Dictionary<ShaderType, string>
            {
                { ShaderType.VertexShader, "main.vert" },
                { ShaderType.FragmentShader, "main.frag" }
            };

            var squareShaders = new Dictionary<ShaderType, string>
            {
                { ShaderType.VertexShader, "square.vert" },
                { ShaderType.FragmentShader, "square.frag" }
            };

            _triangleShader = ShaderProgram.Create(shaders, null);
            _squareShader = ShaderProgram.Create(squareShaders, null);

            var mainContext = new InputMappingContext();

            mainContext.MapKeyToActionPress(0, Key.Space, new JumpCommand(player));
            mainContext.MapKeyToState(0, Key.W, new MoveCameraUpCommand(_camera));
            mainContext.MapKeyToState(0, Key.S, new MoveCameraDownCommand(_camera));
            mainContext.MapKeyToState(0, Key.A, new RotateCameraLeft(_camera));
            mainContext.MapKeyToState(0, Key.D, new RotateCameraRight(_camera));
            mainContext.MapKeyToActionPress(0, Key.P, new OpenMenuCommand(this));

            var contexts = new Dictionary<string, Reload.Input.InputMappingContext>
            {
                {"main", mainContext }
            };

            SceneMachine.Input.Handler.LoadContexts(contexts);
            SceneMachine.Input.Handler.PushActiveContext("main");


            _squareMoveSpeed = 5f;

            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public override void OnLeave()
        {
            _stopwatch.Stop();
            SceneMachine.Input.Handler.ClearContexts();
        }

        public override void OnUpdate(double deltaTime)
        {
            _squarePosition.Y = MathF.Sin((float)(_stopwatch.Elapsed.Milliseconds *  deltaTime * 0.1f));
            _triangleRotation += 1f * (float)deltaTime;
            _camera.RecalculateViewMatrix();
        }

        public override void OnRender(double deltaTime)
        {
            RenderCommand.SetClearColor(Color.FromArgb(0, 20, 20, 15));
            RenderCommand.Clear();

            Renderer.BeginScene(_camera);

            Matrix4x4 triangleTransform = Matrix4x4.CreateFromAxisAngle(Vector3.UnitZ, _triangleRotation);

            Matrix4x4 squareScale = Matrix4x4.CreateScale(0.1f);

            Vector4 blueColor = new Vector4(0.6f, 0.2f, 0.3f, 1.0f);
            Vector4 redColor = new Vector4(0.2f, 0.3f, 0.6f, 1.0f);

            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    Vector3 position = new Vector3(x * 2f, y * 2f, 0.0f);
                    Matrix4x4 transform = Matrix4x4.CreateTranslation(position) * squareScale;

                    if (x % 2 == 0)
                    {
                        _squareShader.SetUniform("u_Color", redColor);
                    }
                    else
                    {
                        _squareShader.SetUniform("u_Color", blueColor);
                    }

                    Renderer.Submit(_squareShader, _squareVA, transform);
                }
            }

            Renderer.Submit(_triangleShader, _triangleVA, triangleTransform);


            Renderer.EndScene();
        }
    }
}