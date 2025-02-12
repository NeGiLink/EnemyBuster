using UnityEngine;

namespace MyAssets
{
    /*
     * ���⏂�̑��������N���X�̃C���^�[�t�F�[�X
     */
    public interface IEquipment
    {
        ShieldController    ShieldTool {  get; }

        GameObject          HaveWeapon {  get; }
        void SetInWeapon();
        void SetOutWeapon();
    }
}
