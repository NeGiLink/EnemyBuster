using UnityEngine;
using UnityEngine.InputSystem;

namespace MyAssets 
{
    public enum DeviceInput
    {
        Key,
        Controller
    }
    /*
     * �v���C���[�̈ړ��A�U���Ȃǂ̓��͂��܂Ƃ߂��N���X
     * �e���͂����o����悤�ɃC���^�[�t�F�[�X������
     */
    public class PlayerActionInput : MonoBehaviour,IMoveInputProvider, IJumpInputProvider, IControllerInput,IAttackInputProvider,IToolInputProvider,IFocusInputProvider
    {

        private GenericInput    genericInput;

        [SerializeField]
        private Vector2         move;
        public bool             IsMove => Mathf.Abs(move.x) > 0.1f || Mathf.Abs(move.y) > 0.1f;

        public Vector2          Move => move;
        public float            Horizontal => move.x;
        public void             SetHorizontal(float horizontalRatio) { move.x *= horizontalRatio; }
        public float            Vertical => move.y;
        public void             SetVertical(float verticalRatio) { move.y *= verticalRatio; }
        [SerializeField]
        private float           dash;
        public float            Dash => dash;

        [SerializeField]
        private bool            jump;
        public bool             Jump => jump;
        private InputAction     jumpAction;

        [SerializeField]
        private bool            attack;
        public bool             Attack => attack;
        private InputAction     attackAction;
        public InputAction      AttackAction => attackAction;

        [SerializeField]
        private bool            chargeAttack;
        public bool             ChargeAttack => chargeAttack;
        private InputAction     chargeAttackAction;
        public InputAction      ChargeAttackAction => chargeAttackAction;


        [SerializeField]
        private bool            receipt;
        public bool             WeaponEquipment => receipt;
        private InputAction     receiptAction;
        [SerializeField]
        private float           receipting;
        public float            Equipmenting => receipting;

        [SerializeField]
        private float           foucus;
        public float            Foucus => foucus;
        private InputAction     foucusAction;
        public InputAction      FoucusAction => foucusAction;

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

            //���͂��Ă�@��Ŕ���
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

            if (chargeAttackAction.WasReleasedThisFrame())
            {
                chargeAttack = false;
            }
            if (chargeAttackAction.IsPressed())
            {
                chargeAttack = true;
            }
        }

        private void OnEnable()
        {
            if(genericInput == null)
            {
                genericInput = new GenericInput();
            }
            // InputAction��L���ɂ���
            genericInput.Enable();

            jumpAction = genericInput.Player.Jump;
            attackAction = genericInput.Player.Attack;
            chargeAttackAction = genericInput.Player.ChargeAttack;
            receiptAction = genericInput.Player.Receipt;
            foucusAction = genericInput.Player.Foucus;

            jumpAction.performed += OnJump;
            attackAction.performed += OnAttack;

            jumpAction.Enable();
            attackAction.Enable();
            chargeAttackAction.Enable();
            receiptAction.Enable();
            foucusAction.Enable();
        }

        private void OnDisable()
        {

            jumpAction.performed -= OnJump;
            attackAction.performed -= OnAttack;

            // InputAction�𖳌��ɂ���
            jumpAction.Disable();
            attackAction.Disable();
            chargeAttackAction.Disable();
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

        private void OnDestroy()
        {
            // InputActionAsset�̃��b�p�[�N���X�̔j��
            // IDisposable���������Ă���̂ŁADispose����K�v������
            genericInput.Dispose();

            jumpAction?.Dispose();
        }
    }
}

