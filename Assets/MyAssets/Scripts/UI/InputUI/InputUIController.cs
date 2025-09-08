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
        //アクションボタンのUI
        [SerializeField]
        private InputButtonUI[]         inputButtonUIs;
        //入力アクション
        [SerializeField]
        private InputActionReference[]  inputActions;

        private void Awake()
        {
            inputButtonUIs = GetComponentsInChildren<InputButtonUI>();
        }

        private void Update()
        {
            //ゲームが停止しているときは処理しない
            if (Time.timeScale < 1.0f) { return; }
            //右クリックと左クリックの処理
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
