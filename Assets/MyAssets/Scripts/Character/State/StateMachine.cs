using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    //TODO : StateMachine
    /*
     * キャラクターの状態を管理するStateMachine
     */
    [System.Serializable]
    public class StateMachine<TKey> : IStateMachine,IStateChanger<TKey>
    {
        //状態をDictionaryで管理
        private Dictionary<TKey, IState>    stateDictionary;
        //現在の状態
        private IState                      currentState;

        public IState                       CurrentState => currentState;

        public event Action<IState>         OnStateChanged;
        //Start時に設定
        public void DoSetup(IState<TKey>[] states, IEqualityComparer<TKey> comparer = null)
        {
            stateDictionary = new Dictionary<TKey, IState>(comparer);
            foreach (var state in states)
            {
                state.StateChanger = this;
                stateDictionary.Add(state.Key, state);
            }
        }
        //Updateで使う関数
        public void DoUpdate(float time) => currentState?.DoUpdate(time);
        //FixedUpdateで使う関数
        public void DoFixedUpdate(float time) => currentState?.DoFixedUpdate(time);
        //LateUpdateで使う関数
        public void DoLateUpdate(float time) => currentState?.DoLateUpdate(time);
        //Animationの更新が終わったLateUpdateで使う関数
        public void DoAnimatorIKUpdate() => currentState?.DoAnimatorIKUpdate();
        //当たり判定の当たり初めに使う関数
        public void DoTriggerEnter(GameObject thisObject, Collider collider) => currentState?.DoTriggerEnter(thisObject,collider);
        //当たり判定の当たり続けている時に使う関数
        public void DoTriggerStay(GameObject thisObject, Collider collider) => currentState?.DoTriggerStay(thisObject, collider);
        //当たり判定の当たり終わり時に使う関数
        public void DoTriggerExit(GameObject thisObject, Collider collider) => currentState?.DoTriggerExit(thisObject, collider);
        //状態を遷移するための条件を追加する関数
        public bool IsContain(TKey key) => stateDictionary.ContainsKey(key);
        //TODO : Stateの変更
        //状態を変更する時の関数
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
        //オブジェクトが削除される時に使う
        public void Dispose()
        {
            stateDictionary = null;
            currentState = null;
            OnStateChanged = null;
        }
    }
}
