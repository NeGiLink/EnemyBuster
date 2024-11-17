using UnityEngine;

namespace MyAssets
{
    public interface IDamageContainer
    {
        int Data { get; }

        void SetData(int d);

        AttackType AttackType { get; }
        void SetAttackType(AttackType attackType);

        Transform Attacker { get; }
        void SetAttacker(Transform t);
    }
}
