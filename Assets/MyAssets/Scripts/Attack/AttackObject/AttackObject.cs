using UnityEngine;

namespace MyAssets
{
    public enum DamageType
    {
        None = -1,
        Small,
        Middle,
        Big
    }
    /*
     * �U���I�u�W�F�N�g�̃p�����[�^�[���܂Ƃ߂��N���X
     * �p���[�A�m�b�N�o�b�N�A�_���[�W�̃^�C�v���܂Ƃ߂�����
     */
    public class AttackObject : MonoBehaviour
    {
        //�X�N���v�^�u���I�u�W�F�N�g
        [SerializeField]
        private AttackData          data;
        //�X�N���v�^�u���I�u�W�F�N�g�̒��g�������Ȃ炻�̔z��̗v�f���Ɏg��
        [SerializeField]
        private int                 attackTypeCount = 0;
        //�p���[
        public int Power =>         data.AttackDataInfo[attackTypeCount].power;
        //�m�b�N�o�b�N
        public float KnockBack =>   data.AttackDataInfo[attackTypeCount].knockBack;
        //�m�b�N�o�b�N���Ƀm�b�N�o�b�N���x��
        public DamageType Type =>   data.AttackDataInfo[attackTypeCount].attackType;
        //�v�f���ύX�֐�
        public void SetAttackTypeCount(int c) { attackTypeCount = c; }
    }
}
