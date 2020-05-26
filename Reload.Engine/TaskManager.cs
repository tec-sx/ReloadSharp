namespace Reload.Engine
{
    using System;
    using System.Threading;
    using Reload.Game;

    public class TaskManager
    {
        private Game _game;
        private ManualResetEvent _done;

        public TaskManager(IGame game)
        {
            _game = game as Game ?? throw new ApplicationException(Properties.Resources.GameIsNull);

        }


        public void Update(double deltaTime)
        {

        }

        public void Render(double deltaTime)
        {
            ThreadPool.QueueUserWorkItem(RenderCallback, deltaTime);
        }

        public void RenderCallback(object threadContext)
        {
            var deltaTime = (float)threadContext;
            _game.SceneManager.Render(deltaTime);
            _game.UiManager.Render(deltaTime);
            _done.Set();
        }

    }
}
