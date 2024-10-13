using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class StepClimberJudgment : IStepClimberJudgment,IPlayerComponent
    {
        [SerializeField]
        private float maxStepCheck = 0.25f;
        [SerializeField]
        private float maxStepHeight = 0.5f;
        public float MaxStepHeight => maxStepHeight;
        [SerializeField]
        private float stepSmooth = 0.7f;
        public float StepSmooth => stepSmooth;
        [SerializeField]
        private float maxSlopeAngle = 90.0f;
        [SerializeField]
        private LayerMask groundMask;

        private IVelocityComponent velocity;

        private Transform thisTransform;

        private Vector3 stepGolePosition;

        public Vector3 StepGolePosition => stepGolePosition;

        public void DoSetup(IPlayerSetup player)
        {
            velocity = player.Velocity;
            thisTransform = player.gameObject.transform;
        }

        public void HandleStepClimbing()
        {
            //キャラクターの前方にレイキャストを飛ばす
            RaycastHit hitLower;
            Vector3 rayStart = thisTransform.position + Vector3.up * maxStepCheck;
            Ray ray1 = new Ray(rayStart, thisTransform.forward * 0.5f);
            if(Physics.Raycast(ray1,out hitLower, 0.5f, groundMask))
            {
                Vector3 surfaceNormal = hitLower.normal;
                float angle = Vector3.Angle(surfaceNormal, Vector3.up);
                if (angle < maxSlopeAngle) { return; }
                //足元近くのレイキャストがヒットした時、その上を調べる
                RaycastHit hitUpper;
                Vector3 rayUpperStart = thisTransform.position + Vector3.up * maxStepHeight;
                Ray ray2 = new Ray(rayUpperStart, thisTransform.forward * 0.5f);
                if (!Physics.Raycast(ray2, out hitUpper, 0.5f, groundMask))
                {
                    // 段差が乗り越えられる高さであれば、その上に移動
                    stepGolePosition = hitLower.point;
                }
                else
                {
                    stepGolePosition = Vector3.zero;
                }
            }
            else
            {
                stepGolePosition = Vector3.zero;
            }
            Debug.DrawRay(ray1.origin,ray1.direction * 0.5f,Color.blue);
        }
    }
}
