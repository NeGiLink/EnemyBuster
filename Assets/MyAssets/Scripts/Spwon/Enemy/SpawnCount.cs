using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// Enemy�I�u�W�F�N�g�ɃA�^�b�`����
    /// �X�|�[���������𐔂��鏈��
    /// </summary>
    public class SpawnCount : MonoBehaviour
    {
        private ItemCreateMachine createMachine;
        public void SetItemCreateMachine(ItemCreateMachine machine) { createMachine = machine; }

        private Vector3 itemPosOffset = new Vector3(0,1,0);

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
            if(transform == null) { return; }
            if(createMachine == null) { return; }
            float ratio = Random.Range(0, 1.1f);
            if(ratio > 0.5f) { return; }
            createMachine.InstRecoveryHealth(transform.position + itemPosOffset,transform.rotation);
        }
    }
}
