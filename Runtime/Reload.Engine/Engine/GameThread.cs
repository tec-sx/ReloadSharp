namespace Reload.Engine
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using Reload.Game;

    public class GameThread
    {
        private readonly Thread _thread;
        private readonly ConcurrentQueue<Tuple<Func<bool>, NullSignal>> _scheduledTasks;
        private readonly Game _game;

        public GameThread(IGame game)
        {
            _game = game as Game ?? throw new ApplicationException("Game can not be null.");
            _thread = new Thread(_game.Run)
            {
                Name = "GameThread"
            };
        }

        public void Run()
        {
            _thread.Start();
        }

        internal void Quit()
        {
            var signal = new AutoResetSignal();
            _scheduledTasks.Enqueue(new Tuple<Func<bool>, NullSignal>(() => false, signal));

            if (_thread.IsAlive)
            {
                signal.Wait();
            }
        }

        private class NullSignal
        {
            internal static NullSignal Instance { get; } = new NullSignal();

            internal virtual void Set() { }

            internal virtual void Wait() { }
        }

        private class AutoResetSignal : NullSignal
        {
            private readonly AutoResetEvent _signal = new AutoResetEvent(false);

            internal override void Set()
            {
                this._signal.Set();
            }

            internal override void Wait()
            {
                this._signal.WaitOne();
            }
        }

    }
}
