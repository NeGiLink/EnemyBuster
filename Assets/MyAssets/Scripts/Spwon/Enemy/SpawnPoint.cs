using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// �L�����N�^�[�𐶐�����ʒu�ɒu���Ă�X�|�[���I�u�W�F�N�g�ɃA�^�b�`�������
    /// ���݂͓��ɉ����Ȃ�
    /// </summary>
    public class SpawnPoint : MonoBehaviour
    {
        private bool use = false;
        public bool IsUse => use;
        public void SetUseFlag(bool b) { use = b; }
    }
}
