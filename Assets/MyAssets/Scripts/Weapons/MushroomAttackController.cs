using MyAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class MushroomAttackController : MonoBehaviour
    {
        [SerializeField]
        private AttackObject attackObject;

        [SerializeField]
        private ISlimeAnimator animator;

        [SerializeField]
        private LayerMask hitLayer;

        [SerializeField]
        private float radius = 0.5f;

        [SerializeField]
        private float dis = 1.0f;

        private new Collider collider;

        private void Awake()
        {
            attackObject = GetComponent<AttackObject>();

            SlimeController controller = GetComponentInParent<SlimeController>();

            if (controller != null)
            {
                animator = controller.SlimeAnimator;
            }

            collider = GetComponent<Collider>();
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
                /*
                Ray ray = new Ray(transform.position, transform.up);
                RaycastHit hit;
                if (Physics.SphereCast(ray, radius, out hit, dis, hitLayer))
                {
                    ICharacterSetup characterSetup = hit.collider.GetComponent<ICharacterSetup>();
                    if (characterSetup == null) { return; }
                    IDamageContainer damageContainer = characterSetup.DamageContainer;
                    if (damageContainer == null) { return; }
                    damageContainer.SetAttackType(attackObject.Type);
                    damageContainer.SetData(attackObject.Power);
                    damageContainer.SetAttacker(transform);
                }
                 */
            }
            else
            {
                AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
                if (animInfo.normalizedTime >= start && animInfo.normalizedTime <= end)
                {
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
                }
            }
        }

        public void NotEnabledCollider()
        {
            collider.enabled = false;
        }

        //ãÖèÛÇÃRayÇâ¬éãâª
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.up * dis, radius);
        }
    }
}
