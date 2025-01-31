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
        //選択してる場所が分かる画像を有効にするかしないかのフラグ
        [SerializeField]
        private bool activateSelect = true;
        //ボタン操作が横方向か縦方向か設定するフラグ
        [SerializeField]
        private bool horizontal;
        //ボタンが複数あるか1つしかないか判定する
        private bool buttonIsArray = false;
        [SerializeField]
        private bool selectsImage;
        [SerializeField]
        [Range(0,100)]
        private int selectImageChangeCount = 0;
        //選択中の画像
        [SerializeField]
        private Image selectImage;
        [SerializeField]
        private Sprite[] selectSprites;
        //選択してる要素数
        private int selectIndex = 0;
        //選択中の画像をボタン横のどれくらいの位置に設置するか
        [SerializeField]
        private float selectImageOffsetX;
        [SerializeField]
        private float selectImageOffsetY = 0;
        //子オブジェクトにボタン
        [SerializeField]
        private Button[] buttons;
        private ButtonHover[] hovers;
        //SE再生用クラス
        private SEHandler seHandler;

        private bool decideFlag;
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
            decideFlag = false;
            selectIndex = 0;
            SetSelectImagePosition(selectIndex);
        }
        private void SetSelectImagePosition(int index)
        {
            if (!activateSelect || selectImage == null) { return; }
            if (!selectsImage)
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
                if(index < selectImageChangeCount)
                {
                    selectImage.sprite = selectSprites[0];
                }
                else
                {
                    selectImage.sprite = selectSprites[1];
                }
            }
            selectImage.rectTransform.localScale = hovers[index].RectTransform.localScale;
            selectImage.rectTransform.sizeDelta = hovers[index].RectTransform.sizeDelta;
            selectImage.SetNativeSize();
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
                        seHandler.Play((int)ButtonSETag.Decide);
                        EnumeratorDecide();
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
                    seHandler.Play((int)ButtonSETag.Select);
                    SetSelectImagePosition(selectIndex);
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
                EnumeratorDecide();
                seHandler.Play((int)ButtonSETag.Decide);
            }
        }

        private void EnumeratorDecide()
        {
            if (decideFlag) { return; }
            StartCoroutine(DecideUpdate());
        }

        private IEnumerator DecideUpdate()
        {
            decideFlag = true;
            yield return new WaitForSecondsRealtime(1.0f);
            buttons[selectIndex].onClick?.Invoke();
        }

        public void ActivateStart()
        {
            this.enabled = true;
        }
    }
}
