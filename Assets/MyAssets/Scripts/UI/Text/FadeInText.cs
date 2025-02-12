using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * �t�F�[�h�C���E�t�F�[�h�A�E�g���镶����UI�̏������s���N���X
     */
    public class FadeInText : MonoBehaviour
    {
        //�����̔w�i��Image
        [SerializeField]
        private Image   panelImage;
        //����
        [SerializeField]
        private Text    text;
        //�����̓��e
        [SerializeField]
        private string  outputText;
        //�w�i�̃A���t�@�l
        [SerializeField]
        private float   panelAlphe = 0;
        //�e�L�X�g�̃A���t�@�l
        [SerializeField]
        private float   textAlphe = 0;
        //�t�F�[�h���x
        [SerializeField]
        private float   fadeSpeed = 1f;
        //�t�F�[�h�I������
        [SerializeField]
        private float   stopCount = 1f;

        public void SetOutputText(string t) { outputText = t; }
        private void Awake()
        {
            panelImage = GetComponentInChildren<Image>();
            text = GetComponentInChildren<Text>();
        }

        void Start()
        {

            panelAlphe = panelImage.color.a;
            textAlphe = text.color.a;
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 0);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

            text.text = outputText;

            if(fadeSpeed <= 0)
            {
                fadeSpeed = 1f;
            }

            StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            // �t�F�[�h�C��
            yield return StartCoroutine(FadePanel(panelAlphe));
            yield return StartCoroutine(FadeText(textAlphe));

            yield return new WaitForSecondsRealtime(stopCount);

            yield return StartCoroutine(FadeText(0));
            yield return StartCoroutine(FadePanel(0));

            Destroy(gameObject);
        }

        // �t�F�[�h����
        private IEnumerator FadePanel(float targetAlpha)
        {
            float startAlpha = panelImage.color.a;
            float time = 0;

            while (time < fadeSpeed)
            {
                time += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeSpeed);
                panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);
                yield return null;
            }

            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, targetAlpha);
        }

        // �t�F�[�h����
        private IEnumerator FadeText(float targetAlpha)
        {
            float startAlpha = text.color.a;
            float time = 0;

            while (time < fadeSpeed)
            {
                time += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeSpeed);
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
                yield return null;
            }

            text.color = new Color(text.color.r, text.color.g, text.color.b, targetAlpha);
        }
    }
}
