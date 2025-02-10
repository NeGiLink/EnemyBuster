using UnityEngine;

namespace MyAssets
{
    /*
     * �Q�[���̌��ʂ̃V�X�e���̃N���X
     */
    public class ResultSystem : MonoBehaviour
    {
        //���[�h
        private AbstractGameMode    gameMode;
        //����UI�I�u�W�F�N�g
        [SerializeField]
        private ResultUI            resultUI;
        //BGMClip
        [SerializeField]
        private AudioClip           gameClearBGM;
        [SerializeField]
        private AudioClip           gameOverBGM;

        private void Awake()
        {
            gameMode = FindObjectOfType<AbstractGameMode>();
            Transform parent = GameCanvas.Instance.UILayer[(int)UILayer.System].transform;
            resultUI = Instantiate(resultUI,parent);
        }

        private void Start()
        {
            GameManager.Instance.SetSceneList(SceneList.Result);
            BGMHandler.Instance.SetPlayer(false, false, 0, false);
            switch (GameController.Instance.GameResultType)
            {
                case GameResultType.GameClear:
                    BGMHandler.Instance.ChangeBGM(gameClearBGM);
                    break;
                case GameResultType.GameOver:
                    BGMHandler.Instance.ChangeBGM(gameOverBGM);
                    break;
                default:
                    BGMHandler.Instance.ChangeBGM(gameOverBGM);
                    break;
            }

            resultUI.EnemyTextOutput(gameMode.CurrentEnemyKillCount);

            resultUI.TimeTextOutput(GameUIController.Instance.TimerCountUI?.OutputTimeText());
        }
    }
}
