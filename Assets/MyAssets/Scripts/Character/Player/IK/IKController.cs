using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class IKController : IAllIK, ICharacterComponent<IPlayerSetup>
    {

        [SerializeField]
        private FootIK footIK;

        [SerializeField]
        private HeadIK headIK;

        public void DoSetup(IPlayerSetup player)
        {
            footIK.DoSetup(player);
            headIK.DoSetup(player);
        }

        public void DoFootIKUpdate()
        {
            footIK.DoUpdate();
        }

        public void DoHeadIKUpdate()
        {
            headIK.DoUpdate();
        }
    }

    [System.Serializable]
    public class FootIK
    {
        private IPlayerAnimator animator;

        [SerializeField]
        private bool ikEnabled;

        public bool isDrawDebug = false;
        [SerializeField]
        private float heelOffsetZ = -0.08f;
        [SerializeField]
        private float toeOffsetZ = 0.05f;
        [SerializeField]
        private float rayLength = 0.1f;

        // �g�̂̊e�{�[���̈ʒu�� Animator �������̂ŃG�C���A�X������Ă����ƕ֗�
        private Transform leftFoot { get { return animator.Animator.GetBoneTransform(HumanBodyBones.LeftFoot); } }
        private Transform rightFoot { get { return animator.Animator.GetBoneTransform(HumanBodyBones.RightFoot); } }
        private Transform leftToe { get { return animator.Animator.GetBoneTransform(HumanBodyBones.LeftToes); } }
        private Transform rightToe { get { return animator.Animator.GetBoneTransform(HumanBodyBones.RightToes); } }

        public void DoSetup(IPlayerSetup player)
        {
            animator = player.PlayerAnimator;
        }

        public void DoUpdate()
        {
            if (!ikEnabled) { return; }
            // IK �̈ʒu�������E�ܐ�̃I�t�Z�b�g��ݒ�
            var heelOffset = Vector3.up * heelOffsetZ;
            var toeOffset = Vector3.up * toeOffsetZ;
            var leftHeelPos = leftFoot.position + heelOffset;
            var leftToePos = leftToe.position + toeOffset;
            var rightHeelPos = rightFoot.position + heelOffset;
            var rightToePos = rightToe.position + toeOffset;

            if (CheckGroundPosition(leftHeelPos, rightHeelPos)) { return; }

            // ���̈ʒu�� IK �ɏ]���ē�����
            var leftIkMoveLength = UpdateFootIk(AvatarIKGoal.LeftFoot, leftHeelPos, leftToePos);
            var rightIkMoveLength = UpdateFootIk(AvatarIKGoal.RightFoot, rightHeelPos, rightToePos);

            // �g�̂̈ʒu�������Ȃ��� IK �ňړ��ł��Ȃ��̂�
            // IK �ňړ����������������g�̂�������
            animator.Animator.bodyPosition += Mathf.Max(leftIkMoveLength, rightIkMoveLength) * Vector3.down;
        }

        private bool CheckGroundPosition(Vector3 heelPosL, Vector3 heelPosR)
        {
            // ���C���������΂��i�߂荞��ł��������C�Ȃ悤�ɂ�����Ə�ɃI�t�Z�b�g������j
            RaycastHit rayHitR;
            var fromR = heelPosR + Vector3.up * rayLength;
            var toR = Vector3.down;
            var lengthR = 2 * rayLength;


            RaycastHit rayHitL;
            var fromL = heelPosL + Vector3.up * rayLength;
            var toL = Vector3.down;
            var lengthL = 2 * rayLength;


            Ray rayR = new Ray(fromR, toR);
            Ray rayL = new Ray(fromL, toL);

            if (Physics.Raycast(rayR, out rayHitR, lengthR) && Physics.Raycast(rayL, out rayHitL, lengthL))
            {
                Vector3 sub = rayHitR.point - rayHitL.point;
                if (sub.magnitude < 0.5f)
                {
                    return true;
                }
            }
            Debug.DrawRay(rayR.origin, rayR.direction * lengthR, Color.blue);
            Debug.DrawRay(rayL.origin, rayL.direction * lengthL, Color.gray);

            return false;
        }


        private float UpdateFootIk(AvatarIKGoal goal, Vector3 heelPos, Vector3 toePos)
        {
            // ���C���������΂��i�߂荞��ł��������C�Ȃ悤�ɂ�����Ə�ɃI�t�Z�b�g������j
            RaycastHit ray;
            var from = heelPos + Vector3.up * rayLength;
            var to = Vector3.down;
            var length = 2 * rayLength;

            if (Physics.Raycast(from, to, out ray, length))
            {
                // ���C�����������ꏊ�����̏ꏊ�ɂ���
                var nextHeelPos = ray.point - Vector3.up * heelOffsetZ;
                var diffHeelPos = (nextHeelPos - heelPos);

                // Animator.SetIKPosition() �� IK �ʒu�𓮂�����̂ŁA
                // ���̈ړ�������������
                // ��P������ AvatarIKGoal �Ƃ��� enum�iLeftFoot �� RightHand �Ȃǁj
                animator.Animator.SetIKPosition(goal, animator.Animator.GetIKPosition(goal) + diffHeelPos);
                // Animator.SetIKPositionWeight() �ł� IK �̃u�����h����w��ł���
                // �{���� 1 �Œ肶��Ȃ��ĐF�X�t�B���^�|����Ɨǂ��Ǝv��
                animator.Animator.SetIKPositionWeight(goal, 1f);

                // ������ܐ�̕����ɐڒn�ʂ���ɂȂ�悤�Ɍ����p�������߂�
                // IK �ɔ��f������
                var rot = GetFootRotation(nextHeelPos, toePos, ray.normal);
                animator.Animator.SetIKRotation(goal, rot);
                animator.Animator.SetIKRotationWeight(goal, 1f);

                // ���C���m�F�p�ɕ`�悵�Ă����ƕ�����₷��
                if (isDrawDebug)
                {
                    Debug.DrawLine(heelPos, ray.point, Color.red);
                    Debug.DrawRay(nextHeelPos, rot * Vector3.forward, Color.blue);
                }

                return diffHeelPos.magnitude;
            }

            return 0f;
        }


        private Quaternion GetFootRotation(Vector3 heelPos, Vector3 toePos, Vector3 slopeNormal)
        {
            // �ܐ�̈ʒu���烌�C�����ɔ�΂�
            RaycastHit ray;
            if (Physics.Raycast(toePos, Vector3.down, out ray, 2 * rayLength))
            {
                if (isDrawDebug)
                {
                    Debug.DrawLine(toePos, ray.point, Color.red);
                }
                var nextToePos = ray.point + Vector3.up * toeOffsetZ;
                // �ܐ�����ɐڒn�ʂ̖@����������Ƃ���X�������߂�
                return Quaternion.LookRotation(nextToePos - heelPos, slopeNormal);
            }
            // ���C��������Ȃ�������ܐ�̈ʒu�͂��̂܂܂Őڒn�ʕ����ɉ�]��������
            return Quaternion.LookRotation(toePos - heelPos, slopeNormal);
        }
    }

    [System.Serializable]
    public class HeadIK : ICharacterComponent<IPlayerSetup>
    {
        [SerializeField]
        private Transform headTransform;

        [SerializeField]
        private FieldOfView fieldOfView;

        private Transform transform;
        public void DoSetup(IPlayerSetup player)
        {
            fieldOfView = player.gameObject.GetComponent<FieldOfView>();
            transform = player.gameObject.transform;
        }

        private Vector3 focusDirection = Vector3.forward;

        public void DoUpdate()
        {
            if (headTransform != null)
            {
                if (fieldOfView.FindTarget)
                {
                    Vector3 ev = fieldOfView.TargetObject.transform.position - headTransform.position;
                    ev.Normalize();
                    Vector3 worldGuardDirection = transform.transform.TransformDirection(focusDirection).normalized;

                    // ���ς��v�Z
                    float dotProduct = Vector3.Dot(worldGuardDirection, ev);

                    // �K�[�h��������
                    if (dotProduct > 0.5f)
                    {
                        headTransform.LookAt(fieldOfView.TargetObject.transform);
                    }
                }
            }
        }
    }
}
