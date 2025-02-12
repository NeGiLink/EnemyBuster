using UnityEngine;

namespace MyAssets
{
    public enum GameLevel
    {
        Easy,
        Normal,
        Hard
    }
    /*
     * ゲームの難易度を設定するクラス
     * ボタンのコールバックに設定して使う
     */
    public class GameLevelController : MonoBehaviour
    {
        [SerializeField]
        private GameLevel       levelTag = GameLevel.Easy;

        private ModeInformation modeInformation;

        private void Awake()
        {
            modeInformation = GetComponentInChildren<ModeInformation>();
        }

        private void Start()
        {
            modeInformation.SetLevelText();
        }
        //コールバックで呼び出す関数
        public void SetLevel(int level)
        {
            levelTag = (GameLevel)level;
            GameManager.Instance.SetGameLevel(levelTag);
            modeInformation.SetLevelText();
        }
    }
}
