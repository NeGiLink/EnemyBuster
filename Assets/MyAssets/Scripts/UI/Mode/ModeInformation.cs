using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{

    public enum ModeInfoTextTag
    {
        Mode,
        ModeInfo,
        Level
    }
    /*
     * ���[�h�̐ݒ肵�����e��\������N���X
     */
    public class ModeInformation : MonoBehaviour
    {
        [SerializeField]
        private Text[] texts;


        private string timeAttackModeInfo = "�o���邾������\n�G��|��";

        private string escapeModeInfo = "���Ԑ����܂Ő����c��";

        private string endlessModeInfo = "���Ԑ����Ȃ��B\n�킢������";



        private void Start()
        {
            SetModeText();
            SetModeInfoText(ModeTag.AllKillEnemy);
            SetLevelText();
        }
        //���[�h�̖��O��ݒ�
        public void SetModeText()
        {
            texts[(int)ModeInfoTextTag.Mode].text = GameManager.Instance.GetModeText();
        }
        //���[�h�̏ڍׂ�ݒ�
        public void SetModeInfoText(ModeTag tag)
        {
            string text = "";
            switch (tag)
            {
                case ModeTag.AllKillEnemy:
                    text = timeAttackModeInfo;
                    break;
                case ModeTag.TimeAttack:
                    text = escapeModeInfo;
                    break;
                case ModeTag.Endless:
                    text = endlessModeInfo;
                    break;
            }
            texts[(int)ModeInfoTextTag.ModeInfo].text = text;
        }
        //�Q�[���̓�Փx�̃e�L�X�g��ݒ�
        public void SetLevelText()
        {
            texts[(int)ModeInfoTextTag.Level].text = GameManager.Instance.GetGameLevelText();
        }
    }
}
