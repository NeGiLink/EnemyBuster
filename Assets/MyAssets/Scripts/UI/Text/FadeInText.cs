using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * フェードイン・フェードアウトする文字のUIの処理を行うクラス
     */
    public class FadeInText : MonoBehaviour
    {
        //文字の背景のImage
        [SerializeField]
        private Image   panelImage;
        //文字
        [SerializeField]
        private Text    text;
        //文字の内容
        [SerializeField]
        private string  outputText;
        //背景のアルファ値
        [SerializeField]
        private float   panelAlphe = 0;
        //テキストのアルファ値
        [SerializeField]
        private float   textAlphe = 0;
        //フェード速度
        [SerializeField]
        private float   fadeSpeed = 1f;
        //フェード終了時間
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
            // フェードイン
            yield return StartCoroutine(FadePanel(panelAlphe));
            yield return StartCoroutine(FadeText(textAlphe));

            yield return new WaitForSecondsRealtime(stopCount);

            yield return StartCoroutine(FadeText(0));
            yield return StartCoroutine(FadePanel(0));

            Destroy(gameObject);
        }

        // フェード処理
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

        // フェード処理
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
