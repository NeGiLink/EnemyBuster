using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// 敵を生成するだけのクラス
    /// </summary>
    public class SpawnEnemy : MonoBehaviour
    {
        [SerializeField]
        private EnemyLedger enemyLedger;

        [SerializeField]
        private int maxSpawnCount;

        [SerializeField]
        private SpawnPoint[] spawnPoint;


        private void Awake()
        {
            spawnPoint = GetComponentsInChildren<SpawnPoint>();
        }

        private void Start()
        {
        
        }


        private void Update()
        {
            if (!GameManager.Instance.Debug)
            {
                DoUpdate();
            }
            else
            {
                DebugUpdate();
            }

        }

        private void DoUpdate()
        {
            if (GameModeController.Instance.AbstractGameMode.IsEnd) { return; }
            Spawn();
        }

        private int SelectSpawnPoint()
        {
            // 値を格納するリストを作成
            List<int> numbers = new List<int>();

            // 指定された範囲の数値をリストに追加
            for (int i = 0; i < spawnPoint.Length; i++)
            {
                if (!spawnPoint[i].IsUse) // 除外値をスキップ
                {
                    numbers.Add(i);
                }
            }

            // リストからランダムな値を選択
            int randomIndex = Random.Range(0, numbers.Count);
            return numbers[randomIndex];
        }

        private void Spawn()
        {
            if (!GameModeController.Instance.AbstractGameMode.WaveChange) { return; }
            int enemyIndex = Random.Range(0,(int)EnemyTag.Count);
            //スポーンポイントを設定
            int spawnIndex = SelectSpawnPoint();
            Vector3 pos = spawnPoint[spawnIndex].SpawnPositionOutput();
            //スポーン
            CharacterBaseController enemy = Instantiate(enemyLedger[enemyIndex], pos, spawnPoint[spawnIndex].transform.rotation);
            //カウント
            SpawnCount count = enemy.gameObject.AddComponent<SpawnCount>();
            count.SetSpawnPoint(spawnPoint[spawnIndex]);
        }

        [SerializeField]
        private EnemyTag enemyTag;

        [SerializeField]
        private bool debugSpawn = false;

        private void DebugUpdate()
        {
            if (!debugSpawn) { return; }
            DebugSpawn();
        }
        private void DebugSpawn()
        {
            int enemyIndex = (int)enemyTag;
            int spawnIndex = SelectSpawnPoint();
            CharacterBaseController enemy = Instantiate(enemyLedger[enemyIndex], spawnPoint[spawnIndex].transform.position, spawnPoint[spawnIndex].transform.rotation);
            SpawnCount count = enemy.gameObject.AddComponent<SpawnCount>();
            count.SetSpawnPoint(spawnPoint[spawnIndex]);
            debugSpawn = false;
        }
    }
}
