using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyAssets
{
    //イベントを開始するためのキーの入力判定を行うクラス
    public class EventKeyActivate : MonoBehaviour
    {
        private GenericInput genericInput;

        [SerializeField]
        private bool eventStart;
        public bool EventStart => eventStart;
        private InputAction eventStartAction;

        public InputAction EventStartAction => eventStartAction;

        [SerializeField]
        private bool eventEnd;
        public bool EventEnd => eventEnd;
        private InputAction eventEndAction;

        public InputAction EventEndAction => eventEndAction;

        public void SetActionPerformed(Action<InputAction.CallbackContext> start,Action<InputAction.CallbackContext> end,bool add)
        {
            if (add)
            {
                eventStartAction.performed += start;
                eventEndAction.performed += end;
            }
            else
            {
                eventStartAction.performed -= start;
                eventEndAction.performed -= end;
            }
        }

        private void OnEnable()
        {
            if (genericInput == null)
            {
                genericInput = new GenericInput();
            }
            // InputActionを有効にする
            genericInput.Enable();

            eventStartAction = genericInput.FindAction("UI/Event");
            eventEndAction = genericInput.FindAction("UI/Cancel");

            eventStartAction.performed += OnEventStart;
            eventEndAction.performed += OnEventEnd;

            eventStartAction.Enable();
            eventEndAction.Enable();
        }

        private void OnDisable()
        {
            eventStartAction.performed -= OnEventStart;
            eventEndAction.performed -= OnEventEnd;

            // InputActionを無効にする
            eventStartAction.Disable();
            eventEndAction.Disable();

            genericInput.Disable();
        }

        private void OnEventStart(InputAction.CallbackContext context)
        {
            eventStart = true;
            StartCoroutine(EventStartButtonPress());
        }

        private void OnEventEnd(InputAction.CallbackContext context)
        {
            eventEnd = true;
            StartCoroutine(EventEndButtonPress());
        }

        private System.Collections.IEnumerator EventStartButtonPress()
        {
            yield return null; // 1フレーム待つ
            eventStart = false;
        }

        private System.Collections.IEnumerator EventEndButtonPress()
        {
            yield return null; // 1フレーム待つ
            eventEnd = false;
        }
    }
}
