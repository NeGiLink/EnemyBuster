using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * アクションボタン
     * コールバックで設定して非同期で処理を行う
     */
    public class InputButtonUI : MonoBehaviour
    {
        private Image       parentImage;

        private Image[]     childImage;

        private Vector2     basePosition = Vector2.zero;

        private bool        press = false;

        private void Awake()
        {
            parentImage = GetComponent<Image>();
            childImage = GetComponentsInChildren<Image>();
        }

        private void Start()
        {
            basePosition = parentImage.rectTransform.anchoredPosition;
        }
        //コールバックに設定する関数
        public void PressDown(InputAction.CallbackContext context)
        {
            if (press) { return; }
            StartCoroutine(PressDownStart());
        }
        //UIを0.1f秒だけ更新して元に戻す関数
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
        //入力を押し続けている間更新するための関数
        public void Press()
        {
            if (press) { return ; }
            Vector2 pos = parentImage.rectTransform.anchoredPosition;
            pos.y -= 10;
            parentImage.rectTransform.anchoredPosition = pos;

            parentImage.color = Color.gray;

            press = true;
        }
        //入力が終了した時に元に戻す関数
        public void PressEnd()
        {
            if (!press) { return; }
            parentImage.rectTransform.anchoredPosition = basePosition;

            parentImage.color = Color.white;

            press = false;
        }
    }
}
