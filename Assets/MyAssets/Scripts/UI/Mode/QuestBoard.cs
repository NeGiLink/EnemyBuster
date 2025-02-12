using UnityEngine;

namespace MyAssets
{
    public enum ModeTag
    {
        None = -1,
        AllKillEnemy,
        TimeAttack,
        Endless
    }
    /*
     * ���[�h��UI�ɃA�^�b�`���Ă���N���X
     * �{�^���̃R�[���o�b�N�ŌĂяo���Ďg��
     */
    public class QuestBoard : MonoBehaviour
    {
        //���[�h�^�O
        [SerializeField]
        private new ModeTag     tag;
        //�ݒ肵�����[�h�̓��e��ݒ肷��N���X
        private ModeInformation modeInformation;

        private void Awake()
        {
            modeInformation = GetComponentInChildren<ModeInformation>();
        }
        //�R�[���o�b�N�ŌĂяo���Ďg���N���X
        public void SetInformation(string name)
        {
            tag = ModeTag.None;
            switch (name)
            {
                case "AllKillEnemy":
                    tag = ModeTag.AllKillEnemy;
                    break;
                case "TimeAttack":
                    tag = ModeTag.TimeAttack;
                    break;
                case "Endless":
                    tag = ModeTag.Endless;
                    break;
            }
            GameManager.Instance.SetModeTag(tag);
            modeInformation.SetModeText();
            modeInformation.SetModeInfoText(tag);
        }
    }
}
