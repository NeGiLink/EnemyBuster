using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class Movement : IMovement,IPlayerComponent
    {
        private Transform thisTransform;

        private IVelocityComponent velocity;

        private Vector3 keepVelocity;

        private IStepClimberJudgment stepClimber;

        public void DoSetup(IPlayerSetup player)
        {
            thisTransform = player.gameObject.transform;
            velocity = player.Velocity;
            stepClimber = player.StepClimberJudgment;
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
            // íiç∑ÇÃè„Ç…å¸Ç©Ç¡ÇƒYé≤Çï‚ê≥ÇµÇƒà⁄ìÆÇ∑ÇÈèàóù
            Vector3 stepUp = new Vector3(0f, stepClimber.MaxStepHeight - (thisTransform.position.y - hitPoint.y), 0f);
            velocity.Rigidbody.MovePosition(thisTransform.position + stepUp * stepClimber.StepSmooth);
        }
    }
}
