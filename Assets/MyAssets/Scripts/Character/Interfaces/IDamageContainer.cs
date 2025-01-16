using UnityEngine;

namespace MyAssets
{
    public interface IDamageContainer
    {
        int Data { get; }

        void SetData(int d);

        void SetActivateKnockback(bool k);

        DamageType AttackType { get; }
        void SetAttackType(DamageType attackType);

        Transform Attacker { get; }
        void SetAttacker(Transform t);

        void GiveDamage(int power, DamageType type, Transform transform,CharacterType charaType);

        void Recoil(DamageType type, Transform t);
    }
}
