using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyAssets 
{
    public enum DeviceInput
    {
        Key,
        Controller
    }

    public class PlayerInput : MonoBehaviour,IMoveInputProvider, IJumpInputProvider, IControllerInput,IAttackInputProvider,IToolInputProvider,IFocusInputProvider
    {
        private static DeviceInput deviceInput = DeviceInput.Key;
        public static DeviceInput GetDeviceInput() { return deviceInput; }

        [Header("Aim")]
        [SerializeField]
        AxisState aimVertical;
        public AxisState AimVertical => aimVertical;
        [SerializeField]
        AxisState aimHorizontal;
        public AxisState AimHorizontal => aimHorizontal;

        private GenericInput genericInput;

        [SerializeField]
        private Vector2 move;
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
        private InputAction jumpAction;
        [SerializeField]
        private bool attack;
        public bool Attack => attack;
        private InputAction attackAction;
        [SerializeField]
        private bool receipt;
        public bool Receipt => receipt;
        private InputAction receiptAction;
        [SerializeField]
        private float receipting;
        public float Receipting => receipting;

        [SerializeField]
        private float foucus;
        public float Foucus => foucus;
        private InputAction foucusAction;
        public void Setup()
        {
            genericInput = new GenericInput();
        }

        public void DoUpdate()
        {
            aimHorizontal.Update(Time.deltaTime);
            aimVertical.Update(Time.deltaTime);

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
            foucus = genericInput.Player.Foucus.ReadValue<float>();
            receipting = genericInput.Player.Receipt.ReadValue<float>();
        }

        public static void CheckInput()
        {
            if (Input.anyKey)
            {
                deviceInput = DeviceInput.Key;
            }

            // �R���g���[���[�{�^���̃`�F�b�N
            for (int i = 0; i < 14; i++)
            {
                if (Input.GetKey("joystick button" + " " + i))
                {
                    deviceInput = DeviceInput.Controller;
                    break;
                }
            }

            // �R���g���[���[�̎��̃`�F�b�N
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) &&
               !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                if ((Mathf.Abs(Input.GetAxis("Horizontal")) >= Define.PressNum) ||
                    (Mathf.Abs(Input.GetAxis("Vertical")) >= Define.PressNum))
                {
                    deviceInput = DeviceInput.Controller;
                }
            }
        }

        private void OnEnable()
        {
            // InputAction��L���ɂ���
            genericInput.Enable();

            jumpAction = genericInput.FindAction("Player/Jump");
            attackAction = genericInput.FindAction("Player/Attack");
            receiptAction = genericInput.FindAction("Player/Receipt");
            //foucusAction = genericInput.FindAction("Player/Foucus");

            jumpAction.performed += OnJump;
            attackAction.performed += OnAttack;
            receiptAction.performed += OnReceipt;
            //foucusAction.performed += OnFoucus;

            jumpAction.Enable();
            attackAction.Enable();
            receiptAction.Enable();
            //foucusAction.Enable();
        }

        private void OnDisable()
        {

            jumpAction.performed -= OnJump;
            attackAction.performed -= OnAttack;
            receiptAction.performed -= OnReceipt;
            //foucusAction.performed -= OnFoucus;

            // InputAction�𖳌��ɂ���
            jumpAction.Disable();
            attackAction.Disable();
            receiptAction.Disable();
            //foucusAction.Disable();
            genericInput.Disable();
        }
        private void OnJump(InputAction.CallbackContext context)
        {
            jump = true;
            // ��u����true�ɂ��āA���̃t���[����false�ɖ߂�
            StartCoroutine(ResetJumpButtonPress());
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            attack = true;
            // ��u����true�ɂ��āA���̃t���[����false�ɖ߂�
            StartCoroutine(ResetAttackButtonPress());
        }
        private void OnReceipt(InputAction.CallbackContext context)
        {
            receipt = true;
            // ��u����true�ɂ��āA���̃t���[����false�ɖ߂�
            StartCoroutine(ResetReceiptButtonPress());
        }

        private void OnFoucus(InputAction.CallbackContext context)
        {
            //foucus = true;
            // ��u����true�ɂ��āA���̃t���[����false�ɖ߂�
            StartCoroutine(ResetFoucusButtonPress());
        }

        private System.Collections.IEnumerator ResetJumpButtonPress()
        {
            yield return null; // 1�t���[���҂�
            jump = false;
        }

        private System.Collections.IEnumerator ResetAttackButtonPress()
        {
            yield return null; // 1�t���[���҂�
            attack = false;
        }
        private System.Collections.IEnumerator ResetReceiptButtonPress()
        {
            yield return null; // 1�t���[���҂�
            receipt = false;
        }
        private System.Collections.IEnumerator ResetFoucusButtonPress()
        {
            yield return null; // 1�t���[���҂�
            //foucus = false;
        }

        private void OnDestroy()
        {
            // InputActionAsset�̃��b�p�[�N���X�̔j��
            // IDisposable���������Ă���̂ŁADispose����K�v������
            genericInput.Dispose();

            jumpAction?.Dispose();
        }
    }
}

