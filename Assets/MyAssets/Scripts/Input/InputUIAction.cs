using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace MyAssets
{
    public class InputUIAction : MonoBehaviour
    {
        private static InputUIAction instance;
        public static InputUIAction Instance => instance;

        private GenericInput inputActions;
        //*�I����͊֌W*//
        private InputAction SelectAction;
        [SerializeField]
        private Vector2 select;
        public Vector2 Select => select;
        //*������͊֌W*//
        private InputAction decideAction;
        [SerializeField]
        private bool decide;
        public bool Decide => decide;
        //*�|�[�Y���͊֌W*//
        private InputAction pauseAciton;
        [SerializeField]
        private bool pause;
        public bool Pause => pause;

        private PlayerInput playerInput;

        public bool IsInputKetBoardAndMouse => playerInput.currentControlScheme == "KeyBoard&Mouse";
        public bool IsInputGamePad => playerInput.currentControlScheme == "GamePad";

        private string lastControlScheme = "";

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);

            inputActions = new GenericInput();

            playerInput = GetComponent<PlayerInput>();
        }

        void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            lastControlScheme = playerInput.currentControlScheme;

            // ���̓C�x���g�̃��X�i�[��o�^
            InputSystem.onEvent += OnInputEvent;
        }
        private void Update()
        {
            //InputManager.CheckInput();

            if (SelectAction.WasReleasedThisFrame())
            {
                select = Vector2.zero;
            }
            if (SelectAction.IsPressed())
            {
                select = Vector2.zero;
            }
            select = Vector2.zero;
            if (SelectAction.WasPressedThisFrame())
            {
                select = inputActions.UI.Select.ReadValue<Vector2>();
            }

            if (decideAction.WasReleasedThisFrame())
            {
                decide = false;
            }
            if (decideAction.IsPressed())
            {
                decide = false;
            }
            if (decideAction.WasPressedThisFrame())
            {
                decide = true;
            }

            if (pauseAciton.WasReleasedThisFrame())
            {
                pause = false;
            }
            if (pauseAciton.IsPressed())
            {
                pause = false;
            }
            if (pauseAciton.WasPressedThisFrame())
            {
                pause = true;
            }
        }

        private void OnEnable()
        {
            if (inputActions == null) { return; }

            inputActions.Enable();

            SelectAction = inputActions.UI.Select;

            SelectAction.Enable();

            decideAction = inputActions.UI.Decide;

            decideAction.Enable();

            pauseAciton = inputActions.UI.Pause;

            pauseAciton.Enable();
        }

        private void OnDisable()
        {
            if(inputActions == null) { return; }
            SelectAction.Disable();

            decideAction.Disable();

            pauseAciton.Disable();

            inputActions.Disable();
        }

        private void OnDestroy()
        {
            if (inputActions == null) { return; }

            // ���X�i�[������
            InputSystem.onEvent -= OnInputEvent;

            inputActions.Dispose();
        }


        private void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
        {
            if (device is Keyboard || device is Mouse)
            {
                if (playerInput.currentControlScheme == "KeyBoardMouse" && lastControlScheme != "KeyBoardMouse")
                {
                    playerInput.SwitchCurrentControlScheme("KeyBoardMouse", Keyboard.current, Mouse.current);
                    //playerInput.defaultControlScheme = "KeyBoardMouse";
                    lastControlScheme = "KeyBoardMouse";
                    InputManager.SetDeviceInput(DeviceInput.Key);
                    Debug.Log("�L�[�{�[�h���}�E�X��L���� / �Q�[���p�b�h�𖳌���");
                }
            }
            // ���łɑI������Ă���R���g���[���X�L�[���Ȃ牽�����Ȃ�
            if (device is Gamepad)
            {
                if (playerInput.currentControlScheme == "GamePad"&&lastControlScheme != "GamePad")
                {

                    playerInput.SwitchCurrentControlScheme("GamePad", Gamepad.current);
                    //playerInput.defaultControlScheme = "GamePad";
                    lastControlScheme = "GamePad";
                    InputManager.SetDeviceInput(DeviceInput.Controller);
                    Debug.Log("�Q�[���p�b�h��L���� / �L�[�{�[�h���}�E�X�𖳌���");
                }
            }

            if(GameManager.Instance.SceneList == SceneList.Game)
            {
                if (SystemManager.IsPause)
                {
                    InputManager.SetFreeCursor();
                }
                else
                {
                    InputManager.SetLockCursor();
                }
            }
            else
            {
                if(lastControlScheme == "KeyBoardMouse")
                {
                    InputManager.SetFreeCursor();
                }
                else if(lastControlScheme == "GamePad")
                {
                    InputManager.SetLockCursor();
                }
            }
            
        }

    }

    public class InputManager
    {
        private static bool inputStop = false;

        public static bool InputStop => inputStop;
        public static void SetInputStop(bool i) {  inputStop = i; }

        private static DeviceInput deviceInput = DeviceInput.Key;
        public static void SetDeviceInput(DeviceInput device) { deviceInput = device; }
        public static DeviceInput GetDeviceInput() { return deviceInput; }

        public static void SetLockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public static void SetFreeCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
