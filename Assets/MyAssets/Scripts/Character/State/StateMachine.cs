using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    //TODO : StateMachine
    /*
     * �L�����N�^�[�̏�Ԃ��Ǘ�����StateMachine
     */
    [System.Serializable]
    public class StateMachine<TKey> : IStateMachine,IStateChanger<TKey>
    {
        //��Ԃ�Dictionary�ŊǗ�
        private Dictionary<TKey, IState>    stateDictionary;
        //���݂̏��
        private IState                      currentState;

        public IState                       CurrentState => currentState;

        public event Action<IState>         OnStateChanged;
        //Start���ɐݒ�
        public void DoSetup(IState<TKey>[] states, IEqualityComparer<TKey> comparer = null)
        {
            stateDictionary = new Dictionary<TKey, IState>(comparer);
            foreach (var state in states)
            {
                state.StateChanger = this;
                stateDictionary.Add(state.Key, state);
            }
        }
        //Update�Ŏg���֐�
        public void DoUpdate(float time) => currentState?.DoUpdate(time);
        //FixedUpdate�Ŏg���֐�
        public void DoFixedUpdate(float time) => currentState?.DoFixedUpdate(time);
        //LateUpdate�Ŏg���֐�
        public void DoLateUpdate(float time) => currentState?.DoLateUpdate(time);
        //Animation�̍X�V���I�����LateUpdate�Ŏg���֐�
        public void DoAnimatorIKUpdate() => currentState?.DoAnimatorIKUpdate();
        //�����蔻��̓����菉�߂Ɏg���֐�
        public void DoTriggerEnter(GameObject thisObject, Collider collider) => currentState?.DoTriggerEnter(thisObject,collider);
        //�����蔻��̓����葱���Ă��鎞�Ɏg���֐�
        public void DoTriggerStay(GameObject thisObject, Collider collider) => currentState?.DoTriggerStay(thisObject, collider);
        //�����蔻��̓�����I��莞�Ɏg���֐�
        public void DoTriggerExit(GameObject thisObject, Collider collider) => currentState?.DoTriggerExit(thisObject, collider);
        //��Ԃ�J�ڂ��邽�߂̏�����ǉ�����֐�
        public bool IsContain(TKey key) => stateDictionary.ContainsKey(key);
        //TODO : State�̕ύX
        //��Ԃ�ύX���鎞�̊֐�
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
        //�I�u�W�F�N�g���폜����鎞�Ɏg��
        public void Dispose()
        {
            stateDictionary = null;
            currentState = null;
            OnStateChanged = null;
        }
    }
}
