using System;

namespace CoxlinCore
{
    public class State
    {
        public Action OnStateEnter;
        public Action OnStateExit;
        public Action OnStateUpdate;
    }

    public static class StateUtils
    {
        public static void ChangeState(State oldState, State newState)
        {
            oldState?.OnStateExit?.Invoke();
            newState.OnStateEnter?.Invoke();
        }
    }
}