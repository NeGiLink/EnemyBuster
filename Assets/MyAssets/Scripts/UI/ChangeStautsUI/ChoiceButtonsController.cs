using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    public enum StatusType
    {
        Null = -1,
        HP,
        SP,
        Speed,
        Power,
        Defense
    }
    public class ChoiceButtonsController : MonoBehaviour
    {
        [SerializeField]
        private ChoiceStauts[] choiceStautsButton;

        [SerializeField]
        private SelectStauts[] selectStautsButton;

        [SerializeField]
        private StatusData statusData;

        private SaveManager saveManager;

        private bool active = false;

        private void Awake()
        {
            choiceStautsButton = GetComponentsInChildren<ChoiceStauts>();
            selectStautsButton = GetComponentsInChildren<SelectStauts>();
            saveManager = FindObjectOfType<SaveManager>();
        }

        private void Start()
        {
            List<SaveStatusData> datas = saveManager.LoadGame();
            int num = 0;
            for(int i = 0; i < choiceStautsButton.Length; i++)
            {
                num = (int)datas[i].type;
                if (datas[i].type == StatusType.Null)
                {
                    num = statusData.StatusInfos.Length - 1;
                }
                choiceStautsButton[i].SetStatus(statusData.StatusInfos[num]);
            }
            for(int i = 0; i < selectStautsButton.Length; i++)
            {
                selectStautsButton[i].SetStatusData(statusData.StatusInfos[i]);
            }
        }

        public void SetChoiceStauts(StatusInfo info,SaveStatusData data)
        {
            if (active) { return; }
            StartCoroutine(ChoiceStauts(info,data));
        }

        private IEnumerator ChoiceStauts(StatusInfo info,SaveStatusData data)
        {
            for (int i = 0; i < choiceStautsButton.Length; i++)
            {
                if (choiceStautsButton[i].ChoiceImage.sprite != null)
                {
                    continue;
                }
                choiceStautsButton[i].SetStatus(info);
                choiceStautsButton[i].SaveStatusData.ChangeData(data);
                break;
            }
            active = true;
            yield return new WaitForSecondsRealtime(0.1f);
            active = false;
        }

        public void OnDisable()
        {
            List<SaveStatusData> datas = new List<SaveStatusData>();
            for(int i = 0; i < choiceStautsButton.Length; i++)
            {
                SaveStatusData data = choiceStautsButton[i].SaveStatusData;
                datas.Add(data);
            }
            saveManager.Save(datas);
        }
    }
}
