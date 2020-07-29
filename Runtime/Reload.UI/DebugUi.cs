namespace Reload.UI
{
    using System.Collections.Generic;
    using System.Numerics;
    using System.Diagnostics;
    using ImGuiNET;

    public class DebugUi: UiWindow
    {
        private const int FpsMaxSamples = 60;
        private const int MemoryPlotSize = 8;
        
        private readonly Stopwatch _stopwatch;
        private readonly Vector4 _keyColor;
        private readonly Queue<float> _memoryPlot;

        private readonly List<double> _fpsList;

        public DebugUi()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _keyColor = new Vector4(0.7f, 0.8f, 0.4f, 1f);
            _memoryPlot = new Queue<float>(MemoryPlotSize);

            _fpsList = new List<double>(FpsMaxSamples);
        }

        public override void Draw(double deltaTime)
        {
            Begin("Performance profiling");

            #region Time based measurments
            ImGui.TextColored(_keyColor, "Fps:");
            ImGui.Text(CalculateAverageFps(1.0d / deltaTime).ToString());

            var ts = _stopwatch.Elapsed;

            ImGui.TextColored(_keyColor, "Time:");
            ImGui.Text($"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");
            #endregion

            End();
        }

        private int CalculateAverageFps(double fps)
        {
            double fpsSum = 0.0f;

            if (_fpsList.Count == FpsMaxSamples)
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
        

        public override void Show()
        {
            throw new System.NotImplementedException();
        }
    }
}
