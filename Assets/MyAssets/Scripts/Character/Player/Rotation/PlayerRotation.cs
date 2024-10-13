using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class PlayerRotation : ICharacterRotation,IPlayerComponent
    {
        [SerializeField]
        private Transform thisTransform;
        [SerializeField]
        private Quaternion targetRotation;

        private IMoveInputProvider moveInput;

        private IVelocityComponent velocity;

        public void DoSetup(IPlayerSetup player)
        {
            thisTransform = player.gameObject.transform;
            targetRotation = thisTransform.rotation;
            velocity = player.Velocity;
            moveInput = player.MoveInput;
        }

        public void DoUpdate()
        {
            var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
            if (moveInput.Horizontal != 0 && moveInput.Vertical != 0)
            {
                velocity.CurrentVelocity = horizontalRotation * new Vector3(moveInput.Horizontal, 0, moveInput.Vertical).normalized;
            }
            else
            {
                velocity.CurrentVelocity = horizontalRotation * new Vector3(moveInput.Horizontal, 0, moveInput.Vertical);
            }
        }

        public void DoFixedUpdate(Vector3 velocity)
        {
            float rotationSpeed = 0;
            switch (PlayerInput.GetDeviceInput())
            {
                case DeviceInput.Key:
                    rotationSpeed = 600 * Time.deltaTime;
                    if (velocity.magnitude > 0.5f)
                    {
                        targetRotation = Quaternion.LookRotation(velocity, Vector3.up);
                    }
                    break;
                case DeviceInput.Controller:
                    rotationSpeed = 1200 * Time.deltaTime;
                    if (velocity.magnitude > 0.1f)
                    {
                        targetRotation = Quaternion.LookRotation(velocity, Vector3.up);
                    }
                    break;
            }
            thisTransform.rotation = Quaternion.RotateTowards(thisTransform.rotation, targetRotation, rotationSpeed);
        }
    }
}
