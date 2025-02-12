using UnityEngine;

namespace MyAssets
{
    public enum StageType
    {
        Stage00,
        Stage01,

        Count
    }
    /*
     * �X�N���v�^�u���I�u�W�F�N�g�ɐݒ肳��Ă���X�e�[�W�I�u�W�F�N�g�������_���ɐ�������
     */
    public class StageCreater : MonoBehaviour
    {
        [SerializeField]
        private StageLedger     stageLedger;

        private GameObject      stageObject;
        //�Q�[�����n�܂����珈���J�n
        private void Start()
        {
            int index = Random.Range(0, (int)StageType.Count);
            stageObject = stageLedger[index];

            Instantiate(stageObject);

            Destroy(gameObject);
        }
    }
}
