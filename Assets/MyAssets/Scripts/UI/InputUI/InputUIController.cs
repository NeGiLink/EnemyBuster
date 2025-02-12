using UnityEngine;
using UnityEngine.InputSystem;

namespace MyAssets
{
    public enum InputType
    {
        MouseLeft,
        MouseRight
    }
    /*
     * 入力のUIをまとめて処理するクラス
     */
    public class InputUIController : MonoBehaviour
    {
        [SerializeField]
        private InputButtonUI[]         inputButtonUIs;
        [SerializeField]
        private InputActionReference[]  inputActions;

        private void Awake()
        {
            inputButtonUIs = GetComponentsInChildren<InputButtonUI>();
        }
        //UIのコールバックは表示にセット
        private void OnEnable()
        {

            inputActions[(int)InputType.MouseLeft].action.performed += inputButtonUIs[(int)InputType.MouseLeft].PressDown;
            inputActions[(int)InputType.MouseLeft].action.Enable();

        }
        //非表示時に解放
        private void OnDisable()
        {
            inputActions[(int)InputType.MouseLeft].action.performed -= inputButtonUIs[(int)InputType.MouseLeft].PressDown;
            inputActions[(int)InputType.MouseLeft].action.Disable();
        }

        private void Update()
        {
            if(inputActions[(int)InputType.MouseRight] == null) { return; }
            if(inputActions[(int)InputType.MouseRight].action.ReadValue<float>() > 0)
            {
                inputButtonUIs[(int)InputType.MouseRight].Press();
            }
            else
            {
                inputButtonUIs[(int)InputType.MouseRight].PressEnd();
            }
        }
    }
}
