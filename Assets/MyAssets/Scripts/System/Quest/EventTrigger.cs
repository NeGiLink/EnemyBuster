using UnityEngine;

namespace MyAssets
{
    public class EventTrigger : MonoBehaviour
    {
        [SerializeField]
        private string triggerTag;

        private Transform otherTransform;
        public Transform OtherTransform => otherTransform;

        private bool triggerStay = false;
        public bool TriggerStay => triggerStay;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag != triggerTag) { return; }
            EventKeyActivate eventActivate = gameObject.AddComponent<EventKeyActivate>();
            otherTransform = other.transform;
            QuestEvent questEvent = GetComponent<QuestEvent>();

            eventActivate.SetActionPerformed(questEvent.CreateUI, questEvent.Destroy,true);
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.tag != triggerTag) {return; }
            triggerStay = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.tag != triggerTag) { return; }
            EventKeyActivate eventActivate = gameObject.GetComponent<EventKeyActivate>();
            QuestEvent questEvent = GetComponent<QuestEvent>();
            eventActivate.SetActionPerformed(questEvent.CreateUI, questEvent.Destroy, false);
            Destroy(eventActivate);
            otherTransform = null;
            triggerStay = false;
        }
    }
}
