using UnityEngine;

namespace MyAssets
{
    /*ダメージクラスのインターフェース
     * 外部から情報を取得する時に必要な情報だけ取得できるように実装している
     */
    public interface IDamageContainer
    {
        int         Data { get; }
        DamageType  AttackType { get; }
        Transform   Attacker { get; }

        float       KnockBack {  get; }

        bool        ActivateKnockback {  get; }

        bool        IsDeath {  get; }

        void        SetValid(bool v);

        void        SetData(int d);

        void        SetActivateKnockback(bool k);

        void        SetAttackType(DamageType attackType);

        void        SetAttacker(Transform t);

        void        SetKnockBack(float k);

        void        GiveDamage(int power, float k, DamageType type, Transform transform, CharacterType charaType);

        void        ClearDamage();

        void        Recoil(DamageType type, Transform t);
    }
}
