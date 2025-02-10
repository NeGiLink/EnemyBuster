using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * �t�F�[�h�p�l���ɃA�^�b�`����
     * SceneChanger�̃t���O����đJ�ڂ��s��
     * ��ʂ̃t�F�[�h���o���s���N���X
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
        //�񓯊��t�F�[�h�J�n
        private IEnumerator FadeStart()
        {
            // �t�F�[�h�C��
            yield return StartCoroutine(Fade(targetAlpha));
            //�J�ڃt���O��true�Ȃ�J��
            if (SceneChanger.Instance != null&&SceneChanger.Instance.IsTransitioning)
            {
                SceneChanger.Instance.OnChangeScene();
                SceneChanger.Instance.SetTransitioning(false);
            }
            //��������Ȃ��Ȃ�j��
            else
            {
                Destroy(gameObject);
            }
        }


        // �t�F�[�h����
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
