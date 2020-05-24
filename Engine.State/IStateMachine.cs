namespace Reload.State
{
    public interface IStateMachine<in T>
    {
        void PushState(T state);
        void PopState();
        void UpdateState();
        void RenderState();
    }
}