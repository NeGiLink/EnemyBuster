using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    public enum ButtonSETag
    {
        Select,
        Decide
    }
    public class InputButtonController : MonoBehaviour
    {
        //�I�����Ă�ꏊ��������摜��L���ɂ��邩���Ȃ����̃t���O
        [SerializeField]
        private bool activateSelect = true;
        //�{�^�����삪���������c�������ݒ肷��t���O
        [SerializeField]
        private bool horizontal;
        //�{�^�����������邩1�����Ȃ������肷��
        private bool buttonIsArray = false;
        //�I�𒆂̉摜
        [SerializeField]
        private Image selectImage;
        [SerializeField]
        private bool buttonAndSelectCommon;
        //�I�����Ă�v�f��
        private int selectIndex = 0;
        //�I�𒆂̉摜���{�^�����̂ǂꂭ�炢�̈ʒu�ɐݒu���邩
        [SerializeField]
        private float selectImageOffsetX;
        [SerializeField]
        private float selectImageOffsetY = 0;
        //�q�I�u�W�F�N�g�Ƀ{�^��
        [SerializeField]
        private Button[] buttons;
        private ButtonHover[] hovers;
        //SE�Đ��p�N���X
        private SEHandler seHandler;

        private void Awake()
        {
            Button[] b = GetComponentsInChildren<Button>();
            buttons = b;
            ButtonHover[] h = GetComponentsInChildren<ButtonHover>();
            hovers = h;
            seHandler = GetComponent<SEHandler>();
        }

        private void Start()
        {
            if (buttons.Length > 1)
            {
                buttonIsArray = true;
            }
            else
            {
                buttonIsArray = false;
            }
            selectIndex = 0;
            SetSelectImagePosition(selectIndex);
        }
        private void SetSelectImagePosition(int index)
        {
            if (!activateSelect || selectImage == null) { return; }
            if (!buttonAndSelectCommon)
            {
                Vector2 pos = hovers[index].RectTransform.anchoredPosition;
                pos.x -= selectImageOffsetX;
                pos.y -= selectImageOffsetY;
                selectImage.rectTransform.anchoredPosition = pos;
            }
            else 
            {
                Vector2 pos = hovers[index].RectTransform.anchoredPosition;
                pos.x -= selectImageOffsetX;
                pos.y -= selectImageOffsetY;
                selectImage.rectTransform.anchoredPosition = pos;

                Image buttonImage = hovers[index].GetComponentInChildren<Image>();
                selectImage.rectTransform.sizeDelta = buttonImage.rectTransform.sizeDelta;
            }
        }

        private void SetActivateSelectImage(bool b)
        {
            selectImage.enabled = b;
        }
        private void Update()
        {
            MouseInput();
            GamePadInput();
        }

        private void MouseInput()
        {
            //int index = 0;
            for(int i = 0; i < hovers.Length; i++)
            {
                if (hovers[i].IsHovering)
                {
                    if(selectIndex != i)
                    {
                        seHandler.Play((int)ButtonSETag.Select);
                    }
                    selectIndex = i;
                    SetSelectImagePosition(selectIndex);
                    SetActivateSelectImage(true);
                    if (InputUIAction.Instance.Decide)
                    {
                        if(selectIndex < 0) { return; }
                        buttons[selectIndex].onClick?.Invoke();
                        seHandler.Play((int)ButtonSETag.Decide);
                    }
                }
            }
        }

        private void GamePadInput()
        {
            if (UnityEngine.Input.anyKey) { return; }
            float select;
            if (horizontal)
            {
                select = InputUIAction.Instance.Select.x;
            }
            else
            {
                select = -InputUIAction.Instance.Select.y;
            }
            SelectInput(select);
        }

        private void SelectInput(float action)
        {
            if (buttonIsArray)
            {
                if (action < 0)
                {
                    selectIndex--;
                    if (selectIndex < 0)
                    {
                        selectIndex = buttons.Length - 1;
                    }
                    SetSelectImagePosition(selectIndex);
                    seHandler.Play((int)ButtonSETag.Select);
                }
                else if (action > 0)
                {
                    selectIndex++;
                    if (selectIndex >= buttons.Length)
                    {
                        selectIndex = 0;
                    }
                    SetSelectImagePosition(selectIndex);
                    seHandler.Play((int)ButtonSETag.Select);
                }
            }
            if (InputUIAction.Instance.Decide)
            {
                buttons[selectIndex].onClick?.Invoke();
                seHandler.Play((int)ButtonSETag.Decide);
            }
        }

        public void ActivateStart()
        {
            this.enabled = true;
        }
    }
}
