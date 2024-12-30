using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// Enemyオブジェクトにアタッチする
    /// スポーンした数を数える処理
    /// </summary>
    public class SpawnCount : MonoBehaviour
    {
        public static int Count { get; private set; }

        private SpawnPoint spawnPoint;
        public void SetSpawnPoint(SpawnPoint point)
        {
            spawnPoint = point;
            spawnPoint.SetUseFlag(true);
        }

        private void Start()
        {
            Count++;
        }

        private void OnDestroy()
        {
            Count--;
            spawnPoint.SetUseFlag(false);
            GameController.Instance.CurrentEnemyKillCount++;
        }
    }
}
