using System;
using UnityEngine;

namespace MyAssets
{
    /*
     * 全キャラクターが共通で持つステータス
     */
    public class BaseStautsProperty
    {
        //最大HP
        [SerializeField]
        protected int       maxHP;
        public int          MaxHP => maxHP;
        //HP
        [SerializeField]
        protected int       hp;
        public int          HP => hp;
        //速度
        [SerializeField]
        protected float     baseSpeed;
        public float        BaseSpeed => baseSpeed;
        //攻撃力
        [SerializeField]
        protected float     basePower;
        public float        BasePower => basePower;
        //防御力
        [SerializeField]
        protected float     baseDefense;
        public float        BaseDefense => baseDefense;
        //最大蓄積ダメージ
        [SerializeField]
        private int         maxStoredDamage;
        public int          MaxStoredDamage => maxStoredDamage;
        //現在の蓄積ダメージ
        [SerializeField]
        private int         storedDamage;
        public int          StoredDamage => storedDamage;
        //無敵タイマー
        private Timer       invincibilityTimer = new Timer();
        public Timer        InvincibilityTimer => invincibilityTimer;
        //HP回復
        public virtual void RecoveryHP(int h)
        {
            hp += h;
            if (hp > maxHP)
            {
                hp = maxHP;
            }
        }
        //HP減少と死亡確認
        public virtual int DecreaseAndDeathCheck(int d)
        {
            int damage = d - (int)baseDefense;
            hp -= Math.Abs(damage);
            HPUIUpdate();
            return damage;
        }
        //HPUIの更新
        protected virtual void HPUIUpdate() { }
        //蓄積ダメージが最大になったかチェックする
        public bool IsMaxStoredDamage(int damage)
        {
            storedDamage += damage;
            if (storedDamage >= maxStoredDamage)
            {
                return true;
            }
            return false;
        }
        //強制的に最大にする
        public void AddMaxStoredDamage()
        {
            storedDamage = maxStoredDamage;
        }
        //蓄積ダメージをクリアする
        public void ClearStoredDamage()
        {
            storedDamage = 0;
        }
        //HPをセットする
        public virtual void Setup()
        {
            hp = maxHP;
        }
        //タイマーの更新
        public virtual void DoUpdate(float time)
        {
            invincibilityTimer.Update(time);
        }

    }
}
