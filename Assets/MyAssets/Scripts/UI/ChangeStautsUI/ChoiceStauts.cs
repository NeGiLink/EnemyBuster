using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * �X�e�[�^�X�ύXUI�̑I�𒆂̃X�e�[�^�X�ɃA�^�b�`���Ă���N���X
     * �I�𒆂̃X�e�[�^�X�̏���ێ����Ă���
     */
    public class ChoiceStauts : MonoBehaviour
    {
        [SerializeField]
        private SaveStatusData  saveStatusData;
        public SaveStatusData   SaveStatusData => saveStatusData;

        [SerializeField]
        private Image           choiceImage;
        public Image            ChoiceImage => choiceImage;
        [SerializeField]
        private bool            choice = false;


        [SerializeField]
        private StatusType      statusType;

        private StatusInfo      statusInfo;
        public void SetStatus(StatusInfo info,SaveStatusData data)
        {
            choiceImage.sprite = info.sprite;
            statusType = info.Type;
            saveStatusData.type = info.Type;
            statusInfo = info;
            choice = true;

            saveStatusData = data;
        }

        public void ChoiceAction()
        {
            if (choice)
            {
                choiceImage.sprite = null;
                saveStatusData.ResetData();
                statusType = StatusType.Null;
                choice = false;
            }
        }
    }
}
