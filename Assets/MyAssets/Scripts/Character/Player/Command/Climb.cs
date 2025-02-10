using UnityEngine;

namespace MyAssets
{
    [System.Serializable]

    public class Climb : IClimb,ICharacterComponent<IPlayerSetup>
    {
        private Transform thisTransform;

        private IVelocityComponent velocity;

        //�R��o�鎞�Ƀv���C���[��O�ɐi�܂��邽�߂̃X�s�[�h�ϐ�
        [SerializeField]
        private float climbForward = 1.0f;
        //��L�̏�����ւ̃X�s�[�h�ϐ�
        [SerializeField]
        private float climbUp = 1.0f;

        //�o�鎞�̊J�n�n�_��ێ�����ϐ�
        [SerializeField]
        private Vector3 climbOldPos = Vector3.zero;
        //�o�鎞�̃S�[���n�_��ێ�����ϐ�
        [SerializeField]
        private Vector3 climbPos = Vector3.zero;

        [SerializeField]
        private bool climbEnd = false;
        public bool IsClimbEnd => climbEnd;

        private IPlayerAnimator animator;
        public void DoSetup(IPlayerSetup actor)
        {
            thisTransform = actor.gameObject.transform;
            velocity = actor.Velocity;
            animator = actor.PlayerAnimator;
        }

        public void DoClimbStart()
        {
            velocity.Rigidbody.useGravity = false;
            velocity.Rigidbody.velocity = Vector3.zero;
            climbEnd = false;
            SetClimbPostion();
        }

        private void SetClimbPostion()
        {
            //  �J�n�ʒu��ێ�
            climbOldPos = thisTransform.position;
            //  �I���ʒu���Z�o
            climbPos = thisTransform.position + thisTransform.forward * climbForward + Vector3.up * climbUp;
        }

        public void DoClimb()
        {
            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            if(!animInfo.IsName("Braced Hang To Crouch")) { return; }
            float f = animInfo.normalizedTime;
            //  ���E�͌㔼�ɂ����đ����ړ�����
            float x = Mathf.Lerp(climbOldPos.x, climbPos.x, Ease(f));
            float z = Mathf.Lerp(climbOldPos.z, climbPos.z, Ease(f));
            //  �㉺�͓��������ňړ�
            float y = Mathf.Lerp(climbOldPos.y, climbPos.y, f);
            //  ���W���X�V
            thisTransform.position = new Vector3(x, y, z);
            if(f > 0.9f)
            {
                climbEnd = true;
            }
        }

        public void DoClimbExit()
        {
            velocity.Rigidbody.useGravity = true;
        }

        //  �C�[�W���O�֐�
        private float Ease(float x)
        {
            return x * x * x;
        }
    }
}
