using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace MyAssets
{
    public class InputButtonController : MonoBehaviour
    {
        //選択してる場所が分かる画像を有効にするかしないかのフラグ
        [SerializeField]
        private bool activateSelect = true;
        //ボタン操作が横方向か縦方向か設定するフラグ
        [SerializeField]
        private bool horizontal;
        //ボタンが複数あるか1つしかないか判定する
        private bool buttonIsArray = false;
        //選択中の画像
        [SerializeField]
        private Image selectImage;
        //選択してる要素数
        private int selectIndex = 0;
        //選択中の画像をボタン横のどれくらいの位置に設置するか
        [SerializeField]
        private float selectImageOffsetX;
        //子オブジェクトにボタン
        [SerializeField]
        private Button[] buttons;
        private ButtonHover[] hovers;
        //SE再生用クラス
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
                    selectIndex = i;
                    SetSelectImagePosition(selectIndex);
                    SetActivateSelectImage(true);
                    if (InputUIAction.Instance.Decide)
                    {
                        if(selectIndex < 0) { return; }
                        buttons[selectIndex].onClick?.Invoke();
                        //seManager.Play(1);
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
                    //seManager.Play();
                }
                else if (action > 0)
                {
                    selectIndex++;
                    if (selectIndex >= buttons.Length)
                    {
                        selectIndex = 0;
                    }
                    SetSelectImagePosition(selectIndex);
                    //seManager.Play();
                }
            }
            if (InputUIAction.Instance.Decide)
            {
                //if (!InputUIAction.Instance.IsInputGamePad) { return; }
                buttons[selectIndex].onClick?.Invoke();
                //seManager.Play(1);
            }
        }

        public void ActivateStart()
        {
            this.enabled = true;
            //StartCoroutine(Activate());
        }

        private IEnumerator Activate()
        {
            yield return null;
            this.enabled = true;
        }
    }
}
