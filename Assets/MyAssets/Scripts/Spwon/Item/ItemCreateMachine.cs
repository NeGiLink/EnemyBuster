using UnityEngine;

namespace MyAssets
{
    /*
     * �A�C�e�����I�u�W�F�N�g�v�[���Ő�������N���X
     */
    public class ItemCreateMachine : MonoBehaviour
    {
        //�A�C�e��
        [SerializeField]
        private RecoveryHealth  health;

        //item��ێ��i�v�[�����O�j�����̃I�u�W�F�N�g
        private Transform       items;


        private void Start()
        {
            //�A�C�e����ێ������̃I�u�W�F�N�g�𐶐�
            items = new GameObject("ItemPool").transform;
            items.SetParent(transform);
        }

        /// <summary>
        /// �A�C�e�������֐�
        /// </summary>
        /// <param name="pos">�����ʒu</param>
        /// <param name="rotation">�������̉�]</param>
        public RecoveryHealth InstRecoveryHealth(Vector3 pos, Quaternion rotation)
        {
            //�A�N�e�B�u�łȂ��I�u�W�F�N�g��bullets�̒�����T��
            foreach (Transform t in items)
            {
                if (!t.gameObject.activeSelf)
                {
                    //��A�N�e�B�u�ȃI�u�W�F�N�g�̈ʒu�Ɖ�]��ݒ�
                    t.SetPositionAndRotation(pos, rotation);
                    //�A�N�e�B�u�ɂ���
                    t.gameObject.SetActive(true);
                    return t.GetComponent<RecoveryHealth>();
                }
            }
            //��A�N�e�B�u�ȃI�u�W�F�N�g���Ȃ��ꍇ�V�K����

            //�������ɃA�C�e���̎q�I�u�W�F�N�g�ɂ���
            return Instantiate(health, pos, rotation, items);
        }
    }
}
