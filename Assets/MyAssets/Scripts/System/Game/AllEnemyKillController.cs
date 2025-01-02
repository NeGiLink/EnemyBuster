using UnityEngine;

namespace MyAssets
{
    public class AllEnemyKillController : MonoBehaviour
    {
        private static AllEnemyKillController instance;
        public static AllEnemyKillController Instance => instance;

        [SerializeField]
        private int maxEnemyKillCount;
        public int MaxEnemyKillCount => maxEnemyKillCount;
        [SerializeField]
        private int currentEnemyKillCount;
        public int CurrentEnemyKillCount { get { return currentEnemyKillCount; } set { currentEnemyKillCount = value; } }

        public bool IsEnd => currentEnemyKillCount >= maxEnemyKillCount;
        [SerializeField]
        private int maxWaveChangeCount = 2;
        public int MaxWaveChangeCount => maxWaveChangeCount;

        public void IsWaveChange()
        {
            if(currentEnemyKillCount >= maxEnemyKillCount)
            {
                waveEnd = true;
                GameUIController.Instance.CreateFadeResultTextUI();
                return;
            }

            if(0 >= SpawnCount.Count)
            {
                waveChange = true;
            }
        }

        public void WaveChangeEnd()
        {
            if(SpawnCount.Count >= maxWaveChangeCount)
            {
                waveChange = false;
            }
        }

        private bool waveChange = false;
        public bool WaveChange => waveChange;

        private bool waveEnd = false;

        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            waveChange = true;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
