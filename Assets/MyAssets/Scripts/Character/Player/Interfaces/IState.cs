

using UnityEngine;

namespace MyAssets
{
    public interface IState
    {
        void DoStart();
        void DoUpdate(float time);
        void DoFixedUpdate(float time);
        void DoAnimatorIKUpdate();
        void DoExit();

        void DoTriggerEnter(Collider collider);
        void DoTriggerStay(Collider collider);
        void DoTriggerExit(Collider collider);
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
