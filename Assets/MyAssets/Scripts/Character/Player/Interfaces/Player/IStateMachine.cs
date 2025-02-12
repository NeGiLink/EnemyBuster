using System;

namespace MyAssets
{
    /*
     * StateMachineのインターフェース
     */
    public interface IStateMachine : IDisposable
    {
        IState CurrentState { get; }

        event Action<IState> OnStateChanged;
    }

    /*
     * StateChangerのインターフェース
     */
    public interface IStateChanger<TKey>
    {
        bool IsContain(TKey key);
        bool ChangeState(TKey key);
    }
}
