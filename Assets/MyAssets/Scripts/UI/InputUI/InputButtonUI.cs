using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MyAssets
{
    public class InputButtonUI : MonoBehaviour
    {
        private Image parentImage;

        private Image[] childImage;

        private Vector2 basePosition = Vector2.zero;

        private bool press = false;

        private void Awake()
        {
            parentImage = GetComponent<Image>();
            childImage = GetComponentsInChildren<Image>();
        }

        private void Start()
        {
            basePosition = parentImage.rectTransform.anchoredPosition;
        }

        public void PressDown(InputAction.CallbackContext context)
        {
            if (press) { return; }
            StartCoroutine(PressDownStart());
        }

        private IEnumerator PressDownStart()
        {
            Vector2 pos = parentImage.rectTransform.anchoredPosition;
            pos.y -= 10;
            parentImage.rectTransform.anchoredPosition = pos;

            parentImage.color = Color.gray;

            press = true;

            yield return new WaitForSecondsRealtime(0.1f);
            parentImage.rectTransform.anchoredPosition = basePosition;

            parentImage.color = Color.white;

            press = false;
        }

        public void Press()
        {
            if (press) { return ; }
            Vector2 pos = parentImage.rectTransform.anchoredPosition;
            pos.y -= 10;
            parentImage.rectTransform.anchoredPosition = pos;

            parentImage.color = Color.gray;

            press = true;
        }

        public void PressEnd()
        {
            if (!press) { return; }
            parentImage.rectTransform.anchoredPosition = basePosition;

            parentImage.color = Color.white;

            press = false;
        }
    }
}
