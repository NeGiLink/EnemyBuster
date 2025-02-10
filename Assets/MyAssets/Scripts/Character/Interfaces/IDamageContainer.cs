using UnityEngine;

namespace MyAssets
{
    /*�_���[�W�N���X�̃C���^�[�t�F�[�X
     * �O����������擾���鎞�ɕK�v�ȏ�񂾂��擾�ł���悤�Ɏ������Ă���
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
