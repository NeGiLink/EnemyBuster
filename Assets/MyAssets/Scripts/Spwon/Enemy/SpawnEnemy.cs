using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// �G�𐶐����邾���̃N���X
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
            // �l���i�[���郊�X�g���쐬
            List<int> numbers = new List<int>();

            // �w�肳�ꂽ�͈͂̐��l�����X�g�ɒǉ�
            for (int i = 0; i < spawnPoint.Length; i++)
            {
                if (!spawnPoint[i].IsUse) // ���O�l���X�L�b�v
                {
                    numbers.Add(i);
                }
            }

            // ���X�g���烉���_���Ȓl��I��
            int randomIndex = Random.Range(0, numbers.Count);
            return numbers[randomIndex];
        }

        private void Spawn()
        {
            if (!GameModeController.Instance.AbstractGameMode.WaveChange) { return; }
            int enemyIndex = Random.Range(0,(int)EnemyTag.Count);
            //�X�|�[���|�C���g��ݒ�
            int spawnIndex = SelectSpawnPoint();
            Vector3 pos = spawnPoint[spawnIndex].SpawnPositionOutput();
            //�X�|�[��
            CharacterBaseController enemy = Instantiate(enemyLedger[enemyIndex], pos, spawnPoint[spawnIndex].transform.rotation);
            //�J�E���g
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
