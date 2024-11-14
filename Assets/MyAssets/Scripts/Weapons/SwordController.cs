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
        private new Collider collider;

        [SerializeField]
        private LayerMask excludeLayers;

        public string[] motionNames = new string[]
        {
            "FirstAttack",
            "SecondAttack",
            "ThirdAttack",
            "JumpAttack"
        };

        private void Awake()
        {
            attackObject = GetComponent<AttackObject>();

            PlayerController controller = GetComponentInParent<PlayerController>();

            if(controller != null)
            {
                animator = controller.PlayerAnimator;
            }
            
            collider = GetComponentInParent<Collider>();
        }

        private void Start()
        {
            collider.enabled = false;
            collider.excludeLayers = excludeLayers;
        }

        private void Update()
        {
            collider.enabled = false;
            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            foreach(string motionName in motionNames)
            {
                if (animInfo.IsName(motionName))
                {
                    collider.enabled = true;
                }
            }
        }

    }
}
