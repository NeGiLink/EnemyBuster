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

    public class PlayerCharacterInput : MonoBehaviour,IMoveInputProvider, IJumpInputProvider, IControllerInput,IAttackInputProvider,IToolInputProvider,IFocusInputProvider
    {

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
        public InputAction AttackAction => attackAction;
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
        public InputAction FoucusAction => foucusAction;

        public void Setup()
        {
            if(genericInput == null)
            {
                genericInput = new GenericInput();
            }
        }

        public void DoUpdate()
        {
            if (InputManager.InputStop) { return; }
            aimHorizontal.Update(Time.deltaTime);
            aimVertical.Update(Time.deltaTime);


            if(InputManager.GetDeviceInput() == DeviceInput.Key)
            {
                var value = genericInput.Player.Move.ReadValue<Vector2>();
                move.x = value.x;
                move.y = value.y;
            }
            else
            {
                // �Q�[���p�b�h�i�f�o�C�X�擾�j
                var gamepad = Gamepad.current;
                if (gamepad != null)
                {
                    move = gamepad.leftStick.ReadValue();
                }
            }
            dash = genericInput.Player.Dash.ReadValue<float>();
            foucus = genericInput.Player.Foucus.ReadValue<float>();
            receipting = genericInput.Player.Receipt.ReadValue<float>();
        }

        private void OnEnable()
        {
            if(genericInput == null)
            {
                genericInput = new GenericInput();
            }
            // InputAction��L���ɂ���
            genericInput.Enable();

            jumpAction = genericInput.FindAction("Player/Jump");
            attackAction = genericInput.FindAction("Player/Attack");
            receiptAction = genericInput.FindAction("Player/Receipt");
            foucusAction = genericInput.FindAction("Player/Foucus");

            jumpAction.performed += OnJump;
            attackAction.performed += OnAttack;
            //receiptAction.performed += OnReceipt;
            //foucusAction.performed += OnFoucus;

            jumpAction.Enable();
            attackAction.Enable();
            receiptAction.Enable();
            foucusAction.Enable();
        }

        private void OnDisable()
        {

            jumpAction.performed -= OnJump;
            attackAction.performed -= OnAttack;
            //receiptAction.performed -= OnReceipt;
            //foucusAction.performed -= OnFoucus;

            // InputAction�𖳌��ɂ���
            jumpAction.Disable();
            attackAction.Disable();
            receiptAction.Disable();
            foucusAction.Disable();

            genericInput.Disable();
        }
        private void OnJump(InputAction.CallbackContext context)
        {
            Debug.Log("�W�����v");
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

