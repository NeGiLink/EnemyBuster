using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class StepClimberJudgment : IStepClimberJudgment,IPlayerComponent
    {
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

        // �l�͈̔�
        private const float MinValue = 0.0f;
        private const float MaxValue = 0.25f;

        // �����i�l���������鑬���𒲐��j
        public float speed = 1.0f;

        // ���݂̒l�i�͈͓��ő�������l�j
        private float currentValue;


        public void HandleStepClimbing()
        {
            currentValue = Mathf.PingPong(Time.time * speed, MaxValue - MinValue) + MinValue;
            //�L�����N�^�[�̑O���Ƀ��C�L���X�g���΂�
            RaycastHit hitLower;
            Vector3 rayStart = thisTransform.position + Vector3.up * currentValue;
            Ray ray1 = new Ray(rayStart, thisTransform.forward * 0.5f);
            if(Physics.Raycast(ray1,out hitLower, 0.5f, groundMask))
            {
                Vector3 surfaceNormal = hitLower.normal;
                float angle = Vector3.Angle(surfaceNormal, Vector3.up);
                if (angle < maxSlopeAngle) { return; }
                //�����߂��̃��C�L���X�g���q�b�g�������A���̏�𒲂ׂ�
                RaycastHit hitUpper;
                Vector3 rayUpperStart = thisTransform.position + Vector3.up * maxStepHeight;
                Ray ray2 = new Ray(rayUpperStart, thisTransform.forward * 0.5f);
                if (!Physics.Raycast(ray2, out hitUpper, 0.5f, groundMask))
                {
                    // �i�������z�����鍂���ł���΁A���̏�Ɉړ�
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
            Debug.DrawRay(ray1.origin,ray1.direction * 0.5f, Color.blue);
        }
    }
}
