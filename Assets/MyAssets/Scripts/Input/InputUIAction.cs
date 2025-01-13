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
            instance = this;

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
        }
        private void OnPause(InputAction.CallbackContext context)
        {
            pause = true;
            // ��u����true�ɂ��āA���̃t���[����false�ɖ߂�
            StartCoroutine(PausePress());
        }
        private System.Collections.IEnumerator PausePress()
        {
            yield return null; // 1�t���[���҂�
            pause = false;
        }

        private void OnEnable()
        {
            inputActions.Enable();

            SelectAction = inputActions.UI.Select;

            SelectAction.Enable();

            decideAction = inputActions.UI.Decide;

            decideAction.Enable();

            pauseAciton = inputActions.UI.Pause;

            pauseAciton.performed += OnPause;

            pauseAciton.Enable();
        }

        private void OnDisable()
        {

            SelectAction.Disable();

            decideAction.Disable();

            pauseAciton.performed -= OnPause;

            pauseAciton.Disable();

            inputActions.Disable();
        }

        private void OnDestroy()
        {
            inputActions.Dispose();
        }
    }
}
