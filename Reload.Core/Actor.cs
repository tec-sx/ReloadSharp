namespace Reload.Core
{
    using System;

    public abstract class Actor
    {
        public void HandleCommand(Command command)
        {
            command.Execute(this);
        }

        public virtual void Jump()
        {
            Console.WriteLine("Jump");
        }

    }
}
