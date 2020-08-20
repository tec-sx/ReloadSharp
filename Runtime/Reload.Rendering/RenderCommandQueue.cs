using System;
using System.Collections.Generic;

namespace Reload.Rendering
{
    /// <summary>
    /// Class for syncronizing render commands.
    /// </summary>
    public class RenderCommandQueue : Queue<Action>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderCommandQueue"/> class.
        /// </summary>
        public RenderCommandQueue()
            : base(10 * 1024 * 1024) // 10mb buffer
        { }

        /// <summary>
        /// Executes every command present in the queue.
        /// </summary>
        public void Execute()
        {
            while (TryDequeue(out var command))
            {
                command?.Invoke();
            }
        }
    }
}