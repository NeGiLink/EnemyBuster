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

            GameModeController.Instance.AbstractGameMode.WaveChangeEnd();
        }

        private void OnDisable()
        {
            Count--;
            spawnPoint.SetUseFlag(false);
            GameModeController.Instance.AbstractGameMode.CurrentEnemyKillCount++;
            GameUIController.Instance.EnemyKillCountUI.CountRefresh(GameModeController.Instance.AbstractGameMode.CurrentEnemyKillCount);
            GameModeController.Instance.AbstractGameMode.IsWaveChangeOrEnd();
        }
    }
}
