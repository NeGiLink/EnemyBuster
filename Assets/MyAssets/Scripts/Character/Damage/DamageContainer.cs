using UnityEngine;

namespace MyAssets
{
    /*
     * �_���[�W�������s���N���X
     * HP�����A�~�σ_���[�W���Z�Ȃǂ̃X�e�[�^�X�ύX
     * �_���[�W�������̃_���[�W�e�L�X�g�̏o�͂Ȃ�
     */
    [System.Serializable]
    public class DamageContainer : IDamageContainer,ICharacterComponent<ICharacterSetup>
    {
        [SerializeField]
        private float       invincibilityCount = 0.1f;

        private bool        activateKnockback = false;
        public bool         ActivateKnockback => activateKnockback;
        public void         SetActivateKnockback(bool k) { activateKnockback = k; }

        [SerializeField]
        private int         data = 0;
        public void SetData(int d) { data = d; }
        public int          Data => data;

        private DamageType  attackType = DamageType.None;
        public void SetAttackType(DamageType type) { attackType = type; }
        public DamageType   AttackType => attackType;

        private Transform   attacker;
        public void SetAttacker(Transform t) { attacker = t; }
        public Transform    Attacker => attacker;

        private float knockBack;
        public float KnockBack => knockBack;
        public void SetKnockBack(float k) {  knockBack = k; }

        private bool death = false;

        public bool IsDeath => death;

        private bool invalid = false;
        public void SetValid(bool v)
        {
            invalid = v;
        }

        private IBaseStatus baseStauts;

        private Transform   thisTransform;

        private FieldOfView fieldOfView;

        public void DoSetup(ICharacterSetup chara)
        {
            baseStauts = chara.BaseStatus;
            thisTransform = chara.gameObject.transform;
            fieldOfView = chara.gameObject.GetComponent<FieldOfView>();

            death = false;
        }

        public void GiveDamage(int power,float k, DamageType type, Transform transform,CharacterType charaType)
        {
            if(baseStauts.HP <= 0) { return; }
            if (!baseStauts.InvincibilityTimer.IsEnd()) { return; }
            if(invalid) { return; }
            //�_���[�W��^����
            int damage = baseStauts.DecreaseAndDeathCheck(power);
            //GameManager�Ƀ_���[�W�e�L�X�g�o�͂��˗�
            DamageTextOutput(damage,charaType);
            fieldOfView?.AllSearchStart();
            if (activateKnockback)
            {
                baseStauts.AddMaxStoredDamage();
                data = power;
                attackType = type;
                attacker = transform;
                knockBack = k;
                activateKnockback = false;
            }
            else
            {
                //�~�σ_���[�W�ʂ����܂�����
                if (baseStauts.IsMaxStoredDamage(power))
                {
                    data = power;
                    attackType = type;
                    attacker = transform;
                    knockBack = k;
                    activateKnockback = false;
                }
            }
            //���G�X�^�[�g
            baseStauts.InvincibilityTimer.Start(invincibilityCount);
            if (baseStauts.HP <= 0)
            {
                death = true;
            }
        }

        public void ClearDamage()
        {
            data = 0;
            attackType = DamageType.None;
            attacker = null;
            knockBack = 0;
            activateKnockback= false;
        }

        public void DamageTextOutput(int power,CharacterType type)
        {
            if (power <= 0) { return; }
            //�󂯎�����^�O�ʂɃe�L�X�g�̐F��ύX
            Color color = Color.white;
            switch (type)
            {
                case CharacterType.Null:
                    break;
                case CharacterType.Player:
                    break;
                case CharacterType.Enemy:
                    color = Color.red;
                    break;
            }
            DamageTextCreator.Instance.Create(thisTransform, power,color);
        }

        public void Recoil(DamageType type, Transform t)
        {
            attackType = type;
            attacker = t;
            baseStauts.IsMaxStoredDamage(baseStauts.MaxStoredDamage);
        }
    }
}
