using UnityEngine;

namespace MyAssets
{
    public abstract class AbstractGameMode : MonoBehaviour
    {
        [SerializeField]
        protected float count;

        [SerializeField]
        protected int maxEnemyKillCount;
        public int MaxEnemyKillCount => maxEnemyKillCount;
        [SerializeField]
        protected int currentEnemyKillCount;
        public int CurrentEnemyKillCount { get { return currentEnemyKillCount; } set { currentEnemyKillCount = value; } }

        public virtual bool IsEnd => currentEnemyKillCount >= maxEnemyKillCount;
        [SerializeField]
        protected int maxWaveChangeCount = 2;
        public int MaxWaveChangeCount => maxWaveChangeCount;

        protected bool waveChange = false;
        public bool WaveChange => waveChange;

        private EnemyKillCountUI enemyKillCountUI;
        public EnemyKillCountUI EnemyKillCountUI => enemyKillCountUI;

        protected SpawnEnemy spawnEnemy;
        public SpawnEnemy SpawnEnemy { get
            {
                if(spawnEnemy == null)
                {
                    spawnEnemy = FindObjectOfType<SpawnEnemy>();
                }
                return spawnEnemy;
            } 
        }

        public virtual ModeTag ModeTag => ModeTag.None;

        protected bool spawnLimit = false;
        public bool SpawnLimit => spawnLimit;

        public abstract void Setup(int maxEnemy,int maxWaveEnemy);

        public abstract void TimerStart();

        public abstract void IsWaveChangeOrEnd();

        public abstract void WaveChangeEnd();
    }
}
