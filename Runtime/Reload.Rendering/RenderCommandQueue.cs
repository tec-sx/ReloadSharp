namespace Reload.Rendering
{
    using System;
    using System.Collections.Generic;
    
    public class RenderCommandQueue : Queue<Action>
    {
        public RenderCommandQueue()
            : base(10 * 1024 * 1024) // 10mb buffer
        { }

        public void Execute()
        {
            while (TryDequeue(out var command))
            {
                command.Invoke();
            }
        }
    }
}