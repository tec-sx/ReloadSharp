using System.Numerics;

namespace Reload.UI
{
    using System.Diagnostics;
    using Game;
    using ImGuiNET;

    public class DebugUi: IUserInterface
    {
        private readonly IGame _game;
        private readonly Stopwatch _stopwatch;
        private readonly Vector4 _keyColor;
        public DebugUi(IGame game)
        {
            _game = game;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _keyColor = new Vector4(205f, 231f, 96f, 1f);
        }

        public void Draw()
        {
            #region Time based measurments
            ImGui.TextColored(_keyColor, "Fps:");
            ImGui.Text(_game.Window.FramesPerSecond.ToString("N"));

            var ts = _stopwatch.Elapsed;

            ImGui.TextColored(_keyColor, "Time:");
            ImGui.Text($"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");
            #endregion
        }


    }
}
