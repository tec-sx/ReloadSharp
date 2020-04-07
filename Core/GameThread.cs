// namespace Core
// {
//     using System;
//     using System.Threading;
//     using System.Collections.Concurrent;
//     using Logging;
//     using CoreSystem;
//
//     public class GameThread
//     {
//         public event Action Update;
//         public event Action Render;
//         
//         private readonly ConcurrentQueue<Tuple<Func<bool>, NullSignal>> _scheduledTasks;
//         private readonly GraphicsDevice _device;
//         private readonly Thread _thread;
//         
//         private bool _shouldTick;
//         
//         public GameThread(GraphicsDevice device)
//         {
//             _scheduledTasks = new ConcurrentQueue<Tuple<Func<bool>, NullSignal>>();
//             _device = device;
//             _thread = new Thread(RunLoop) { Name = "GameThread"};
//             
//             _thread.Start();
//             ConsoleLog.Info("THREADS",$"{_thread.Name} is running.");
//         }
//
//         internal void EnableTicking()
//         {
//             _shouldTick = true;
//         }
//
//         private void RunLoop()
//         {
//             while (true)
//             {
//                 if (!_shouldTick && _scheduledTasks.IsEmpty)
//                 {
//                     Thread.Sleep(50);
//                     continue;
//                 }
//
//                 while (!_scheduledTasks.IsEmpty)
//                 {
//                     _scheduledTasks.TryDequeue(out var result);
//
//                     var keepRunning = result.Item1.Invoke();
//                     result.Item2.Set();
//
//                     if (!keepRunning || !_shouldTick)
//                     {
//                         return;
//                     }
//                     
//                     Update?.Invoke();
//                     Render?.Invoke();
//                     
//                     _device.SwapBuffers();
//                 }
//             }
//         }
//         
//         internal void Quit()
//         {
//             var signal = new AutoResetSignal();
//             _scheduledTasks.Enqueue(new Tuple<Func<bool>, NullSignal>(() => false, signal));
//
//             if (_thread.IsAlive)
//             {
//                 signal.Wait();
//             }
//         }
//             
//         private class NullSignal
//         {
//             internal static NullSignal Instance { get; } = new NullSignal();
//
//             internal virtual void Set() { }
//
//             internal virtual void Wait() { }
//         }
//
//         private class AutoResetSignal : NullSignal
//         {
//             private readonly AutoResetEvent _signal = new AutoResetEvent(false);
//
//             internal override void Set()
//             {
//                 this._signal.Set();
//             }
//
//             internal override void Wait()
//             {
//                 this._signal.WaitOne();
//             }
//         }
//     }
// }