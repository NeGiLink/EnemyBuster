using UnityEngine;

namespace MyAssets
{
    public class GameModeController : MonoBehaviour
    {
        private static GameModeController instance;
        public static GameModeController Instance => instance;

        [SerializeField]
        private AbstractGameMode abstractGameMode;
        public AbstractGameMode AbstractGameMode => abstractGameMode;

        [SerializeField]
        private int[] levelAnotherMaxEnemyCounts;

        private int maxEnemyCount;
        public int MaxEnemyCount => maxEnemyCount;

        [SerializeField]
        private int[] levelAnotherMaxWaveChangeEnemyCounts;

        private int maxWaveChangeCount;
        public int MaxWaveChangeCount => maxWaveChangeCount;


        [SerializeField]
        private GameUIController nextCreateManager;

        private void Awake()
        {
            instance = this;

            abstractGameMode = Instantiate(GameManager.Instance.GameModeLedger[(int)GameManager.Instance.ModeTag]);
        }

        private void Start()
        {
            //TODO : à¯êîÇÕâº
            switch (GameManager.Instance.ModeTag)
            {
                case ModeTag.AllKillEnemy:
                    break;
                case ModeTag.TimeAttack:
                    break;
                case ModeTag.Endless:
                    break;
            }
            maxEnemyCount = levelAnotherMaxEnemyCounts[(int)GameManager.Instance.GameLevel];
            maxWaveChangeCount = levelAnotherMaxWaveChangeEnemyCounts[(int)GameManager.Instance.GameLevel];
            abstractGameMode.Setup(maxEnemyCount, maxWaveChangeCount);
            abstractGameMode.TimerStart();

            SetActive(false);
        }

        public void SetActive(bool a)
        {
            gameObject.SetActive(a);
        }

        public void CreateUIManager()
        {
            Instantiate(nextCreateManager);
        }
    }
}
