

namespace MyAssets
{
    /*
     * 攻撃入力のインターフェース
     */
    public interface IAttackInputProvider
    {
        bool Attack {  get; }
        bool ChargeAttack {  get; }
    }
}
