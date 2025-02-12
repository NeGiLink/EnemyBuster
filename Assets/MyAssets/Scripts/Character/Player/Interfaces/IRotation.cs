namespace MyAssets
{
    /*
     * キャラクターの回転処理クラスのインターフェース
     */
    public interface IRotation
    {
        void DoUpdate();

        void DoFixedUpdate();

        void DoFreeMode();
    }
}
