
namespace MyAssets
{
    public interface ICharacterComponent
    {
        void DoSetup(ICharacterSetup chara);
    }
    public interface IPlayerComponent
    {
        void DoSetup(IPlayerSetup player);
    }
    public interface ISlimeComponent
    {
        void DoSetup(ISlimeSetup slime);
    }
    public interface IPlayerState<TKey> : IState<TKey>, IPlayerComponent{ }
    public interface ISlimeState<TKey> : IState<TKey>, ISlimeComponent { }
    public interface ICharacterStateTransition<TKey>
    {
        bool IsTransition();
        void DoTransition();
    }
}
