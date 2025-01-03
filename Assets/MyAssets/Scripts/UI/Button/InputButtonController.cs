using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
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
        //�I�����Ă�v�f��
        private int selectIndex = 0;
        //�I�𒆂̉摜���{�^�����̂ǂꂭ�炢�̈ʒu�ɐݒu���邩
        [SerializeField]
        private float selectImageOffsetX;
        //�q�I�u�W�F�N�g�Ƀ{�^��
        [SerializeField]
        private Button[] buttons;
        private ButtonHover[] hovers;
        //SE�Đ��p�N���X
        //private SEManager seManager;

        private void Awake()
        {
            Button[] b = GetComponentsInChildren<Button>();
            buttons = b;
            ButtonHover[] h = GetComponentsInChildren<ButtonHover>();
            hovers = h;
            //seManager = GetComponent<SEManager>();
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
            Vector2 pos = hovers[index].RectTransform.anchoredPosition;
            pos.x -= selectImageOffsetX;
            selectImage.rectTransform.anchoredPosition = pos;
        }
        private void Update()
        {
            MouseInput();
            KeyAndGamePadInput();
        }

        private void MouseInput()
        {
            for(int i = 0; i < hovers.Length; i++)
            {
                if (hovers[i].IsHovering)
                {
                    selectIndex = i;
                    SetSelectImagePosition(selectIndex);
                }
            }
        }

        private void KeyAndGamePadInput()
        {
            float select;
            if (horizontal)
            {
                select = InputUIAction.Instance.Select.x;
            }
            else
            {
                select = -InputUIAction.Instance.Select.y;
            }
            Input(select);
        }

        private void Input(float action)
        {
            if (buttonIsArray)
            {
                if (action < 0)
                {
                    selectIndex--;
                    if (selectIndex < 0)
                    {
                        selectIndex = 0;
                    }
                    SetSelectImagePosition(selectIndex);
                    //seManager.Play();
                }
                else if (action > 0)
                {
                    selectIndex++;
                    if (selectIndex >= buttons.Length)
                    {
                        selectIndex = buttons.Length - 1;
                    }
                    SetSelectImagePosition(selectIndex);
                    //seManager.Play();
                }
            }

            if (InputUIAction.Instance.Decide)
            {
                buttons[selectIndex].onClick.Invoke();
                //seManager.Play(1);
            }
        }
    }
}
