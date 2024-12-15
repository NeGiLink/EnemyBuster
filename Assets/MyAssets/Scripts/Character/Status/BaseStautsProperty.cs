using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class BaseStautsProperty
    {
        [SerializeField]
        private int maxHP;
        public int MaxHP => maxHP;

        [SerializeField]
        private int hp;
        public int HP => hp;
        public void RecoveryHP(int h)
        {
            hp += h;
            if (hp > maxHP)
            {
                hp = maxHP;
            }
        }

        public bool DecreaseAndDeathCheck(int d)
        {
            hp -= d;
            if(hp <= 0)
            {
                return true;
            }
            return false;
        }

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
        public void ClearStoredDamage()
        {
            storedDamage = 0;
        }
        private Timer invincibilityTimer = new Timer();
        public Timer InvincibilityTimer => invincibilityTimer;

        public virtual void DoUpdate(float time)
        {
            invincibilityTimer.Update(time);
        }

    }
}
