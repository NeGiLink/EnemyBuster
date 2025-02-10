using System;
using UnityEngine;

namespace MyAssets
{
    /*
     * �S�L�����N�^�[�����ʂŎ��X�e�[�^�X
     */
    public class BaseStautsProperty
    {
        //�ő�HP
        [SerializeField]
        protected int       maxHP;
        public int          MaxHP => maxHP;
        //HP
        [SerializeField]
        protected int       hp;
        public int          HP => hp;
        //���x
        [SerializeField]
        protected float     baseSpeed;
        public float        BaseSpeed => baseSpeed;
        //�U����
        [SerializeField]
        protected float     basePower;
        public float        BasePower => basePower;
        //�h���
        [SerializeField]
        protected float     baseDefense;
        public float        BaseDefense => baseDefense;
        //�ő�~�σ_���[�W
        [SerializeField]
        private int         maxStoredDamage;
        public int          MaxStoredDamage => maxStoredDamage;
        //���݂̒~�σ_���[�W
        [SerializeField]
        private int         storedDamage;
        public int          StoredDamage => storedDamage;
        //���G�^�C�}�[
        private Timer       invincibilityTimer = new Timer();
        public Timer        InvincibilityTimer => invincibilityTimer;
        //HP��
        public virtual void RecoveryHP(int h)
        {
            hp += h;
            if (hp > maxHP)
            {
                hp = maxHP;
            }
        }
        //HP�����Ǝ��S�m�F
        public virtual int DecreaseAndDeathCheck(int d)
        {
            int damage = d - (int)baseDefense;
            hp -= Math.Abs(damage);
            HPUIUpdate();
            return damage;
        }
        //HPUI�̍X�V
        protected virtual void HPUIUpdate() { }
        //�~�σ_���[�W���ő�ɂȂ������`�F�b�N����
        public bool IsMaxStoredDamage(int damage)
        {
            storedDamage += damage;
            if (storedDamage >= maxStoredDamage)
            {
                return true;
            }
            return false;
        }
        //�����I�ɍő�ɂ���
        public void AddMaxStoredDamage()
        {
            storedDamage = maxStoredDamage;
        }
        //�~�σ_���[�W���N���A����
        public void ClearStoredDamage()
        {
            storedDamage = 0;
        }
        //HP���Z�b�g����
        public virtual void Setup()
        {
            hp = maxHP;
        }
        //�^�C�}�[�̍X�V
        public virtual void DoUpdate(float time)
        {
            invincibilityTimer.Update(time);
        }

    }
}
