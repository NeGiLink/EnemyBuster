using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class PlayerRotation : IRotation, ICharacterComponent<IPlayerSetup>
    {
        [SerializeField]
        private Transform thisTransform;

        [SerializeField]
        private Transform y_Focus;

        private Quaternion targetRotation;

        private IMoveInputProvider moveInput;

        private IVelocityComponent velocity;

        private IFocusInputProvider focusInput;

        private FieldOfView fieldOfView;

        private IPlayerAnimator animator;

        // 追加: 切り替え前の回転を保持する変数
        [SerializeField]
        private Quaternion previousCameraRotation;

        public void DoSetup(IPlayerSetup player)
        {
            thisTransform = player.gameObject.transform;
            targetRotation = thisTransform.rotation;
            velocity = player.Velocity;
            moveInput = player.MoveInput;
            focusInput = player.gameObject.GetComponent<IFocusInputProvider>();
            fieldOfView = player.gameObject.GetComponent<FieldOfView>();
            animator = player.PlayerAnimator;
        }

        public void DoUpdate()
        {
            var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
            if (moveInput.Horizontal != Define.Zero && moveInput.Vertical != Define.Zero)
            {
                velocity.CurrentVelocity = horizontalRotation * new Vector3(moveInput.Horizontal, Define.Zero, moveInput.Vertical).normalized;
            }
            else
            {
                velocity.CurrentVelocity = horizontalRotation * new Vector3(moveInput.Horizontal, Define.Zero, moveInput.Vertical);
            }
        }

        public void DoFixedUpdate()
        {
            if(focusInput.Foucus < Define.PressNum)
            {
                float rotationSpeed = Define.Zero;
                switch (PlayerInput.GetDeviceInput())
                {
                    case DeviceInput.Key:
                        rotationSpeed = 600 * Time.deltaTime;
                        if (velocity.CurrentVelocity.magnitude > 0.5f)
                        {
                            targetRotation = Quaternion.LookRotation(velocity.CurrentVelocity, Vector3.up);
                        }
                        break;
                    case DeviceInput.Controller:
                        rotationSpeed = 1200 * Time.deltaTime;
                        if (velocity.CurrentVelocity.magnitude > 0.1f)
                        {
                            targetRotation = Quaternion.LookRotation(velocity.CurrentVelocity, Vector3.up);
                        }
                        break;
                }

                thisTransform.rotation = Quaternion.RotateTowards(thisTransform.rotation, targetRotation, rotationSpeed);
                // 切り替え前の回転量を適用
                //targetRotation = previousCameraRotation * targetRotation;
            }
            else if (focusInput.Foucus > Define.Zero)
            {
                if(fieldOfView.TargetObject != null)
                {
                    if (fieldOfView.TargetObject == null) { return; }
                    DoLookOnTarget(fieldOfView.TargetObject.transform.position);
                }
                else
                {
                    var horizontalRotation = Quaternion.AngleAxis(moveInput.AimHorizontal.Value,Vector3.up);
                    var verticalRotation = Quaternion.AngleAxis(moveInput.AimVertical.Value,Vector3.right);
                    thisTransform.rotation = horizontalRotation;
                    y_Focus.localRotation = verticalRotation;

                    // ここでカメラ切り替え前の回転量を適用
                    targetRotation = horizontalRotation;
                }
            }
        }


        public void DoLookOnTarget(Vector3 dir)
        {
            // 敵の方向ベクトルを取得
            Vector3 targetObject = dir;
            targetObject.y = thisTransform.transform.position.y;
            Vector3 enemyDirection = (targetObject - thisTransform.transform.position).normalized;
            // プレイヤーが常に敵の方向を向く
            thisTransform.rotation = Quaternion.LookRotation(enemyDirection);
            // ここでカメラ切り替え前の回転量を適用
            targetRotation = thisTransform.rotation;
        }

        public void DoFreeMode()
        {
            float rotationSpeed = Define.Zero;
            switch (PlayerInput.GetDeviceInput())
            {
                case DeviceInput.Key:
                    rotationSpeed = 600 * Time.deltaTime;
                    if (velocity.CurrentVelocity.magnitude > 0.5f)
                    {
                        targetRotation = Quaternion.LookRotation(velocity.CurrentVelocity, Vector3.up);
                    }
                    break;
                case DeviceInput.Controller:
                    rotationSpeed = 1200 * Time.deltaTime;
                    if (velocity.CurrentVelocity.magnitude > 0.1f)
                    {
                        targetRotation = Quaternion.LookRotation(velocity.CurrentVelocity, Vector3.up);
                    }
                    break;
            }

            thisTransform.rotation = Quaternion.RotateTowards(thisTransform.rotation, targetRotation, rotationSpeed);
        }

        public void DoTargetLookOnMode()
        {
            if (fieldOfView.TargetObject == null) { return; }
            DoLookOnTarget(fieldOfView.TargetObject.transform.position);
        }

        public void OverTheShoulder()
        {
            var horizontalRotation = Quaternion.AngleAxis(moveInput.AimHorizontal.Value, Vector3.up);
            var verticalRotation = Quaternion.AngleAxis(moveInput.AimVertical.Value, Vector3.right);
            thisTransform.rotation = horizontalRotation;
            y_Focus.localRotation = verticalRotation;

            // ここでカメラ切り替え前の回転量を適用
            targetRotation = horizontalRotation;
        }
    }
}
