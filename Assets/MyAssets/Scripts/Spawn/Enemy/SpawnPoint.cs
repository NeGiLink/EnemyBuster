using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// �L�����N�^�[�𐶐�����ʒu�ɒu���Ă�X�|�[���I�u�W�F�N�g�ɃA�^�b�`�������
    /// ���݂͓��ɉ����Ȃ�
    /// </summary>
    public class SpawnPoint : MonoBehaviour
    {
        private bool        use = false;
        public bool         IsUse => use;

        [SerializeField]
        private LayerMask   targetLayer;
        public void SetUseFlag(bool b) { use = b; }

        public Vector3 SpawnPositionOutput()
        {
            Vector3 pos = transform.position;
            Ray ray = new Ray(transform.position,-transform.up);
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(ray, out hit, 5, targetLayer);
            pos.y = hit.point.y;
            return pos;
        }
    }
}
