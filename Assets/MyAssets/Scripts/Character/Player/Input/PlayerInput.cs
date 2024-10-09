using UnityEngine;
using UnityEngine.InputSystem;

namespace MyAssets 
{
    public enum DeviceInput
    {
        Key,
        Controller
    }
    public class PlayerInput : MonoBehaviour,IMoveInputProvider, IJumpInputProvider, IControllerInput
    {
        private static DeviceInput deviceInput = DeviceInput.Key;
        public static DeviceInput GetDeviceInput() { return deviceInput; }

        private GenericInput genericInput;

        [SerializeField]
        private Vector2 move;
        [SerializeField]
        private string horizontalName = "Horizontal";
        [SerializeField]
        private string verticalName = "Vertical";
        public bool IsMove => Mathf.Abs(move.x) > 0.1f || Mathf.Abs(move.y) > 0.1f;

        public Vector2 Move => move;
        public float Horizontal => move.x;
        public float Vertical => move.y;

        [SerializeField]
        private float dash;
        public float Dash => dash;

        [SerializeField]
        private float jump;
        public float Jump => jump;
        [SerializeField]
        private bool jumpPush;
        public bool IsJumpPush => jumpPush;

        public void Setup()
        {
            genericInput = new GenericInput();
        }

        public void DoUpdate()
        {
            CheckInput();
            switch (deviceInput)
            {
                case DeviceInput.Key:
                    var value = genericInput.Player.Move.ReadValue<Vector2>();
                    move.x = value.x;
                    move.y = value.y;
                    break;
                case DeviceInput.Controller:
                    move.x = Input.GetAxis("Horizontal");
                    move.y = Input.GetAxis("Vertical");
                    break;
            }
            dash = genericInput.Player.Dash.ReadValue<float>();
            jump = genericInput.Player.Jump.ReadValue<float>();
        }

        public static void CheckInput()
        {
            if (Input.anyKey)
            {
                deviceInput = DeviceInput.Key;
            }

            // コントローラーボタンのチェック
            for (int i = 0; i < 14; i++)
            {
                if (Input.GetKey("joystick button" + " " + i))
                {
                    deviceInput = DeviceInput.Controller;
                    break;
                }
            }

            // コントローラーの軸のチェック
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) &&
               !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                if ((Mathf.Abs(Input.GetAxis("Horizontal")) >= 1.0f) ||
                    (Mathf.Abs(Input.GetAxis("Vertical")) >= 1.0f))
                {
                    deviceInput = DeviceInput.Controller;
                }
            }
        }

        void OnEnable()
        {
            // InputActionを有効にする
            genericInput.Enable();
        }

        void OnDisable()
        {
            // InputActionを無効にする
            genericInput.Disable();
        }

        private void OnDestroy()
        {
            // InputActionAssetのラッパークラスの破棄
            // IDisposableを実装しているので、Disposeする必要がある
            genericInput.Dispose();
        }
    }
}

