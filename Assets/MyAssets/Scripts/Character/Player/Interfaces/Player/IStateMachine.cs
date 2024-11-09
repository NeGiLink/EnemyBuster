using System;

namespace MyAssets
{
    public interface IStateMachine : IDisposable
    {
        IState CurrentState { get; }

        event Action<IState> OnStateChanged;
    }

    public interface IStateChanger<TKey>
    {
        bool IsContain(TKey key);
        bool ChangeState(TKey key);
    }
}
