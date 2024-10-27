using UnityEngine;
using UnityEngine.InputSystem.XR;

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

        private IFocusInputProvider focusInput;

        private FieldOfView fieldOfView;

        public void DoSetup(IPlayerSetup player)
        {
            thisTransform = player.gameObject.transform;
            targetRotation = thisTransform.rotation;
            velocity = player.Velocity;
            moveInput = player.MoveInput;
            focusInput = player.gameObject.GetComponent<IFocusInputProvider>();
            fieldOfView = player.gameObject.GetComponent<FieldOfView>();
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
            if(focusInput.Foucus < 1)
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
            else if (focusInput.Foucus > 0)
            {
                if(fieldOfView.TargetObject != null)
                {
                    // 敵の方向ベクトルを取得
                    Vector3 targetObject = fieldOfView.TargetObject.transform.position;
                    targetObject.y = thisTransform.transform.position.y;
                    Vector3 enemyDirection = (targetObject - thisTransform.transform.position).normalized;
                    // プレイヤーが常に敵の方向を向く
                    thisTransform.rotation = Quaternion.LookRotation(enemyDirection, Vector3.up);
                }
            }
        }
    }
}
