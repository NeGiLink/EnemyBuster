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
     * ���͂�UI���܂Ƃ߂ď�������N���X
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
        //UI�̃R�[���o�b�N�͕\���ɃZ�b�g
        private void OnEnable()
        {

            inputActions[(int)InputType.MouseLeft].action.performed += inputButtonUIs[(int)InputType.MouseLeft].PressDown;
            inputActions[(int)InputType.MouseLeft].action.Enable();

        }
        //��\�����ɉ��
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
