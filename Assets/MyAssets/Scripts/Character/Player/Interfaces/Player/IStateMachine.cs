using System;

namespace MyAssets
{
    /*
     * StateMachine�̃C���^�[�t�F�[�X
     */
    public interface IStateMachine : IDisposable
    {
        IState CurrentState { get; }

        event Action<IState> OnStateChanged;
    }

    /*
     * StateChanger�̃C���^�[�t�F�[�X
     */
    public interface IStateChanger<TKey>
    {
        bool IsContain(TKey key);
        bool ChangeState(TKey key);
    }
}
