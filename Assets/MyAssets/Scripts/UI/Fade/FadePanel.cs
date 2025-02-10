using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * フェードパネルにアタッチして
     * SceneChangerのフラグよって遷移を行う
     * 画面のフェード演出を行うクラス
     */
    public class FadePanel : MonoBehaviour
    {
        private Image   panel;

        [SerializeField]
        private float   speed = 1.0f;
        [SerializeField]
        private float   targetAlpha = 0.0f;

        public void SetTargetAlpha(float alpha) { targetAlpha = alpha; }

        private void Awake()
        {
            panel = GetComponent<Image>();
        }

        private void Start()
        {
            if (targetAlpha > 0)
            {
                panel.color = new Color(0, 0, 0, 0);
            }
            else
            {
                panel.color = new Color(0, 0, 0, 1);
            }

            StartCoroutine(FadeStart());
        }
        //非同期フェード開始
        private IEnumerator FadeStart()
        {
            // フェードイン
            yield return StartCoroutine(Fade(targetAlpha));
            //遷移フラグがtrueなら遷移
            if (SceneChanger.Instance != null&&SceneChanger.Instance.IsTransitioning)
            {
                SceneChanger.Instance.OnChangeScene();
                SceneChanger.Instance.SetTransitioning(false);
            }
            //そうじゃないなら破壊
            else
            {
                Destroy(gameObject);
            }
        }


        // フェード処理
        private IEnumerator Fade(float targetAlpha)
        {
            float startAlpha = panel.color.a;
            float time = 0;

            while (time < speed)
            {
                time += Time.unscaledDeltaTime;
                float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / speed);
                panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alpha);
                yield return null;
            }

            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, targetAlpha);
        }
    }
}
