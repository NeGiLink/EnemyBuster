using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    public class SelectStauts : MonoBehaviour
    {
        [SerializeField]
        private SaveStatusData saveStatusData;

        [SerializeField]
        private Image image;

        [SerializeField]
        private Sprite sprite;

        [SerializeField]
        private StatusType statusType;

        private StatusInfo statusInfo;

        private ChoiceButtonsController choiceButtonsController;

        private void Awake()
        {
            if(image != null)
            {
                sprite = image.sprite;
            }
            choiceButtonsController = GetComponentInParent<ChoiceButtonsController>();
        }
        // Start is called before the first frame update
        void Start()
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
