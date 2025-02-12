using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * �A�N�V�����{�^��
     * �R�[���o�b�N�Őݒ肵�Ĕ񓯊��ŏ������s��
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
        //�R�[���o�b�N�ɐݒ肷��֐�
        public void PressDown(InputAction.CallbackContext context)
        {
            if (press) { return; }
            StartCoroutine(PressDownStart());
        }
        //UI��0.1f�b�����X�V���Č��ɖ߂��֐�
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
        //���͂����������Ă���ԍX�V���邽�߂̊֐�
        public void Press()
        {
            if (press) { return ; }
            Vector2 pos = parentImage.rectTransform.anchoredPosition;
            pos.y -= 10;
            parentImage.rectTransform.anchoredPosition = pos;

            parentImage.color = Color.gray;

            press = true;
        }
        //���͂��I���������Ɍ��ɖ߂��֐�
        public void PressEnd()
        {
            if (!press) { return; }
            parentImage.rectTransform.anchoredPosition = basePosition;

            parentImage.color = Color.white;

            press = false;
        }
    }
}
