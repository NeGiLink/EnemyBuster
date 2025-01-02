using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class DamageContainer : IDamageContainer,ICharacterComponent<ICharacterSetup>
    {
        [SerializeField]
        private float invincibilityCount = 0.1f;

        [SerializeField]
        private int data = 0;
        public void SetData(int d) { data = d; }
        public int Data => data;

        private DamageType attackType = DamageType.None;
        public void SetAttackType(DamageType type) { attackType = type; }
        public DamageType AttackType => attackType;

        private Transform attacker;
        public void SetAttacker(Transform t) { attacker = t; }
        public Transform Attacker => attacker;

        private IBaseStauts baseStauts;

        private Transform thisTransform;

        private FieldOfView fieldOfView;

        public void DoSetup(ICharacterSetup chara)
        {
            baseStauts = chara.BaseStauts;
            thisTransform = chara.gameObject.transform;
            fieldOfView = chara.gameObject.GetComponent<FieldOfView>();
        }

        public void GiveYouDamage(int power, DamageType type, Transform transform)
        {
            if(baseStauts.HP <= 0) { return; }
            if (!baseStauts.InvincibilityTimer.IsEnd()) { return; }
            //�_���[�W��^����
            baseStauts.DecreaseAndDeathCheck(power);
            //GameManager�Ƀ_���[�W�e�L�X�g�o�͂��˗�
            DamageTextOutput(power);
            fieldOfView?.AllSearchStart();
            //�~�σ_���[�W�ʂ����܂�����
            if (baseStauts.IsMaxStoredDamage(power))
            {
                data = power;
                attackType = type;
                attacker = transform;
            }
            //���G�X�^�[�g
            baseStauts.InvincibilityTimer.Start(invincibilityCount);
        }

        public void DamageTextOutput(int power)
        {
            if (power > 0)
            {
                GameManager.Instance.DamageTextCreator.Crate(thisTransform, power);
            }
        }

        public void Recoil(DamageType type, Transform t)
        {
            attackType = type;
            attacker = t;
            baseStauts.IsMaxStoredDamage(baseStauts.MaxStoredDamage);
        }
    }
}
