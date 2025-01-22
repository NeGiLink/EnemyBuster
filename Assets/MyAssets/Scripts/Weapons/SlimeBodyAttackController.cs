using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public enum SlimeBodyAttackEffectType
    {
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

        private ISlimeSetup slimeSetup;

        private SmallEnemyEffectHandller effectHandller;

        [SerializeField]
        private LayerMask hitLayer;

        //保存用のcenter・radius
        private Vector3 center;

        private float radius;

        private new SphereCollider collider;

        private AttackType attackType = AttackType.Single;
        public void SetAttackType(AttackType type) { attackType = type; }

        private void Awake()
        {
            attackObject = GetComponent<AttackObject>();

            SlimeController controller = GetComponentInParent<SlimeController>();
            slimeSetup = controller.GetComponent<ISlimeSetup>();
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
            effectHandller.EffectLedger.SetPosAndRotCreate((int)SlimeBodyAttackEffectType.Hit, other.ClosestPoint(transform.position), transform.rotation);
            damageContainer.GiveDamage(attackObject.Power, attackObject.KnockBack, attackObject.Type, transform,slimeSetup.CharaType);
        }
    }
}
