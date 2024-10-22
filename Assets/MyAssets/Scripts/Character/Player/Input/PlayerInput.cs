using UnityEngine;
using UnityEngine.InputSystem;

namespace MyAssets 
{
    public enum DeviceInput
    {
        Key,
        Controller
    }
    public class PlayerInput : MonoBehaviour,IMoveInputProvider, IJumpInputProvider, IControllerInput,IAttackInputProvider
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
        public void SetHorizontal(float horizontalRatio) { move.x *= horizontalRatio; }
        public float Vertical => move.y;
        public void SetVertical(float verticalRatio) { move.y *= verticalRatio; }
        [SerializeField]
        private float dash;
        public float Dash => dash;

        [SerializeField]
        private bool jump;
        public bool Jump => jump;
        [SerializeField]
        private bool jumpPush;
        public bool IsJumpPush => jumpPush;
        private InputAction jumpAction;

        [SerializeField]
        private bool attack;
        public bool Attack => attack;
        private InputAction attackAction;

        private Timer attackInputTimer;
        public Timer AttackInputTimer => attackInputTimer;

        public void Setup()
        {
            genericInput = new GenericInput();
            attackInputTimer = new Timer();
        }

        private void TimerUpdate()
        {
            attackInputTimer.Update(Time.deltaTime);
        }

        public void DoUpdate()
        {
            TimerUpdate();
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
            //var j = genericInput.Player.Jump.ReadValue<float>();
            //if(j != 0)
            //{
            //    jump = true;
            //}
            jump = Input.GetKeyDown(KeyCode.Space);
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

        private void OnEnable()
        {
            // InputActionを有効にする
            genericInput.Enable();

            jumpAction = genericInput.FindAction("Player/Jump");

            jumpAction.Enable();

            attackAction = genericInput.FindAction("Player/Attack");

            attackAction.performed += OnAttack;

            attackAction.Enable();

        }

        private void OnDisable()
        {

            jumpAction.Disable();

            attackAction.performed -= OnAttack;
            attackAction.Disable();

            // InputActionを無効にする
            genericInput.Disable();
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            attack = true;
            /*
            if (attackInputTimer.IsEnd())
            {
                attackInputTimer.Start(0.05f);
            }
             */
            // 一瞬だけtrueにして、次のフレームでfalseに戻す
            StartCoroutine(ResetButtonPress());
        }
        private System.Collections.IEnumerator ResetButtonPress()
        {
            yield return null; // 1フレーム待つ
            attack = false;
        }

        private void OnDestroy()
        {
            // InputActionAssetのラッパークラスの破棄
            // IDisposableを実装しているので、Disposeする必要がある
            genericInput.Dispose();

            jumpAction?.Dispose();
        }
    }
}

