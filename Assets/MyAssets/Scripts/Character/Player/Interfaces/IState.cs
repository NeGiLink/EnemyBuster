

using UnityEngine;

namespace MyAssets
{
    public interface IState
    {
        void DoStart();
        void DoUpdate(float time);
        void DoFixedUpdate(float time);
        void DoLateUpdate(float time);
        void DoAnimatorIKUpdate();
        void DoExit();

        void DoTriggerEnter(GameObject thisObject,Collider collider);
        void DoTriggerStay(GameObject thisObject,Collider collider);
        void DoTriggerExit(GameObject thisObject, Collider collider);
    }
    public interface IStateKey<TKey>
    {
        TKey Key { get; }
    }
    public interface IState<TKey> : IState, IStateKey<TKey>
    {
        IStateChanger<TKey> StateChanger { set; }
    }
}
