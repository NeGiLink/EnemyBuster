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

    public class SlimeBodyAttackController : MonoBehaviour
    {
        [SerializeField]
        private Transform thisTransform;

        [SerializeField]
        private AttackObject attackObject;

        [SerializeField]
        private ISlimeAnimator animator;

        private ISlimeSetup setup;

        private SmallEnemyEffectHandller effectHandller;

        private SEHandler seHandler;

        [SerializeField]
        private LayerMask hitLayer;

        //保存用のcenter・radius
        private Vector3 center;

        private float radius;

        private new SphereCollider collider;

        private AttackType attackType = AttackType.Normal;
        public void SetAttackType(AttackType type) { attackType = type; }

        private void Awake()
        {
            attackObject = GetComponent<AttackObject>();

            SlimeController controller = GetComponentInParent<SlimeController>();
            setup = controller.GetComponent<ISlimeSetup>();
            effectHandller = GetComponent<SmallEnemyEffectHandller>();

            seHandler = GetComponent<SEHandler>();

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
            return attackObject.Power + (int)setup.BaseStauts.BasePower;
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
            damageContainer.GiveDamage(GetPower(), attackObject.KnockBack, attackObject.Type, transform,setup.CharaType);
        }
    }
}
