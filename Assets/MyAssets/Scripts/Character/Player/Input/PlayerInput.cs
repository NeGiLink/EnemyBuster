using UnityEngine;
using UnityEngine.InputSystem;

namespace MyAssets 
{
    public enum DeviceInput
    {
        Key,
        Controller
    }
    public class PlayerInput : MonoBehaviour,IMoveInputProvider, IControllerInput
    {
        private static DeviceInput deviceInput = DeviceInput.Key;
        public static DeviceInput GetDeviceInput() { return deviceInput; }
        // Actionをインスペクターから編集できるようにする
        [SerializeField]
        private InputAction _action;

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

        public void DoUpdate()
        {
            CheckInput();
            switch (deviceInput)
            {
                case DeviceInput.Key:
                    var value = _action.ReadValue<Vector2>();
                    move.x = value.x;
                    move.y = value.y;
                    break;
                case DeviceInput.Controller:
                    move.x = Input.GetAxis("Horizontal");
                    move.y = Input.GetAxis("Vertical");

                    break;
            }
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
            _action.Enable();
        }

        void OnDisable()
        {
            // InputActionを無効にする
            _action.Disable();
        }
    }
}

