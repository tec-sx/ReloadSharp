namespace Reload.Scene.Commands
{
    using Reload.Core.Commands;

    public abstract class ProgramFlowCommand : Command
    {
        public ProgramFlowCommand()
            : base(InputType.ActionPress)
        { }

        public abstract void Execute(SceneBase scene);
    }
}
