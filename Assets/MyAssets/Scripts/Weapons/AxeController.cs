using UnityEngine;

namespace MyAssets
{
    public enum AxeSEType
    {
        Single,
        Damage
    }
    /*
     * �u���^���N�̕���(�A�b�N�X)�̃N���X
     * �\���E��\����R���C�_�[�̐ݒ�A�����蔻��̋�ʂ��s���Ă���
     */
    [RequireComponent(typeof(SwordEffectHandler))]
    [RequireComponent(typeof(SphereCollider))]
    public class AxeController : BaseAttackController
    {
        [SerializeField]
        private Transform           thisTransform;

        [SerializeField]
        private IBullTankAnimator   animator;

        private IBullTankSetup      bullTank;

        [SerializeField]
        private new SphereCollider  collider;

        //�ۑ��p��center�Eradius
        private Vector3             center;

        private float               radius;


        public void SetAttackType(AttackType type) { attackType = type; }

        private SwordEffectHandler  swordEffectHandler;


        protected override void Awake()
        {
            base.Awake();

            BullTankController controller = GetComponentInParent<BullTankController>();

            if (controller != null)
            {
                bullTank = controller.GetComponent<IBullTankSetup>();
                animator = controller.BullTankAnimator;
            }

            collider = GetComponent<SphereCollider>();

            swordEffectHandler = GetComponent<SwordEffectHandler>();

            center = collider.center;
            radius = collider.radius;
        }

        private void NoActivateCollider()
        {
            collider.center = Vector3.zero;
            collider.radius = 0.0f;
        }

        private void ActivateCollider()
        {
            collider.center = center;
            collider.radius = radius;
        }

        private void Start()
        {
            collider.enabled = false;
        }

        public void EnabledCollider(float start, float end, bool all)
        {
            if (all)
            {
                collider.enabled = true;
                swordEffectHandler.ActivateSlachEffect(true);
                ActivateCollider();
            }
            else
            {
                AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
                if (animInfo.normalizedTime >= start && animInfo.normalizedTime <= end)
                {
                    collider.enabled = true;
                    ActivateCollider();
                    swordEffectHandler.ActivateSlachEffect(true);
                }
                else
                {
                    NoActivateCollider();
                    swordEffectHandler.ActivateSlachEffect(false);
                }
            }
        }
        public void NotEnabledCollider()
        {
            NoActivateCollider();
            swordEffectHandler.ActivateSlachEffect(false);
            collider.enabled = false;
        }

        public void SlashSE()
        {
            seHandler.Play((int)AxeSEType.Single);
        }

        //����_���ƃL�����_����+�����l���o��
        private int GetPower()
        {
            return attackObject.Power + (int)bullTank.BaseStauts.BasePower;
        }

        private void OnTriggerEnter(Collider other)
        {
            //�U���̃^�C�v�𒲂ׂ�
            if (attackType == AttackType.Succession) { return; }
            //���C���[�`�F�b�N
            if (other.gameObject.layer != 6) { return; }
            ICharacterSetup characterSetup = other.GetComponentInChildren<ICharacterSetup>();
            if (characterSetup == null) { return; }
            IDamageContainer damageContainer = characterSetup.DamageContainer;
            if (damageContainer == null) { return; }
            ShieldController shield = other.GetComponentInChildren<ShieldController>();
            if (shield != null)
            {
                if (shield.IsGuarid(other.transform, thisTransform))
                {
                    return;
                }
            }
            swordEffectHandler.EffectLedger.SetPosAndRotCreate((int)SwordEffectType.Hit, other.ClosestPoint(transform.position), other.transform.rotation);
            seHandler.Play((int)AxeSEType.Damage);
            damageContainer.GiveDamage(GetPower(), attackObject.KnockBack, attackObject.Type, transform, bullTank.CharaType);
        }

        private void OnTriggerStay(Collider other)
        {
            if (attackType == AttackType.Normal) { return; }
            if (other.gameObject.layer != 4) { return; }
            ICharacterSetup characterSetup = other.GetComponentInChildren<ICharacterSetup>();
            if (characterSetup == null) { return; }
            IDamageContainer damageContainer = characterSetup.DamageContainer;
            if (damageContainer == null) { return; }
            swordEffectHandler.EffectLedger.SetPosAndRotCreate((int)SwordEffectType.Hit, other.ClosestPoint(transform.position), other.transform.rotation);
            seHandler.Play((int)AxeSEType.Damage);
            damageContainer.GiveDamage(GetPower(), attackObject.KnockBack, attackObject.Type, transform, bullTank.CharaType);
        }
    }
}

