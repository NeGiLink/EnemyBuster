using Unity.VisualScripting;
using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// Canvas�ւ̒ǉ��E�ύX�E�폜���s���N���X
    /// </summary>
    public class GameUIController : MonoBehaviour
    {
        private static GameUIController instance;
        public static GameUIController Instance => instance;

        [SerializeField]
        private FadeInText fadeInText;

        [SerializeField]
        private GameObject resultUI;

        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void CreateFadeResultTextUI()
        {
            Transform parent = GameCanvas.Instance.UILayer[(int)UILayer.System].transform;
            FadeInText text = Instantiate(fadeInText, parent);
            text.gameObject.AddComponent<ResultCreater>();
            if(AllEnemyKillController.Instance.CurrentEnemyKillCount >= AllEnemyKillController.Instance.MaxEnemyKillCount)
            {
                text.SetOutputText("�Q�[���N���A");
            }
            else
            {
                text.SetOutputText("�Q�[���I�[�o�[");
            }
        }

        public void CreateResultUI()
        {
            GameManager.Instance.SetFreeCursor();
            Transform parent = GameCanvas.Instance.UILayer[(int)UILayer.System].transform;
            Instantiate(resultUI, parent);
        }
    }
}
