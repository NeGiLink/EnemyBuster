using UnityEngine;

namespace MyAssets
{
    public class AllEnemyKillController : AbstractGameMode
    {
        public override ModeTag ModeTag => ModeTag.AllKillEnemy;

        public override void Setup(int maxEnemy, int maxWaveEnemy)
        {
            maxEnemyKillCount = maxEnemy;
            maxWaveChangeCount = maxWaveEnemy;
            spawnLimit = true;
        }

        public override void TimerStart()
        {
            GameController.Instance.TimerStart(count,true);
        }

        public override void IsWaveChangeOrEnd()
        {
            if(currentEnemyKillCount >= maxEnemyKillCount)
            {
                GameController.Instance.SetGameResultType(GameResultType.GameClear);
                GameUIController.Instance.CreateFadeResultTextUI();
                SpawnEnemy.AllEnemyDestroy();
                return;
            }

            if(0 >= SpawnCount.Count)
            {
                waveChange = true;
            }
        }

        public override void WaveChangeEnd()
        {
            if(SpawnCount.Count >= maxWaveChangeCount)
            {
                waveChange = false;
            }
        }

        private void Start()
        {
            waveChange = true;
        }
    }
}
