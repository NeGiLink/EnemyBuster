namespace MyAssets
{
    /*
     * ベースのステータスのインターフェース
     */
    public interface IBaseStauts
    {
        int             MaxHP { get; }
        int             HP { get; }
        float           BaseSpeed { get; }
        float           BasePower { get; }
        float           BaseDefense { get; }
        int             MaxStoredDamage {  get; }
        int             StoredDamage {  get; }
        public Timer    InvincibilityTimer {  get; }
        void            RecoveryHP(int h);
        int             DecreaseAndDeathCheck(int d);
        bool            IsMaxStoredDamage(int damage);
        void            AddMaxStoredDamage();
        void            ClearStoredDamage();


        void            DoUpdate(float time);
    }
    /*
     * 下記からはキャラクターごとのステータスのインターフェース
     * そのキャラクター独自の要素を持っていたら追加する形
     */
    public interface IPlayerStauts : IBaseStauts
    {
        int MaxSP { get; }
        int SP {  get; }

        int RollingUseSP { get; }

        int SpinAttackUseSP {  get; }

        int GuardUseSP {  get; }

        int CounterAttackUseSP { get; }
        int ChargeAttackUseSP { get; }
        void DecreaseSP(int s);
        void RecoverySP(int s);
    }
    public interface ISlimeStauts : IBaseStauts
    {

    }
    public interface IMushroomStauts : IBaseStauts
    {

    }
    public interface IBullTankStauts : IBaseStauts
    {

    }
    public interface IGolemStauts : IBaseStauts
    {

    }
    public interface INPCStauts : IBaseStauts
    {

    }
}
