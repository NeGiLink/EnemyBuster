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
        public virtual void RecoveryHP(int h)
        {
            hp += h;
            if (hp > maxHP)
            {
                hp = maxHP;
            }
        }

        public virtual bool DecreaseAndDeathCheck(int d)
        {
            hp -= d;
            HPUIUpdate();
            if(hp <= 0)
            {
                return true;
            }
            return false;
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
