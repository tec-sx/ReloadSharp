namespace Reload.UI
{
    using System.Collections.Generic;
    using System.Numerics;
    using System.Diagnostics;
    using Game;
    using ImGuiNET;
    using Reload.Core.Collections;

    public class DebugUi: IUserInterface
    {
        private const int fpsMaxSamples = 60;
        private const int MemoryPlotSize = 8;

        private readonly IGame _game;
        private readonly Stopwatch _stopwatch;
        private readonly Vector4 _keyColor;
        private readonly Queue<float> _memoryPlot;

        private FastList<double> _fpsList;

        public DebugUi(IGame game)
        {
            _game = game;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _keyColor = new Vector4(0.7f, 0.8f, 0.4f, 1f);
            _memoryPlot = new Queue<float>(MemoryPlotSize);

            _fpsList = new FastList<double>(fpsMaxSamples);
        }

        public void Draw(double deltaTime)
        {
            ImGui.BeginMainMenuBar();

            #region Time based measurments
            ImGui.TextColored(_keyColor, "Fps:");
            ImGui.Text(CalculateAverageFps(1.0d / deltaTime).ToString());

            var ts = _stopwatch.Elapsed;

            ImGui.TextColored(_keyColor, "Time:");
            ImGui.Text($"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");
            #endregion

            ImGui.EndMainMenuBar();
        }

        private int CalculateAverageFps(double fps)
        {
            double fpsSum = 0.0f;

            if (_fpsList.Count == fpsMaxSamples)
            {
                _fpsList.RemoveAt(0);
            }

            _fpsList.Add(fps);

            for (int i = 0; i < _fpsList.Count; i++)
            {
                fpsSum += _fpsList[i];
            }

            return (int)(fpsSum / _fpsList.Count);
        }
    }
}
