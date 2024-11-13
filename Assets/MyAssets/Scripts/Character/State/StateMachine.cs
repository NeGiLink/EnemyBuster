using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class StateMachine<TKey> : IStateMachine,IStateChanger<TKey>
    {
        Dictionary<TKey, IState> stateDictionary;
        IState currentState;

        public IState CurrentState => currentState;

        public event Action<IState> OnStateChanged;

        public void DoSetup(IState<TKey>[] states, IEqualityComparer<TKey> comparer = null)
        {
            stateDictionary = new Dictionary<TKey, IState>(comparer);
            foreach (var state in states)
            {
                state.StateChanger = this;
                stateDictionary.Add(state.Key, state);
            }
        }

        public void DoUpdate(float time) => currentState?.DoUpdate(time);

        public void DoFixedUpdate(float time) => currentState?.DoFixedUpdate(time);

        public void DoAnimatorIKUpdate() => currentState?.DoAnimatorIKUpdate();

        public void DoTriggerEnter(Collider collider) => currentState?.DoTriggerEnter(collider);

        public void DoTriggerStay(Collider collider) => currentState?.DoTriggerStay(collider);

        public void DoTriggerExit(Collider collider) => currentState?.DoTriggerExit(collider);

        public bool IsContain(TKey key) => stateDictionary.ContainsKey(key);
        public bool ChangeState(TKey key)
        {
            if (!stateDictionary.TryGetValue(key, out IState state))
            {
                return false;
            }
            currentState?.DoExit();
            currentState = state;
            currentState.DoStart();

            OnStateChanged?.Invoke(currentState);
            return true;
        }

        public void Dispose()
        {
            stateDictionary = null;
            currentState = null;
            OnStateChanged = null;
        }
    }
}
