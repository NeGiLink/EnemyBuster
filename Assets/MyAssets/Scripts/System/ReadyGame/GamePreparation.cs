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

        private PlayerActionInput playerCharacterInput;

        private void Awake()
        {
            playerCharacterInput = FindObjectOfType<PlayerActionInput>();

            StartCoroutine(PreparationCreate());
        }

        private void Start()
        {
            playerCharacterInput.enabled = false;
            InputManager.SetInputStop(true);
        }

        private IEnumerator PreparationCreate()
        {
            yield return new WaitForSecondsRealtime(3f);
            GameController controller = Instantiate(gameController);
            gameModeController = controller.GetComponent<GameModeController>();
            gameStartTimer = Instantiate(gameStartTimer);
            gameStartTimer.SetGamePreparation(this);
            spawnEnemy = Instantiate(spawnEnemy);
            spawnEnemy.SetActive(false);
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
