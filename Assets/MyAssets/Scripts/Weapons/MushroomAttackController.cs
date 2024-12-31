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
        private IMushroomAnimator animator;

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

            MushroomController controller = GetComponentInParent<MushroomController>();

            if (controller != null)
            {
                animator = controller.MushroomAnimator;
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
            }
            else
            {
                AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
                if (animInfo.normalizedTime >= start && animInfo.normalizedTime <= end)
                {
                    collider.enabled = true;
                }
                else
                {
                    collider.enabled = false;
                }
            }
        }

        public void NotEnabledCollider()
        {
            collider.enabled = false;
        }

        //‹…ó‚ÌRay‚ð‰ÂŽ‹‰»
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.up * dis, radius);
        }
    }
}
