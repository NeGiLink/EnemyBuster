using UnityEngine;

namespace MyAssets
{
    public class TimeAttackMode : AbstractGameMode
    {
        public override ModeTag ModeTag => ModeTag.TimeAttack;

        private Timer enemySpawnCoolDown = new Timer();


        [SerializeField]
        private float easyTime = 360.0f;
        [SerializeField]
        private float normalTime = 180.0f;
        [SerializeField]
        private float hardTime = 90.0f;

        [SerializeField]
        private float easyCoolDown = 5.0f;
        [SerializeField]
        private float normalCoolDown = 2.5f;
        [SerializeField]
        private float hardCoolDown = 1.0f;

        private float coolDown = 0;

        public override void Setup(int maxEnemy, int maxWaveEnemy)
        {
            maxEnemyKillCount = maxEnemy;
            maxWaveChangeCount = maxWaveEnemy;
            spawnLimit = false;
            SetTimerCount();
        }

        private void SetTimerCount()
        {
            GameLevel level = GameManager.Instance.GameLevel;
            switch (level)
            {
                case GameLevel.Easy:
                    count = easyTime;
                    coolDown = easyCoolDown;
                    break;
                case GameLevel.Normal:
                    count = normalTime;
                    coolDown = normalCoolDown;
                    break;
                case GameLevel.Hard:
                    count = hardTime;
                    coolDown = hardCoolDown;
                    break;
            }
        }

        // “G‚ª“|‚³‚ê‚½‚Æ‚«‚ÉŒÄ‚Ño‚³‚ê‚éŠÖ”
        public override void IsWaveChangeOrEnd()
        {
            if (GameController.Instance.GameResultType != GameResultType.Null) { return; }
            if (currentEnemyKillCount >= maxEnemyKillCount)
            {
                GameController.Instance.SetGameResultType(GameResultType.GameClear);
                GameUIController.Instance.CreateFadeResultTextUI();
            }
        }

        public override void WaveChangeEnd()
        {
            enemySpawnCoolDown.Start(coolDown);
            waveChange = false;
        }

        private void ActivateSpawn()
        {
            waveChange = true;
        }

        private void Start()
        {
            waveChange = true;
            TimerStart();
        }
        private void Update()
        {
            enemySpawnCoolDown.Update(Time.deltaTime);
        }

        public override void TimerStart()
        {
            GameController.Instance.TimerStart(count, false);
            GameController.Instance.Timer.OnEnd += OnGameClear;

            enemySpawnCoolDown.OnEnd += ActivateSpawn;
        }

        public void OnGameClear()
        {
            GameController.Instance.SetGameResultType(GameResultType.GameClear);
            GameUIController.Instance.CreateFadeResultTextUI();
            SpawnEnemy.AllEnemyDestroy();
        }
    }
}
