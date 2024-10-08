using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class Movement : IMovement,IPlayerComponent
    {
        private Transform thisTransform;

        private IVelocityComponent velocity;

        public void DoSetup(IPlayerSetup player)
        {
            thisTransform = player.gameObject.transform;
            velocity = player.Velocity;
        }

        public void Move(float maxSpeed)
        {
            var moveVelocity = velocity.CurrentVelocity;
            moveVelocity = moveVelocity * maxSpeed;
            velocity.Rigidbody.velocity = new Vector3(moveVelocity.x, velocity.Rigidbody.velocity.y, moveVelocity.z);
        }
    }
}
