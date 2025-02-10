using UnityEngine;

namespace MyAssets
{
    [System.Serializable]

    public class Climb : IClimb,ICharacterComponent<IPlayerSetup>
    {
        private Transform thisTransform;

        private IVelocityComponent velocity;

        //崖を登る時にプレイヤーを前に進ませるためのスピード変数
        [SerializeField]
        private float climbForward = 1.0f;
        //上記の上方向へのスピード変数
        [SerializeField]
        private float climbUp = 1.0f;

        //登る時の開始地点を保持する変数
        [SerializeField]
        private Vector3 climbOldPos = Vector3.zero;
        //登る時のゴール地点を保持する変数
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
            //  開始位置を保持
            climbOldPos = thisTransform.position;
            //  終了位置を算出
            climbPos = thisTransform.position + thisTransform.forward * climbForward + Vector3.up * climbUp;
        }

        public void DoClimb()
        {
            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            if(!animInfo.IsName("Braced Hang To Crouch")) { return; }
            float f = animInfo.normalizedTime;
            //  左右は後半にかけて早く移動する
            float x = Mathf.Lerp(climbOldPos.x, climbPos.x, Ease(f));
            float z = Mathf.Lerp(climbOldPos.z, climbPos.z, Ease(f));
            //  上下は等速直線で移動
            float y = Mathf.Lerp(climbOldPos.y, climbPos.y, f);
            //  座標を更新
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

        //  イージング関数
        private float Ease(float x)
        {
            return x * x * x;
        }
    }
}
