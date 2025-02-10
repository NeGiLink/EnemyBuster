using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * �I���{�^���ɃA�^�b�`����N���X
     * �V�[���F"01_SelectScene"�̃X�e�[�^�X�ύX�{�^�����莞��
     * ��������郁�j���[�Ɏg�p
     */
    public class SelectStauts : MonoBehaviour
    {
        [SerializeField]
        private SaveStatusData          saveStatusData;

        [SerializeField]
        private Image                   image;

        [SerializeField]
        private Sprite                  sprite;

        [SerializeField]
        private StatusType              statusType;

        private StatusInfo              statusInfo;

        private ChoiceButtonsController choiceButtonsController;

        private void Awake()
        {
            if(image != null)
            {
                sprite = image.sprite;
            }
            choiceButtonsController = GetComponentInParent<ChoiceButtonsController>();
        }

        private void Start()
        {
        
        }

        public void SetStatusData(StatusInfo data)
        {
            sprite = data.sprite;
            statusType = data.Type;
            saveStatusData.type = data.Type;
            statusInfo = data;
        }

        public void SetStauts()
        {
            choiceButtonsController.SetChoiceStauts(statusInfo,saveStatusData);
        }
    }
}
