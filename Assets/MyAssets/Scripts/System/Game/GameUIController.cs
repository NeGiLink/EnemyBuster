using Unity.VisualScripting;
using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// Canvasへの追加・変更・削除を行うクラス
    /// </summary>
    public class GameUIController : MonoBehaviour
    {
        private static GameUIController instance;
        public static GameUIController Instance => instance;

        [SerializeField]
        private FadeInText fadeInText;

        [SerializeField]
        private GameObject resultUI;


        private DamageTextCreator damageTextCreator;
        public DamageTextCreator DamageTextCreator => damageTextCreator;

        private void Awake()
        {
            instance = this;

            damageTextCreator = GetComponent<DamageTextCreator>();
        }

        public void CreateFadeResultTextUI()
        {
            Transform parent = GameCanvas.Instance.UILayer[(int)UILayer.System].transform;
            FadeInText text = Instantiate(fadeInText, parent);
            text.gameObject.AddComponent<ResultCreater>();
            if(AllEnemyKillController.Instance.CurrentEnemyKillCount >= AllEnemyKillController.Instance.MaxEnemyKillCount)
            {
                text.SetOutputText("ゲームクリア");
            }
            else
            {
                text.SetOutputText("ゲームオーバー");
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
