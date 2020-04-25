namespace Engine.State
{
    public interface IStateMachine<T>
    {
        void PushState(T state);
        void PopState();
        void UpdateState();
        void RenderState();
    }
}