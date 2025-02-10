using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace MyAssets
{
    /*
     * UIのボタン入力を行うクラス
     * ゲーム中1つで十分なのでシングルトンパターン
     */
    public class InputUIAction : MonoBehaviour
    {
        private static InputUIAction    instance;
        public static InputUIAction     Instance => instance;

        private GenericInput            inputActions;

        //*選択入力関係*//
        private InputAction             SelectAction;
        [SerializeField]
        private Vector2                 select;
        public Vector2                  Select => select;

        //*決定入力関係*//
        private InputAction             decideAction;
        [SerializeField]
        private bool                    decide;
        public bool                     Decide => decide;

        //*ポーズ入力関係*//
        private InputAction             pauseAciton;
        [SerializeField]
        private bool                    pause;
        public bool                     Pause => pause;

        private PlayerInput             playerInput;

        private string                  lastControlScheme = "";

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

            // 入力イベントのリスナーを登録
            InputSystem.onEvent += OnInputEvent;
        }
        private void Update()
        {
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

            // リスナーを解除
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
                    lastControlScheme = "KeyBoardMouse";
                    InputManager.SetDeviceInput(DeviceInput.Key);
                    Debug.Log("キーボード＆マウスを有効化 / ゲームパッドを無効化");
                }
            }
            // すでに選択されているコントロールスキームなら何もしない
            if (device is Gamepad)
            {
                if (playerInput.currentControlScheme == "GamePad"&&lastControlScheme != "GamePad")
                {

                    playerInput.SwitchCurrentControlScheme("GamePad", Gamepad.current);
                    lastControlScheme = "GamePad";
                    InputManager.SetDeviceInput(DeviceInput.Controller);
                    Debug.Log("ゲームパッドを有効化 / キーボード＆マウスを無効化");
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
}
