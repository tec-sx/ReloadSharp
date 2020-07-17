namespace Reload.Editor.Scenes.Layers.Components
{
    using ImGuiNET;
    using Reload.UI;
    using System;
    using  System.Numerics;

    public class RightAsideComponent : UiWindow
    {

        public static Action<float> SizeValueChanged;
        public static Action<Vector3> PositionChanged;
        public static Action<Vector3> RotationChanged;

        private float _sizeValue = 3.0f;
        private Vector3 _position;
        private Vector3 _rotation;

        public override void Draw()
        {
            if (show)
            {
                if(!Begin("mainAside"))
                {
                    End();
                }
                else
                {
                    
                    var position = new Vector2(Program.Editor.Window.Size.Width - ImGui.GetWindowWidth(), 50);
                    var size = new Vector2(Program.Editor.Window.Monitor.Bounds.Width / 5, Program.Editor.Window.Size.Height - 50);

                    ImGui.SetWindowPos(position);
                    ImGui.SetWindowSize(size);

                    if (ImGui.BeginTabBar("asideTabs"))
                    {
                        if (ImGui.BeginTabItem("Main"))
                        {
                            ImGui.Text("Scale");

                            float oldSize = _sizeValue;
                            ImGui.SliderFloat("", ref _sizeValue, 0.0f, 10.0f);

                            if (oldSize != _sizeValue)
                            {
                                SizeValueChanged?.Invoke(_sizeValue);
                            }

                            ImGui.Text("Position");

                            float oldPositionX = _position.X;
                            float oldPositionY = _position.Y;
                            float oldPositionZ = _position.Z;

                            ImGui.SliderFloat("Position X", ref _position.X, -10.0f, 10.0f);
                            ImGui.SliderFloat("Position Y", ref _position.Y, -10.0f, 10.0f);
                            ImGui.SliderFloat("Position Z", ref _position.Z, -10.0f, 10.0f);

                            if (oldPositionX != _position.X || oldPositionY != _position.Y || oldPositionZ != _position.Z)
                            {
                                PositionChanged?.Invoke(_position);
                            }

                            ImGui.Text("Rotation");

                            float oldRotationX = _rotation.X;
                            float oldRotationY = _rotation.Y;
                            float oldRotationZ = _rotation.Z;

                            ImGui.SliderFloat("Rotation X", ref _rotation.X, -180.0f, 180.0f);
                            ImGui.SliderFloat("Rotation Y", ref _rotation.Y, -180.0f, 180.0f);
                            ImGui.SliderFloat("Rotation Z", ref _rotation.Z, -180.0f, 180.0f);


                            if (oldRotationX != _rotation.X || oldRotationY != _rotation.Y || oldRotationZ != _rotation.Z)
                            {
                                RotationChanged?.Invoke(_rotation);
                            }

                            ImGui.EndTabItem();
                        }

                        if (ImGui.BeginTabItem("Properties"))
                        {
                            ImGui.Text("Properties");
                            ImGui.EndTabItem();
                        }

                        ImGui.EndTabBar();
                    }

                    End();
                }
            }
        }

        public override void Show()
        {
            show = true;
        }
    }
}
