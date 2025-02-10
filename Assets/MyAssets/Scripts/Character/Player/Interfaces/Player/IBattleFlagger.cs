

namespace MyAssets
{
    public interface IBattleFlagger
    {
        bool IsBattleMode {  get; }

        void SetBattleMode(bool b);
    }
}
