using System;
using UnityEngine;

namespace MyAssets
{

    public class SwordController : MonoBehaviour
    {
        [SerializeField]
        private AttackObject attackObject;

        [SerializeField]
        private IPlayerAnimator animator;

        [SerializeField]
        private LayerMask hitLayer;

        [SerializeField]
        private float radius = 0.5f;

        [SerializeField]
        private float dis = 1.0f;

        private void Awake()
        {
            attackObject = GetComponent<AttackObject>();

            PlayerController controller = GetComponentInParent<PlayerController>();

            if(controller != null)
            {
                animator = controller.PlayerAnimator;
            }
        }

        public void EnabledCollider(float start,float end,bool all)
        {
            if (all)
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
            else
            {
                AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
                if(animInfo.normalizedTime >= start &&animInfo.normalizedTime <= end)
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

        //�����Ray������
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.up * dis, radius);
        }

    }
}
