using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public enum GolemFistSEType
    {
        Attack,
        Stamp,
        Hit
    }

    [RequireComponent(typeof(SphereCollider))]
    public class GolemFistController : MonoBehaviour
    {
        [SerializeField]
        private Transform thisTransform;

        [SerializeField]
        private AttackObject attackObject;

        [SerializeField]
        private IGolemAnimator animator;

        private IGolemSetup setup;

        [SerializeField]
        private new SphereCollider collider;

        //保存用のcenter・radius
        private Vector3 center;

        private float radius;

        private List<IDamageContainer> damagers = new List<IDamageContainer>();

        private AttackType attackType = AttackType.Normal;
        public void SetAttackType(AttackType type) { attackType = type; }

        private SwordEffectHandler swordEffectHandler;

        private SEHandler seHandler;

        private void Awake()
        {
            attackObject = GetComponent<AttackObject>();

            GolemController controller = GetComponentInParent<GolemController>();

            if (controller != null)
            {
                setup = controller.GetComponent<IGolemSetup>();
                animator = controller.GolemAnimator;
            }

            collider = GetComponent<SphereCollider>();

            swordEffectHandler = GetComponent<SwordEffectHandler>();

            seHandler = GetComponent<SEHandler>();

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

        public void AttackSE()
        {
            seHandler.Play((int)GolemFistSEType.Attack);
        }

        public void StampSE()
        {
            seHandler.Play((int)GolemFistSEType.Stamp);
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
            seHandler.Play((int)GolemFistSEType.Hit);
            damageContainer.GiveDamage(GetPower(), attackObject.KnockBack, attackObject.Type, transform, setup.CharaType);
        }

        private void OnTriggerStay(Collider other)
        {
            if (attackType == AttackType.Normal) { return; }
            if (other.gameObject.layer != 4) { return; }
            ICharacterSetup characterSetup = other.GetComponentInChildren<ICharacterSetup>();
            if (characterSetup == null) { return; }
            IDamageContainer damageContainer = characterSetup.DamageContainer;
            if (damageContainer == null) { return; }
            seHandler.Play((int)GolemFistSEType.Hit);
            damageContainer.GiveDamage(GetPower(), attackObject.KnockBack, attackObject.Type, transform, setup.CharaType);
        }
    }
}
