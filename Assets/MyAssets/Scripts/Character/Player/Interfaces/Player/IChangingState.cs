

namespace MyAssets
{
    public interface IChangingState
    {
        bool IsBattleMode {  get; }

        void SetBattleMode(bool b);
    }
}
