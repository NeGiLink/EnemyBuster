using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyAssets
{
    /*
     * �V�[���̑J�ڂ�ݒ���s���N���X
     * �V�[����1�����Ȃ̂ŃV���O���g���p�^�[��
     */
    public class SceneChanger : MonoBehaviour
    {
        private static SceneChanger     instance;
        public static SceneChanger      Instance => instance;

        [SerializeField]
        private FadePanel               fadePanel;

        [SerializeField]
        private float                   changeCount = 3f;

        private bool                    isTransitioning = false;
        public bool                     IsTransitioning => isTransitioning;

        private SceneList               nextScene;


        private Canvas                  canvas;
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
        public void SetTransitioning(bool t) {  isTransitioning = t; }

        public void SetNextScene(SceneList scene)
        {
            nextScene = scene;
            isTransitioning = true;
            CreateFadePanel(false);
        }


        public void SlowSceneChange(SceneList scene, float count)
        {
            changeCount = count;
            nextScene = scene;
            StartCoroutine(SlowFade());
        }

        private System.Collections.IEnumerator SlowFade()
        {
            // �ݒ肵�����Ԏ~�߂�
            yield return new WaitForSecondsRealtime(changeCount); 
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
         * �����Ŋe�V�[���̖��O�����X�g�ʂɎ擾
         * �����P�FSceneList scene �擾�����V�[���̖��O���擾
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
                case SceneList.Credit:
                    temp = "03_CreditScene";
                    break;
                case SceneList.Training:
                    temp = "04_TrainingScene";
                    break;
                default:
                    temp = "TitleScene";
                    break;
            }
            return temp;
        }
        //�擾�����V�[���֑J��
        public void ChangeScene()
        {
            SceneManager.LoadScene(GetSceneName(nextScene));
        }

        //�V�[���J�ڃ{�^�������肵�����ɌĂяo����郁�\�b�h
        public void OnChangeScene()
        {
            if (instance == null) { return; }
            StartCoroutine(ChangeStart());
        }
        //�w�肵��count��ɃV�[����J�ڂ��郁�\�b�h
        private System.Collections.IEnumerator ChangeStart()
        {
            yield return new WaitForSecondsRealtime(changeCount); // 1�t���[���҂�
            ChangeScene();
        }

        //�V�[���J�ڎ��Ƀt�F�[�h�p�l���𐶐�����
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
