using UnityEngine;

namespace MyAssets
{
    public class GameController : MonoBehaviour
    {
        private static GameController instance;
        public static GameController Instance => instance;

        [SerializeField]
        private int maxEnemyKillCount;
        [SerializeField]
        private int currentEnemyKillCount;
        public int CurrentEnemyKillCount { get { return currentEnemyKillCount; }set { currentEnemyKillCount = value; } }

        public bool IsEnd => currentEnemyKillCount >= maxEnemyKillCount;

        private void Awake()
        {
            instance = this;
        }


        private void Update()
        {
        
        }
    }
}
