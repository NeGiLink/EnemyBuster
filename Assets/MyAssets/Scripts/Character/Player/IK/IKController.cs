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

        // 身体の各ボーンの位置は Animator から取れるのでエイリアスを作っておくと便利
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
            // IK の位置から踵・つま先のオフセットを設定
            var heelOffset = Vector3.up * heelOffsetZ;
            var toeOffset = Vector3.up * toeOffsetZ;
            var leftHeelPos = leftFoot.position + heelOffset;
            var leftToePos = leftToe.position + toeOffset;
            var rightHeelPos = rightFoot.position + heelOffset;
            var rightToePos = rightToe.position + toeOffset;

            if (CheckGroundPosition(leftHeelPos, rightHeelPos)) { return; }

            // 足の位置を IK に従って動かす
            var leftIkMoveLength = UpdateFootIk(AvatarIKGoal.LeftFoot, leftHeelPos, leftToePos);
            var rightIkMoveLength = UpdateFootIk(AvatarIKGoal.RightFoot, rightHeelPos, rightToePos);

            // 身体の位置を下げないと IK で移動できないので
            // IK で移動させた差分だけ身体を下げる
            animator.Animator.bodyPosition += Mathf.Max(leftIkMoveLength, rightIkMoveLength) * Vector3.down;
        }

        private bool CheckGroundPosition(Vector3 heelPosL, Vector3 heelPosR)
        {
            // レイを踵から飛ばす（めり込んでた時も平気なようにちょっと上にオフセットさせる）
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
            // レイを踵から飛ばす（めり込んでた時も平気なようにちょっと上にオフセットさせる）
            RaycastHit ray;
            var from = heelPos + Vector3.up * rayLength;
            var to = Vector3.down;
            var length = 2 * rayLength;

            if (Physics.Raycast(from, to, out ray, length))
            {
                // レイが当たった場所を踵の場所にする
                var nextHeelPos = ray.point - Vector3.up * heelOffsetZ;
                var diffHeelPos = (nextHeelPos - heelPos);

                // Animator.SetIKPosition() で IK 位置を動かせるので、
                // 踵の移動分だけ動かす
                // 第１引数は AvatarIKGoal という enum（LeftFoot や RightHand など）
                animator.Animator.SetIKPosition(goal, animator.Animator.GetIKPosition(goal) + diffHeelPos);
                // Animator.SetIKPositionWeight() では IK のブレンド具合を指定できる
                // 本当は 1 固定じゃなくて色々フィルタ掛けると良いと思う
                animator.Animator.SetIKPositionWeight(goal, 1f);

                // 踵からつま先の方向に接地面が上になるように向く姿勢を求めて
                // IK に反映させる
                var rot = GetFootRotation(nextHeelPos, toePos, ray.normal);
                animator.Animator.SetIKRotation(goal, rot);
                animator.Animator.SetIKRotationWeight(goal, 1f);

                // レイを確認用に描画しておくと分かりやすい
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
            // つま先の位置からレイを下に飛ばす
            RaycastHit ray;
            if (Physics.Raycast(toePos, Vector3.down, out ray, 2 * rayLength))
            {
                if (isDrawDebug)
                {
                    Debug.DrawLine(toePos, ray.point, Color.red);
                }
                var nextToePos = ray.point + Vector3.up * toeOffsetZ;
                // つま先方向に接地面の法線を上向きとする傾きを求める
                return Quaternion.LookRotation(nextToePos - heelPos, slopeNormal);
            }
            // レイが当たらなかったらつま先の位置はそのままで接地面方向に回転だけする
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

                    // 内積を計算
                    float dotProduct = Vector3.Dot(worldGuardDirection, ev);

                    // ガード成功判定
                    if (dotProduct > 0.5f)
                    {
                        headTransform.LookAt(fieldOfView.TargetObject.transform);
                    }
                }
            }
        }
    }
}
