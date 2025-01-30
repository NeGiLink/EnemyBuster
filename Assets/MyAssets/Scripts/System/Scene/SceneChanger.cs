using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyAssets
{
    public class SceneChanger : MonoBehaviour
    {
        private static SceneChanger instance;
        public static SceneChanger Instance => instance;

        private bool isTransitioning = false;
        public bool IsTransitioning => isTransitioning;
        public void SetTransitioning(bool t) {  isTransitioning = t; }

        private SceneList nextScene;

        [SerializeField]
        private FadePanel fadePanel;

        private Canvas canvas;
        public Canvas Canvas
        {
            get 
            {
                if(canvas == null)
                {
                    canvas = FindObjectOfType<Canvas>();
                }
                return canvas; 
            }
        }

        public void SetNextScene(SceneList scene)
        {
            nextScene = scene;
            isTransitioning = true;
            CreateFadePanel(false);
        }

        [SerializeField]
        private float changeCount = 3f;

        public void SlowSceneChange(SceneList scene, float count)
        {
            changeCount = count;
            nextScene = scene;
            StartCoroutine(SlowFade());
        }

        private System.Collections.IEnumerator SlowFade()
        {
            yield return new WaitForSecondsRealtime(changeCount); // 1フレーム待つ
            CreateFadePanel(false);
            isTransitioning = true;
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        /*
         * ここで各シーンの名前をリスト別に取得
         * 引数１：SceneList scene 取得したシーンの名前を取得
         */
        private string GetSceneName(SceneList scene)
        {
            string temp;

            switch (scene)
            {
                case SceneList.Title:
                    temp = "00_TitleScene";
                    break;
                case SceneList.Select:
                    temp = "01_SelectScene";
                    break;
                case SceneList.Game:
                    temp = "02_GameScene";
                    break;
                default:
                    temp = "TitleScene";
                    break;
            }
            return temp;
        }
        //取得したシーンへ遷移
        public void ChangeScene()
        {
            SceneManager.LoadScene(GetSceneName(nextScene));
        }

        //シーン遷移ボタンを決定した時に呼び出されるメソッド
        public void OnChangeScene()
        {
            if (instance == null) { return; }
            StartCoroutine(ChangeStart());
        }
        //指定したcount後にシーンを遷移するメソッド
        private System.Collections.IEnumerator ChangeStart()
        {
            yield return new WaitForSecondsRealtime(changeCount); // 1フレーム待つ
            ChangeScene();
        }


        private void CreateFadePanel(bool fadeIn)
        {
            FadePanel panel = Instantiate(fadePanel, Canvas.transform);

            float alpha = 1f;
            if (fadeIn)
            {
                alpha = 0f;
            }
            panel.SetTargetAlpha(alpha);
        }
    }
}
