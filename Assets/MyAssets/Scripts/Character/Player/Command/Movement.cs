using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class Movement : ICharacterMovement,ICharacterComponent
    {
        private Transform thisTransform;

        private IVelocityComponent velocity;

        private Vector3 keepVelocity;

        private IStepClimberJudgment stepClimber;

        public void DoSetup(ICharacterSetup chara)
        {
            thisTransform = chara.gameObject.transform;
            velocity = chara.Velocity;
            stepClimber = chara.StepClimberJudgment;
        }

        public void Move(float maxSpeed)
        {
            var moveVelocity = velocity.CurrentVelocity;
            moveVelocity = moveVelocity * maxSpeed;
            velocity.Rigidbody.velocity = new Vector3(moveVelocity.x, velocity.Rigidbody.velocity.y, moveVelocity.z);
            keepVelocity = velocity.CurrentVelocity;
        }
        public void ForwardMove(float maxSpeed)
        {
            var moveVelocity = velocity.CurrentVelocity;
            moveVelocity = moveVelocity * maxSpeed;
            velocity.Rigidbody.velocity = new Vector3(moveVelocity.x, velocity.Rigidbody.velocity.y, moveVelocity.z);
            keepVelocity = velocity.CurrentVelocity;
        }

        public void ForwardLerpMove(Vector3 basePos, float dis)
        {
            thisTransform.position = Vector3.Lerp(basePos, basePos + thisTransform.forward * dis, Time.deltaTime * 5.0f);
        }

        public void StartClimbStep(Vector3 hitPoint)
        {
            if (hitPoint == Vector3.zero) { return; }
            // 段差の上に向かってY軸を補正して移動する処理
            Vector3 stepUp = new Vector3(0f, stepClimber.MaxStepHeight - (thisTransform.position.y - hitPoint.y), 0f);
            velocity.Rigidbody.MovePosition(thisTransform.position + stepUp * stepClimber.StepSmooth);
        }

        private const float MoveSpeedApproximately = 0.1f;

        public void MoveTo(Vector3 targetPoint, float targetMoveSpeed, float moveSpeedChangeRate, float rotationSpeed, float time)
        {
            //現在の目標地点に向かうベクトルを求める
            Vector3 targetVec = targetPoint - thisTransform.position;
            //縦には移動しないようにする
            targetVec.y = 0;
            //ターゲット方向に向きを変えていく
            var targetRotation = Quaternion.LookRotation(targetVec, Vector3.up);
            thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, targetRotation, rotationSpeed * time);

            //移動速度の加速・減速
            float speed = targetMoveSpeed;
            float currentSpeed = velocity.CurrentMoveSpeed;
            if (currentSpeed < targetMoveSpeed - MoveSpeedApproximately ||
                currentSpeed > targetMoveSpeed + MoveSpeedApproximately)
            {
                speed = Mathf.Lerp(currentSpeed, targetMoveSpeed, moveSpeedChangeRate * time);
                speed = Mathf.Round(speed * 1000.0f) * 0.001f;
            }

            //前方に向かって移動する
            Vector3 currentVelocity = velocity.CurrentVelocity;
            currentVelocity.x = thisTransform.forward.x * speed;
            currentVelocity.z = thisTransform.forward.z * speed;
            velocity.CurrentVelocity = currentVelocity;
            velocity.Rigidbody.velocity = currentVelocity;
        }
    }
}
