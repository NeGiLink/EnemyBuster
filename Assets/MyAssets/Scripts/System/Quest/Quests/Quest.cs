using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class Quest : MonoBehaviour
    {
        private static Quest instance;
        public static Quest Instance => instance;

        private bool subjugation = false;
        public void NotSubjugation()
        {
            subjugation = false;
        }

        [SerializeField]
        private GameObject questType01;

        [SerializeField]
        private GameObject questObject;

        [SerializeField]
        private QuestPointData questPointData;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        public void SetQuestObject(ModeTag tag)
        {
            switch (tag)
            {
                case ModeTag.AllKillEnemy:
                    questObject = questType01;
                    break;
                case ModeTag.TimeAttack:
                    break;
                case ModeTag.Endless:
                    break;
            }
        }

        public void CreateQuest()
        {
            if (subjugation) { return; }
            int index = Random.Range(0, questPointData.Points.Count);
            Vector3 rot = questPointData.Points[index].rotation;
            Quaternion rotQuat = Quaternion.Euler(rot.x, rot.y, rot.z);
            Instantiate(questObject, questPointData.Points[index].position, rotQuat);
            questObject = null;
            subjugation = true;
        }
    }
}
