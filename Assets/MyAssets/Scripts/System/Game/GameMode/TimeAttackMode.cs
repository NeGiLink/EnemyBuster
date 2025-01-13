using UnityEngine;

namespace MyAssets
{
    public class TimeAttackMode : AbstractGameMode
    {

        [SerializeField]
        private float easyTime = 360.0f;
        [SerializeField]
        private float normalTime = 180.0f;
        [SerializeField]
        private float hardTime = 90.0f;

        private bool waveEnd = false;

        public override void Setup(int maxEnemy, int maxWaveEnemy)
        {
            maxEnemyKillCount = maxEnemy;
            maxWaveChangeCount = maxWaveEnemy;
            SetTimerCount();
        }

        private void SetTimerCount()
        {
            GameLevel level = GameManager.Instance.GameLevel;
            switch (level)
            {
                case GameLevel.Easy:
                    count = easyTime;
                    break;
                case GameLevel.Normal:
                    count = normalTime;
                    break;
                case GameLevel.Hard:
                    count = hardTime;
                    break;
            }
        }

        // “G‚ª“|‚³‚ê‚½‚Æ‚«‚ÉŒÄ‚Ño‚³‚ê‚éŠÖ”
        public override void IsWaveChangeOrEnd()
        {
            if (GameController.Instance.GameResultType != GameResultType.Null) { return; }
            if (currentEnemyKillCount >= maxEnemyKillCount)
            {
                waveEnd = true;
                GameController.Instance.SetGameResultType(GameResultType.GameClear);
                GameUIController.Instance.CreateFadeResultTextUI();
                return;
            }

            if (0 >= SpawnCount.Count)
            {
                waveChange = true;
            }
        }

        public override void WaveChangeEnd()
        {
            if (SpawnCount.Count >= maxWaveChangeCount)
            {
                waveChange = false;
            }
        }

        private bool end = false;

        private void Start()
        {
            waveChange = true;
            GameController.Instance.Timer.OnEnd += OnGameOver;
            TimerStart();
        }

        public override void TimerStart()
        {
            GameController.Instance.TimerStart(count, false);
        }

        private void OnGameOver()
        {
            GameController.Instance.SetGameResultType(GameResultType.GameOver);
        }
    }
}
