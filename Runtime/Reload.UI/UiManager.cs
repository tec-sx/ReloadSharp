using System.Reflection;
using System.Runtime.InteropServices;
using Reload.Core.IO;
using Ultz.SilkExtensions.ImGui;

namespace Reload.UI
{
    using System;
    using System.Numerics;
    using ImGuiNET;
    using System.Drawing;
    using Silk.NET.OpenGL;
    using Silk.NET.Windowing.Common;
    using Silk.NET.Input.Common;
    using System.Collections.Generic;

    public class UiManager : IDisposable
    {
        private List<UiWindow> _uiWindows;

#if DEBUG
        private DebugUi _debugUi;
        private ImGuiController _controller;
#endif

        public UiManager()
        {
        }

        public unsafe void Initialize(GL api, IWindow appWindow, IInputContext inputContext)
        {
            ImGui.CreateContext();
            _controller = new ImGuiController(api, appWindow, inputContext);
            _uiWindows = new List<UiWindow>();

            var io = ImGui.GetIO();

            io.ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard;
            io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;
            io.ConfigFlags |= ImGuiConfigFlags.ViewportsEnable;

            var fontName = "MajorMonoDisplay-Regular.ttf";
            var font = io.Fonts.AddFontFromFileTTF(fontName, 18.0f);

            SetStyles();
            
#if DEBUG
            AddWindow(new DebugUi());
#endif
        }

        public void AddWindow(UiWindow window)
        {
            _uiWindows.Add(window);
        }

        public void Update(double deltaTime)
        {
            _controller.Update((float)deltaTime);
        }

        public void Render(double deltaTime)
        {
            for (int i = 0; i < _uiWindows.Count; i++)
            {
                _uiWindows[i].Draw(deltaTime);
            }

            _controller.Render();
        }

        public void Resize(Size size)
        {
        }

        private void SetStyles()
        {
            ImGui.StyleColorsDark();

            var style = ImGui.GetStyle();
            var colors = style.Colors;


            // colors[(int)ImGuiCol.Text] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            // colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);
            // colors[(int)ImGuiCol.WindowBg] = new Vector4(0.2f, 0.2f, 0.3f, 0.5f);
            // colors[(int)ImGuiCol.ChildBg] = new Vector4(0.3f, 0.3f, 0.3f, 0.0f);
            // colors[(int)ImGuiCol.PopupBg] = new Vector4(0.3f, 0.3f, 0.3f, 0.5f);
            // colors[(int)ImGuiCol.Border] = new Vector4(0.266f, 0.266f, 0.266f, 1.000f);
            // colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.000f, 0.000f, 0.000f, 0.000f);
            // colors[(int)ImGuiCol.FrameBg] = new Vector4(0.160f, 0.160f, 0.160f, 1.000f);
            // colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.200f, 0.200f, 0.200f, 1.000f);
            // colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.280f, 0.280f, 0.280f, 1.000f);
            // colors[(int)ImGuiCol.TitleBg] = new Vector4(0.148f, 0.148f, 0.148f, 1.000f);
            // colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.148f, 0.148f, 0.148f, 1.000f);
            // colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.148f, 0.148f, 0.148f, 1.000f);
            // colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.195f, 0.195f, 0.195f, 1.000f);
            // colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.160f, 0.160f, 0.160f, 1.000f);
            // colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.277f, 0.277f, 0.277f, 1.000f);
            // colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.300f, 0.300f, 0.300f, 1.000f);
            // colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
            // colors[(int)ImGuiCol.CheckMark] = new Vector4(1.000f, 1.000f, 1.000f, 1.000f);
            // colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.391f, 0.391f, 0.391f, 1.000f);
            // colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
            // colors[(int)ImGuiCol.Button] = new Vector4(0.0f, 0.0f, 0.0f, 0.5f);
            // colors[(int)ImGuiCol.ButtonHovered] = new Vector4(1.000f, 1.000f, 1.000f, 0.156f);
            // colors[(int)ImGuiCol.ButtonActive] = new Vector4(1.000f, 1.000f, 1.000f, 0.391f);
            // colors[(int)ImGuiCol.Header] = new Vector4(0.313f, 0.313f, 0.313f, 1.000f);
            // colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.469f, 0.469f, 0.469f, 1.000f);
            // colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.469f, 0.469f, 0.469f, 1.000f);
            // colors[(int)ImGuiCol.Separator] = colors[(int)ImGuiCol.Border];
            // colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.391f, 0.391f, 0.391f, 1.000f);
            // colors[(int)ImGuiCol.SeparatorActive] = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
            // colors[(int)ImGuiCol.ResizeGrip] = new Vector4(1.000f, 1.000f, 1.000f, 0.250f);
            // colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(1.000f, 1.000f, 1.000f, 0.670f);
            // colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
            // colors[(int)ImGuiCol.Tab] = new Vector4(0.098f, 0.098f, 0.098f, 1.000f);
            // colors[(int)ImGuiCol.TabHovered] = new Vector4(0.352f, 0.352f, 0.352f, 1.000f);
            // colors[(int)ImGuiCol.TabActive] = new Vector4(0.195f, 0.195f, 0.195f, 1.000f);
            // colors[(int)ImGuiCol.TabUnfocused] = new Vector4(0.098f, 0.098f, 0.098f, 1.000f);
            // colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4(0.195f, 0.195f, 0.195f, 1.000f);
            // colors[(int)ImGuiCol.PlotLines] = new Vector4(0.469f, 0.469f, 0.469f, 1.000f);
            // colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
            // colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.586f, 0.586f, 0.586f, 1.000f);
            // colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
            // colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(1.000f, 1.000f, 1.000f, 0.156f);
            // colors[(int)ImGuiCol.DragDropTarget] = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
            // colors[(int)ImGuiCol.NavHighlight] = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
            // colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
            // colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.000f, 0.000f, 0.000f, 0.586f);
            // colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.000f, 0.000f, 0.000f, 0.586f);

            style.ChildRounding = 0.0f;
            style.FrameBorderSize = 0.0f;
            style.FrameRounding = 0.0f;
            style.GrabMinSize = 7.0f;
            style.PopupRounding = 0.0f;
            style.ScrollbarRounding = 0.0f;
            style.ScrollbarSize = 13.0f;
            style.TabBorderSize = 1.0f;
            style.TabRounding = 0.0f;
            style.WindowRounding = 0.0f;
        }

        public void Dispose()
        {
            // _controller?.Dispose();
        }
    }
}