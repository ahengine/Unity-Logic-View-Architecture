using System.Collections.Generic;

namespace UPatterns
{
    public class FiniteStateMachine<T>
    {
        protected Dictionary<T, State<T>> states;

        protected State<T> currentState;
        public State<T> CurrentState => currentState;
        public T CurrentStateID
        {
            get
            {
                foreach (var state in states)
                    if (state.Value == currentState)
                        return state.Key;

                return default(T);
            }
        }
        public bool HasAState => currentState != null;

        public FiniteStateMachine() => states = new Dictionary<T, State<T>>();

        public void Add(State<T> state) => states.Add(state.ID, state);
        public void Add(T stateID, State<T> state) => states.Add(stateID, state);

        public State<T> GetState(T stateID) =>
            states.ContainsKey(stateID) ? states[stateID] : null;

        public void SetCurrentState(T stateID) =>
            SetCurrentState(states[stateID]);

        public State<T> GetCurrentState => currentState;

        public void SetCurrentState(State<T> state)
        {
            ExitCurrentState();
            EnterState(state);
        }

        public void ExitCurrentState()
        {
            if (currentState != null)
                currentState.Exit();

            currentState = null;
        }

        public void EnterState(T stateID) =>
            EnterState(states[stateID]);
        public void EnterState(State<T> state)
        {
            if (currentState != null)
            {
                SetCurrentState(state);
                return;
            }

            if (currentState == state)
                return;

            currentState = state;

            if (currentState != null)
                currentState.Enter();
        }

        public void Update()
        {
            if (currentState != null)
                currentState.Updates();
        }
        public void FixedUpdate()
        {
            if (currentState != null)
                currentState.FixedUpdates();
        }
    }
}