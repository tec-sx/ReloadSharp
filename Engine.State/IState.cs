namespace Engine.State
{
    public interface IState
    {
        void Enter();
        void Update();
        void Draw();
        void Exit();
    }
}