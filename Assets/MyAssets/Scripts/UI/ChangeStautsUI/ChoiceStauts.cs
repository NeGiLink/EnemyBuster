using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * ステータス変更UIの選択中のステータスにアタッチしているクラス
     * 選択中のステータスの情報を保持している
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
