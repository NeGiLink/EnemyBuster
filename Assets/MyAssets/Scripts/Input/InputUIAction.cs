using UnityEngine;
using UnityEngine.InputSystem;

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
        private void OnPause(InputAction.CallbackContext context)
        {
            // ��u����true�ɂ��āA���̃t���[����false�ɖ߂�
            StartCoroutine(PausePress());
        }
        private System.Collections.IEnumerator PausePress()
        {
            pause = true;
            yield return null; // 1�t���[���҂�
            pause = false;
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

           // pauseAciton.performed += OnPause;

            pauseAciton.Enable();
        }

        private void OnDisable()
        {
            if(inputActions == null) { return; }
            SelectAction.Disable();

            decideAction.Disable();

            //pauseAciton.performed -= OnPause;

            pauseAciton.Disable();

            inputActions.Disable();
        }

        private void OnDestroy()
        {
            if (inputActions == null) { return; }
            inputActions.Dispose();
        }
    }
}
