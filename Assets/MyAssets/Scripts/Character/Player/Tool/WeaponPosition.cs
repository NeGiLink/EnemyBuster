using UnityEngine;

namespace MyAssets
{
    public enum WeaponPositionTag
    {
        None = -1,
        Hand,
        Receipt,
        Count
    }
    //����̈ړ��������I�u�W�F�N�g�ɃA�^�b�`���Ďg������̃|�W�V�����N���X
    public class WeaponPosition : MonoBehaviour
    {
        //�^�O�Ŕ���
        [SerializeField]
        private new WeaponPositionTag   tag = WeaponPositionTag.None;
        public WeaponPositionTag        Tag => tag;

        public GameObject               ThisObject => gameObject;
    }
}
