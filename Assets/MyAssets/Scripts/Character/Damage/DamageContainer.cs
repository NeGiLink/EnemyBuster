using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class DamageContainer : IDamageContainer,ICharacterComponent<ICharacterSetup>
    {
        [SerializeField]
        private int data = 0;
        public void SetData(int d) { data = d; }
        public int Data => data;

        private AttackType attackType = AttackType.None;
        public void SetAttackType(AttackType type) { attackType = type; }
        public AttackType AttackType => attackType;

        private Transform attacker;
        public void SetAttacker(Transform t) { attacker = t; }
        public Transform Attacker => attacker;

        private IBaseStauts baseStauts;

        private Transform thisTransform;

        public void DoSetup(ICharacterSetup chara)
        {
            baseStauts = chara.BaseStauts;
            thisTransform = chara.gameObject.transform;
        }

        public void SetAttackerData(int power, AttackType type, Transform transform)
        {
            if (!baseStauts.InvincibilityTimer.IsEnd()) { return; }
            baseStauts.DecreaseAndDeathCheck(power);
            if (power > 0)
            {
                GameManager.Instance.DamageTextCreator.Crate(thisTransform, power);
            }
            if (baseStauts.IsMaxStoredDamage(power))
            {
                data = power;
                attackType = type;
                attacker = transform;
            }
            baseStauts.InvincibilityTimer.Start(0.1f);
        }

        public void Recoil(AttackType type, Transform t)
        {
            attackType = type;
            attacker = t;
            baseStauts.IsMaxStoredDamage(baseStauts.MaxStoredDamage);
        }
    }
}
