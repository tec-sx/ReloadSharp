namespace Reload.Engine.StateSystem
{
    public interface IState
    {
        void Enter();
        void Update();
        void Draw();
        void Exit();
    }
}