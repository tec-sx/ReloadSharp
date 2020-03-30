namespace Core.State
{
    using System.Collections.Generic;
    
    public class StateMachine<T> : IStateMachine<T>
        where T : IState
    {
        private readonly Stack<T> _states;

        public StateMachine()
        {
            _states = new Stack<T>();
        }

        public void PushState(T state)
        {
            _states.Push(state);
            state.Enter();
        }

        public void PopState()
        {
            _states.Pop();
            _states.Peek().Exit();
        }

        public void UpdateState()
        {
            _states.Peek().Update();
        }

        public void RenderState()
        {
            _states.Peek().Draw();
        }
    }
}