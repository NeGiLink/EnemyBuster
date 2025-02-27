using UnityEngine;

namespace MyAssets
{
    public enum SlimeBodyAttackEffectType
    {
        Hit
    }
    public enum SlimeBodyAttackSEType
    {
        Attack,
        Hit
    }
    /*
     * スライムの武器(体)のクラス
     * 表示・非表示やコライダーの設定、当たり判定の区別を行っている
     */
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(SmallEnemyEffectHandller))]
    public class SlimeBodyAttackController : BaseAttackController
    {
        [SerializeField]
        private Transform                   thisTransform;

        [SerializeField]
        private ISlimeAnimator              animator;

        private ISlimeSetup                 slime;

        private SmallEnemyEffectHandller    effectHandller;

        //保存用のcenter・radius
        private Vector3                     center;

        private float                       radius;

        private new SphereCollider          collider;

        public void SetAttackType(AttackType type) { attackType = type; }

        protected override void Awake()
        {
            base.Awake();

            SlimeController controller = GetComponentInParent<SlimeController>();
            slime = controller.GetComponent<ISlimeSetup>();
            effectHandller = GetComponent<SmallEnemyEffectHandller>();

            if (controller != null)
            {
                animator = controller.SlimeAnimator;
            }

            collider = GetComponent<SphereCollider>();
        }
        private void Start()
        {
            collider.enabled = false;

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

        public void EnabledCollider(float start, float end, bool all)
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

        public void NotEnabledCollider()
        {
            NoActivateCollider();
            collider.enabled = false;
            attackType = AttackType.Null;
        }

        public void NormalAttackSE()
        {
            seHandler.Play((int)SlimeBodyAttackSEType.Attack);
        }

        private int GetPower()
        {
            return attackObject.Power + (int)slime.BaseStatus.BasePower;
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
            ShieldController shield = other.GetComponentInChildren<ShieldController>();
            if (shield != null)
            {
                if (shield.IsGuarid(other.transform, thisTransform))
                {
                    return;
                }
            }
            seHandler.Play((int)SlimeBodyAttackSEType.Hit);
            effectHandller.EffectLedger.SetPosAndRotCreate((int)SlimeBodyAttackEffectType.Hit, other.ClosestPoint(transform.position), transform.rotation);
            damageContainer.GiveDamage(GetPower(), attackObject.KnockBack, attackObject.Type, transform,slime.CharaType);
        }
    }
}
