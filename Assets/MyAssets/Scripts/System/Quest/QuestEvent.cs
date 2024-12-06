using UnityEngine;
using UnityEngine.InputSystem;

namespace MyAssets
{
    public class QuestEvent : MonoBehaviour
    {
        [SerializeField]
        private GameObject questPanel;

        private GameObject keepQuestPanel;

        [SerializeField]
        private UIActivate uIActivate;

        private EventTrigger eventTrigger;

        private void Awake()
        {
            eventTrigger = GetComponent<EventTrigger>();
            uIActivate.SetAwake();
        }

        private void Start()
        {
            uIActivate.Setup();
        }

        private void Update()
        {
            if (eventTrigger.TriggerStay)
            {
                uIActivate.EventUpdate();
            }
            else
            {
                uIActivate.NoEvent();
            }
        }

        public void CreateUI(InputAction.CallbackContext context)
        {
            if(keepQuestPanel != null) { return; }
            GameCanvas canvas = GameCanvas.Instance;
            keepQuestPanel = Instantiate(questPanel, canvas.transform);
        }

        public void Destroy(InputAction.CallbackContext context)
        {
            if (keepQuestPanel == null) { return; }
            Destroy(keepQuestPanel);
        }
    }
}
