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

        [SerializeField]
        private new CapsuleCollider collider;

        //ï€ë∂ópÇÃcenterÅEradiusÅEheight
        private Vector3 center;

        private float radius;

        private float height;

        private List<IDamageContainer> damagers = new List<IDamageContainer>();

        private AttackType attackType = AttackType.Single;
        public void SetAttackType(AttackType type) {  attackType = type; }

        private void Awake()
        {
            attackObject = GetComponent<AttackObject>();

            PlayerController controller = GetComponentInParent<PlayerController>();

            if(controller != null)
            {
                animator = controller.PlayerAnimator;
            }

            collider = GetComponent<CapsuleCollider>();

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

        private void Start()
        {
            collider.enabled = false;
        }

        public void EnabledCollider(float start,float end,bool all)
        {
            if (all)
            {
                collider.enabled = true;
                ActivateCollider();
            }
            else
            {
                AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
                if(animInfo.normalizedTime >= start &&animInfo.normalizedTime <= end)
                {
                    collider.enabled = true;
                    ActivateCollider();
                    /*
                    Ray ray = new Ray(transform.position, transform.up);
                    RaycastHit hit;
                    if (Physics.SphereCast(ray, radius, out hit, dis, hitLayer))
                    {
                        ICharacterSetup characterSetup = hit.collider.GetComponent<ICharacterSetup>();
                        if (characterSetup == null) { return; }
                        IDamageContainer damageContainer = characterSetup.DamageContainer;
                        if (damageContainer == null) { return; }
                        damageContainer.SetAttackerData(attackObject.Power, attackObject.Type, transform);
                    }
                     */
                }
                else
                {
                    NoActivateCollider();
                }
            }
        }
        public void NotEnabledCollider()
        {
            NoActivateCollider();
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
            damageContainer.GiveYouDamage(attackObject.Power, attackObject.Type, transform);
        }

        private void OnTriggerStay(Collider other)
        {
            if(attackType == AttackType.Single) { return; }
            if (other.gameObject.layer != 8) { return; }
            ICharacterSetup characterSetup = other.GetComponentInChildren<ICharacterSetup>();
            if (characterSetup == null) { return; }
            IDamageContainer damageContainer = characterSetup.DamageContainer;
            if (damageContainer == null) { return; }
            damageContainer.GiveYouDamage(attackObject.Power, attackObject.Type, transform);
        }

    }
}
