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

        private IndexBuffer _indexBuffer;
        private VertexBuffer _vertexBuffer;
        private VertexArray _vertexArray;
        private ShaderProgram _shaderProgram;

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

            _vertexArray = VertexArray.Create();

            float[] vertices =
            {
                -0.5f, -0.5f, 0.0f,     0.8f, 0.2f, 0.8f,
                0.5f, -0.5f, 0.0f,      0.2f, 0.3f, 0.8f,
                0.0f, 0.5f, 0.0f,       0.8f, 0.8f, 0.2f
            };

            _vertexBuffer = VertexBuffer.Create(new Span<float>(vertices));

            var layout = new BufferLayout
            {
                new BufferElement(ShaderDataType.Float3, "position"),
                new BufferElement(ShaderDataType.Float3, "color")
            };

            _vertexBuffer.SetLayout(layout);
            _vertexArray.AddVertexBuffer(_vertexBuffer);

            uint[] indices = { 0, 1, 2 };

            _indexBuffer = IndexBuffer.Create(new Span<uint>(indices));
            _vertexArray.SetIndexBuffer(_indexBuffer);

            var shaders = new Dictionary<ShaderType, string>
            {
                { ShaderType.VertexShader, "main.vert" },
                { ShaderType.FragmentShader, "main.frag" }
            };

            _shaderProgram = ShaderProgram.Create(shaders, null);
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
            _shaderProgram.Use();

            Renderer.Submit(_vertexArray);
        }
    }
}