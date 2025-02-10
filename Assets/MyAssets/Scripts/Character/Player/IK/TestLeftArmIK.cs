using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class TestLeftArmIK : MonoBehaviour
    {
        [SerializeField]
        private IPlayerAnimator animator;
        [SerializeField]
        private Transform leftHandTarget; // �����\����^�[�Q�b�g�|�W�V����

        [SerializeField]
        private float p;
        [SerializeField]
        private float r;

        [SerializeField]
        private Vector3 pos;
        [SerializeField]
        private Quaternion qua;
        private void Awake()
        {
            IPlayerSetup actor = GetComponent<IPlayerSetup>();
            animator = actor.PlayerAnimator;
        }
        // Start is called before the first frame update
        void Start()
        {
            //animator = GetComponent<IPlayerAnimator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            // IK�̃E�F�C�g�i�e���x�j��ݒ�
            if (leftHandTarget != null)
            {
                animator.Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, p);
                animator.Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, r);

                // ������^�[�Q�b�g�̈ʒu�Ɉړ�
                animator.Animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position+pos);
                animator.Animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
            }
        }
    }
}
