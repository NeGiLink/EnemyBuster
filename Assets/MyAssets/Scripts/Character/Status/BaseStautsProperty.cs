using System;
using UnityEngine;

namespace MyAssets
{
    public class BaseStautsProperty
    {
        [SerializeField]
        protected int maxHP;
        public int MaxHP => maxHP;

        [SerializeField]
        protected int hp;
        public int HP => hp;

        [SerializeField]
        protected float baseSpeed;
        public float BaseSpeed => baseSpeed;
        [SerializeField]
        protected float basePower;
        public float BasePower => basePower;
        [SerializeField]
        protected float baseDefense;
        public float BaseDefense => baseDefense;
        public virtual void RecoveryHP(int h)
        {
            hp += h;
            if (hp > maxHP)
            {
                hp = maxHP;
            }
        }

        public virtual int DecreaseAndDeathCheck(int d)
        {
            int damage = d - (int)baseDefense;
            hp -= Math.Abs(damage);
            HPUIUpdate();
            return damage;
        }

        protected virtual void HPUIUpdate() { }

        [SerializeField]
        private int maxStoredDamage;
        public int MaxStoredDamage => maxStoredDamage;
        [SerializeField]
        private int storedDamage;
        public int StoredDamage => storedDamage;
        public bool IsMaxStoredDamage(int damage)
        {
            storedDamage += damage;
            if (storedDamage >= maxStoredDamage)
            {
                return true;
            }
            return false;
        }
        public void AddMaxStoredDamage()
        {
            storedDamage = maxStoredDamage;
        }
        public void ClearStoredDamage()
        {
            storedDamage = 0;
        }
        private Timer invincibilityTimer = new Timer();
        public Timer InvincibilityTimer => invincibilityTimer;

        public virtual void Setup()
        {
            hp = maxHP;
        }

        public virtual void DoUpdate(float time)
        {
            invincibilityTimer.Update(time);
        }

    }
}
