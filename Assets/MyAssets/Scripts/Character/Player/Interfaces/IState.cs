

namespace MyAssets
{
    public interface IState
    {
        void DoStart();
        void DoUpdate(float time);
        void DoFixedUpdate(float time);

        void DoAnimatorIKUpdate();
        void DoExit();
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
