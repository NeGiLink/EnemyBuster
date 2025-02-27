using UnityEngine;

namespace MyAssets
{
    public enum BullTankHeadAttackEffectType
    {
        Hit
    }
    /*
     * ブルタンクの武器2(頭)のクラス
     * 表示・非表示やコライダーの設定、当たり判定の区別を行っている
     */
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(SmallEnemyEffectHandller))]
    public class BullTankHeadAttackController : BaseAttackController
    {
        [SerializeField]
        private Transform                   thisTransform;

        [SerializeField]
        private IBullTankAnimator           animator;

        private SmallEnemyEffectHandller    effectHandller;

        private new SphereCollider          collider;

        //保存用のcenter・radius
        private Vector3                     center;

        private float                       radius;

        private IBullTankSetup              bullTank;

        public void SetAttackType(AttackType type) { attackType = type; }


        protected override void Awake()
        {
            base.Awake();

            bullTank = GetComponentInParent<IBullTankSetup>();

            effectHandller = GetComponent<SmallEnemyEffectHandller>();

            if (bullTank != null)
            {
                animator = bullTank.BullTankAnimator;
            }

            collider = GetComponent<SphereCollider>();
        }

        private void Start()
        {
            collider.enabled = false;

            center = collider.center;
            radius = collider.radius;
            NoActivateCollider();
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

        public void EnabledCollider(float start, float end, bool all)
        {
            if (all)
            {
                collider.enabled = true;
                ActivateCollider();
            }
            else
            {
                AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
                if (animInfo.normalizedTime >= start && animInfo.normalizedTime <= end)
                {
                    collider.enabled = true;
                    ActivateCollider();
                }
                else
                {
                    NoActivateCollider();
                    collider.enabled = false;
                }
            }
        }

        public void NotEnabledCollider()
        {
            NoActivateCollider();
            collider.enabled = false;
        }

        private int GetPower()
        {
            return attackObject.Power + (int)bullTank.BaseStatus.BasePower;
        }

        private void OnTriggerEnter(Collider other)
        {
            //攻撃のタイプを調べる
            if (attackType == AttackType.Succession) { return; }
            //レイヤーチェック
            if (other.gameObject.layer != 6) { return; }
            ICharacterSetup characterSetup = other.GetComponentInChildren<ICharacterSetup>();
            if (characterSetup == null) { return; }
            IDamageContainer damageContainer = characterSetup.DamageContainer;
            if (damageContainer == null) { return; }
            int power = GetPower();
            ShieldController shield = other.GetComponentInChildren<ShieldController>();
            if (shield != null)
            {
                if (shield.IsGuarid(other.transform, thisTransform))
                {
                    power = 0;
                }
            }
            effectHandller.EffectLedger.SetPosAndRotCreate((int)BullTankHeadAttackEffectType.Hit, other.ClosestPoint(transform.position), transform.rotation);
            damageContainer.SetActivateKnockback(true);
            damageContainer.GiveDamage(power, attackObject.KnockBack, attackObject.Type, transform, bullTank.CharaType);
        }
    }
}
