namespace Reload.Engine.StateSystem
{
    public interface IStateMachine<in T>
    {
        void PushState(T state);
        void PopState();
        void UpdateState();
        void RenderState();
    }
}