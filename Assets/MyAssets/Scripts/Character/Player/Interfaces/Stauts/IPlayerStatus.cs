namespace MyAssets
{
    /*
     * �x�[�X�̃X�e�[�^�X�̃C���^�[�t�F�[�X
     */
    public interface IBaseStatus
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
     * ���L����̓L�����N�^�[���Ƃ̃X�e�[�^�X�̃C���^�[�t�F�[�X
     * ���̃L�����N�^�[�Ǝ��̗v�f�������Ă�����ǉ�����`
     */
    public interface IPlayerStatus : IBaseStatus
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
    public interface ISlimeStatus : IBaseStatus
    {

    }
    public interface IMushroomStatus : IBaseStatus
    {

    }
    public interface IBullTankStatus : IBaseStatus
    {

    }
    public interface IGolemStatus : IBaseStatus
    {

    }
    public interface INPCStatus : IBaseStatus
    {

    }
}
