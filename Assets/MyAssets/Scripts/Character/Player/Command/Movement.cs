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
            // �i���̏�Ɍ�������Y����␳���Ĉړ����鏈��
            Vector3 stepUp = new Vector3(0f, stepClimber.MaxStepHeight - (thisTransform.position.y - hitPoint.y), 0f);
            velocity.Rigidbody.MovePosition(thisTransform.position + stepUp * stepClimber.StepSmooth);
        }

        private const float MoveSpeedApproximately = 0.1f;

        public void MoveTo(Vector3 targetPoint, float targetMoveSpeed, float moveSpeedChangeRate, float rotationSpeed, float time)
        {
            //���݂̖ڕW�n�_�Ɍ������x�N�g�������߂�
            Vector3 targetVec = targetPoint - thisTransform.position;
            //�c�ɂ͈ړ����Ȃ��悤�ɂ���
            targetVec.y = 0;
            //�^�[�Q�b�g�����Ɍ�����ς��Ă���
            var targetRotation = Quaternion.LookRotation(targetVec, Vector3.up);
            thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, targetRotation, rotationSpeed * time);

            //�ړ����x�̉����E����
            float speed = targetMoveSpeed;
            float currentSpeed = velocity.CurrentMoveSpeed;
            if (currentSpeed < targetMoveSpeed - MoveSpeedApproximately ||
                currentSpeed > targetMoveSpeed + MoveSpeedApproximately)
            {
                speed = Mathf.Lerp(currentSpeed, targetMoveSpeed, moveSpeedChangeRate * time);
                speed = Mathf.Round(speed * 1000.0f) * 0.001f;
            }

            //�O���Ɍ������Ĉړ�����
            Vector3 currentVelocity = velocity.CurrentVelocity;
            currentVelocity.x = thisTransform.forward.x * speed;
            currentVelocity.z = thisTransform.forward.z * speed;
            velocity.CurrentVelocity = currentVelocity;
            velocity.Rigidbody.velocity = currentVelocity;
        }
    }
}
