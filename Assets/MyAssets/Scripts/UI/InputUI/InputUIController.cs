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
        //�A�N�V�����{�^����UI
        [SerializeField]
        private InputButtonUI[]         inputButtonUIs;
        //���̓A�N�V����
        [SerializeField]
        private InputActionReference[]  inputActions;

        private void Awake()
        {
            inputButtonUIs = GetComponentsInChildren<InputButtonUI>();
        }

        private void Update()
        {
            //�Q�[������~���Ă���Ƃ��͏������Ȃ�
            if (Time.timeScale < 1.0f) { return; }
            //�E�N���b�N�ƍ��N���b�N�̏���
            if (inputActions[(int)InputType.MouseRight])
            {
                if(inputActions[(int)InputType.MouseRight].action.ReadValue<float>() > 0)
                {
                    inputButtonUIs[(int)InputType.MouseRight].Press();
                }
                else
                {
                    inputButtonUIs[(int)InputType.MouseRight].PressEnd();
                }
            }

            if (inputActions[(int)InputType.MouseLeft])
            {
                if (inputActions[(int)InputType.MouseLeft].action.ReadValue<float>() > 0)
                {
                    inputButtonUIs[(int)InputType.MouseLeft].Press();
                }
                else
                {
                    inputButtonUIs[(int)InputType.MouseLeft].PressEnd();
                }
            }
        }
    }
}
