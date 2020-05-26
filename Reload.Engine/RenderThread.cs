//namespace Reload.Engine
//{
//    using System;
//    using System.Collections.Concurrent;
//    using System.Threading;
//    using Reload.Game;

    //public class RenderThread
    //{
    //    private readonly Thread _thread;
    //    private readonly ConcurrentQueue<Tuple<Func<bool>, NullSignal>> _scheduledTasks;
    //    private readonly Game _game;

    //    public RenderThread(IGame game)
    //    {
    //        _game = game as Game ?? throw new ApplicationException("Game can not be null.");
    //        _thread = new Thread(_game.Run)
    //        {
    //            Name = "RenderThread"
    //        };
    //    }s
    //}
//}
