using UnityEngine;

namespace MyAssets
{
    /*
     * �_���[�W���󂯂����̃m�b�N�o�b�N�������܂Ƃ߂��C���^�[�t�F�[�X
     */
    public interface IDamagement
    {
        void AddForceMove(Vector3 origin, Vector3 target, float power);
    }
}
