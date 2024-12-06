using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public enum QuestInformationTag
    {
        None = -1,
        Subjugation,
        Transport,
        Transaction
    }
    public class QuestBoard : MonoBehaviour
    {
        [SerializeField]
        private new QuestInformationTag tag;

        public void SetInformation(string name)
        {
            tag = QuestInformationTag.None;
            switch (name)
            {
                case "Subjugation":
                    tag = QuestInformationTag.Subjugation;
                    break;
                case "Transport":
                    tag = QuestInformationTag.Transport;
                    break;
                case "Transaction":
                    tag = QuestInformationTag.Transaction;
                    break;
            }
            Quest.Instance.SetQuestObject(tag);
        }

        public void DesideQuest()
        {
            Destroy(gameObject);
            Quest.Instance.CreateQuest();
            tag = QuestInformationTag.None;
        }

        private void OnEnable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.ActivatePlayerInput(false);
        }

        private void OnDestroy()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.Instance.ActivatePlayerInput(true);
        }
    }
}
