using System.Collections;
using UnityEngine;

namespace MyAssets
{
    /*
     * TODO : ゲームスタートまでの準備を行うクラス
     */
    public class GamePreparation : MonoBehaviour
    {
        [SerializeField]
        private GameController gameController;
        private GameModeController gameModeController;

        [SerializeField]
        private SpawnEnemy spawnEnemy;

        [SerializeField]
        private GameStartTimer gameStartTimer;

        private PlayerCharacterInput playerCharacterInput;

        private void Awake()
        {
            playerCharacterInput = FindObjectOfType<PlayerCharacterInput>();

            StartCoroutine(PreparationCreate());
        }

        private void Start()
        {
            playerCharacterInput.enabled = false;
        }

        private IEnumerator PreparationCreate()
        {
            yield return new WaitForSecondsRealtime(3f);
            GameController controller = Instantiate(gameController);
            gameModeController = controller.GetComponent<GameModeController>();
            spawnEnemy = Instantiate(spawnEnemy);
            spawnEnemy.SetActive(false);
            gameStartTimer = Instantiate(gameStartTimer);
            gameStartTimer.SetGamePreparation(this);
        }

        public void GameStart()
        {
            playerCharacterInput.enabled = true;
            gameModeController.SetActive(true);
            gameModeController.CreateUIManager();
            spawnEnemy.SetActive(true);
            BGMHandler.Instance.SetPlayer(true, false, 0, true);
        }
    }
}
