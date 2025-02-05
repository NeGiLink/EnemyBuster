using MyAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class EndlessMode : AbstractGameMode
    {
        public override bool IsEnd => false;

        public override ModeTag ModeTag => ModeTag.Endless;
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

        private void Start()
        {
            waveChange = true;
        }
    }
}
