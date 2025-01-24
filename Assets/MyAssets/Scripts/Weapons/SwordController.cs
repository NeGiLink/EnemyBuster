using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class SwordController : MonoBehaviour
    {
        [SerializeField]
        private AttackObject attackObject;

        [SerializeField]
        private IPlayerAnimator animator;

        private IPlayerSetup    playerSetup;

        [SerializeField]
        private new CapsuleCollider collider;

        [SerializeField]
        private SEHandler seHandler;

        //保存用のcenter・radius・height
        private Vector3 center;

        private float radius;

        private float height;

        private List<IDamageContainer> damagers = new List<IDamageContainer>();

        private AttackType attackType = AttackType.Single;
        public void SetAttackType(AttackType type) {  attackType = type; }

        private SwordEffectHandler swordEffectHandler;

        private void Awake()
        {
            attackObject = GetComponent<AttackObject>();

            seHandler = GetComponent<SEHandler>();

            PlayerController controller = GetComponentInParent<PlayerController>();

            if(controller != null)
            {
                playerSetup = controller.GetComponent<IPlayerSetup>();
                animator = controller.PlayerAnimator;
            }

            collider = GetComponent<CapsuleCollider>();

            swordEffectHandler = GetComponent<SwordEffectHandler>();

            center = collider.center;
            radius = collider.radius;
            height = collider.height;
        }

        private void NoActivateCollider()
        {
            collider.center = Vector3.zero;
            collider.radius = 0.0f;
            collider.height = 0.0f;
        }

        private void ActivateCollider()
        {
            collider.center = center;
            collider.radius = radius;
            collider.height = height;
        }

        public void Slash()
        {
            seHandler.Play(0);
        }

        public void SpinSlash()
        {
            seHandler.OnPlay(1);
        }

        private void Start()
        {
            collider.enabled = false;
        }

        public void EnabledCollider(float start,float end,bool all)
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
                if(animInfo.normalizedTime >= start &&animInfo.normalizedTime <= end)
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

        private void OnTriggerEnter(Collider other)
        {
            if(attackType == AttackType.Succession) { return; }
            if (other.gameObject.layer != 8) { return; }
            ICharacterSetup characterSetup = other.GetComponentInChildren<ICharacterSetup>();
            if (characterSetup == null) { return; }
            IDamageContainer damageContainer = characterSetup.DamageContainer;
            if (damageContainer == null) { return; }
            swordEffectHandler.EffectLedger.SetPosAndRotCreate((int)SwordEffectType.Hit, other.ClosestPoint(transform.position), other.transform.rotation);
            seHandler.Play(2);
            //基礎ダメージと武器のダメージ分
            int damage = attackObject.Power + (int)playerSetup.Stauts.BasePower;
            damageContainer.GiveDamage(damage, attackObject.KnockBack, attackObject.Type, transform,playerSetup.CharaType);
        }

        private void OnTriggerStay(Collider other)
        {
            if(attackType == AttackType.Single) { return; }
            if (other.gameObject.layer != 8) { return; }
            ICharacterSetup characterSetup = other.GetComponentInChildren<ICharacterSetup>();
            if (characterSetup == null) { return; }
            IDamageContainer damageContainer = characterSetup.DamageContainer;
            if (damageContainer == null){return;}
            swordEffectHandler.EffectLedger.SetPosAndRotCreate((int)SwordEffectType.Hit, other.ClosestPoint(transform.position),other.transform.rotation);
            seHandler.Play(2);
            int damage = attackObject.Power + (int)playerSetup.Stauts.BasePower;
            damageContainer.GiveDamage(damage, attackObject.KnockBack, attackObject.Type, transform, playerSetup.CharaType);
        }

    }
}
