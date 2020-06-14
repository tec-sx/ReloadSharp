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
    using Reload.Configuration;
    using System.IO;

    public class IntroScene : Scene
    {
        private Player player = new Player();

        private VertexArray _triangleVA;
        private ShaderProgram _triangleShader;

        private VertexArray _squareVA;
        private ShaderProgram _squareShader;

        public override void OnEnter()
        {
            var mainContext = new InputMappingContext();

            mainContext.MapKeyToActionPress(0, Key.Space, new JumpCommand(player));
            mainContext.MapKeyToState(0, Key.W, new WalkCommand(player));
            mainContext.MapKeyToState(0, Key.ShiftLeft, new RunCommand(player));
            mainContext.MapKeyToActionPress(0, Key.P, new OpenMenuCommand(this));

            var contexts = new Dictionary<string, Reload.Input.InputMappingContext>
            {
                {"main", mainContext }
            };

            SceneManager.Input.Handler.LoadContexts(contexts);
            SceneManager.Input.Handler.PushActiveContext("main");

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
        }

        public override void OnLeave()
        {
            SceneManager.Input.Handler.ClearContexts();
        }

        public override void OnUpdate(double deltaTime)
        {
        }

        public override void OnRender(double deltaTime)
        {
            _squareShader.Use();
            Renderer.Submit(_squareVA);

            _triangleShader.Use();
            Renderer.Submit(_triangleVA);
        }
    }
}