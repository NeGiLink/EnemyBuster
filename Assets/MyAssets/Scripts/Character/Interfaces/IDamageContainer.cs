using UnityEngine;

namespace MyAssets
{
    public interface IDamageContainer
    {
        float Data { get; }

        void SetData(float d);

        AttackType AttackType { get; }
        void SetAttackType(AttackType attackType);

        Transform Attacker { get; }
        void SetAttacker(Transform t);
    }
}
