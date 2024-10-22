

namespace MyAssets
{
    public interface IAttackInputProvider
    {
        Timer AttackInputTimer { get; }
        bool Attack {  get; }
    }
}
