namespace MyAssets
{
    /*
     * 防御の判定をまとめたクラスのインターフェース
     */
    public interface IGuardTrigger
    {
        bool IsGuard { get; }

        void SetGuardFlag(bool guard);
    }
}

