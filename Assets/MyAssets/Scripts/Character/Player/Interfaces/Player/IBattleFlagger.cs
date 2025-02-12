

namespace MyAssets
{
    /*
     * 戦闘状態管理クラスのインターフェース
     */
    public interface IBattleFlagger
    {
        bool IsBattleMode {  get; }

        void SetBattleMode(bool b);
    }
}
