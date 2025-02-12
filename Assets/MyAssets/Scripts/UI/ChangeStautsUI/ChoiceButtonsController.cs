using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     *�X�e�[�^�X�̃^�O
     *���̃^�O�ŃX�e�[�^�X�ǉ����̔��ʂ��s���Ă��܂��B
     *�V�[���F"01_SelectScene"�̃X�e�[�^�X�ύX�{�^�����莞��
     *��������郁�j���[�Ɏg�p
     */

    public enum StatusType
    {
        Null = -1,
        HP,
        SP,
        Speed,
        Power,
        Defense
    }
    /*
     * �X�e�[�^�X�ǉ����j���[�ɃA�^�b�`���Ă���X�e�[�^�X�ǉ��N���X
     */
    public class ChoiceButtonsController : MonoBehaviour
    {
        //���荀�ڂ̃{�^���z��
        [SerializeField]
        private ChoiceStauts[]  choiceStautsButton;
        //�I�����ڂ̃{�^���z��
        [SerializeField]
        private SelectStauts[]  selectStautsButton;

        [SerializeField]
        private StatusData      statusData;
        //�Z�[�u�N���X
        private SaveManager     saveManager;

        private bool            active = false;

        private void Awake()
        {
            choiceStautsButton = GetComponentsInChildren<ChoiceStauts>();
            selectStautsButton = GetComponentsInChildren<SelectStauts>();
            saveManager = FindObjectOfType<SaveManager>();
        }

        private void Start()
        {
            List<SaveStatusData> datas = saveManager.LoadGame();
            if(datas.Count <= 0){ return; }

            int num = 0;
            for(int i = 0; i < choiceStautsButton.Length; i++)
            {
                num = (int)datas[i].type;
                if (datas[i].type == StatusType.Null)
                {
                    num = statusData.StatusInfos.Length - 1;
                }
                choiceStautsButton[i].SetStatus(statusData.StatusInfos[num], datas[i]);
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
                choiceStautsButton[i].SetStatus(info,data);
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
