using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    public enum ButtonSETag
    {
        Select,
        Decide,
        Decide2
    }
    /*
     * ボタンをコントローラー、マウスで操作するためのクラス
     * ボタンの親オブジェクトにアタッチして使う
     */
    public class InputButtonController : MonoBehaviour
    {
        //選択してる場所が分かる画像を有効にするかしないかのフラグ
        [SerializeField]
        private bool            activateSelect = true;
        //画像の大きさに設定するかボタンのサイズに合わせるかのフラグ
        [SerializeField]
        private bool            nativeSize = true;
        [SerializeField]
        private bool            selectsImage;
        [SerializeField]
        [Range(0,100)]
        private int             selectImageChangeCount = 0;
        //選択中の画像
        [SerializeField]
        private Image           selectImage;
        [SerializeField]
        private Sprite[]        selectSprites;
        //選択してる要素数
        private int             selectIndex = 0;
        //選択中の画像をボタン横のどれくらいの位置に設置するか
        [SerializeField]
        private float           selectImageOffsetX;
        [SerializeField]
        private float           selectImageOffsetY = 0;
        //子オブジェクトにボタン
        [SerializeField]
        private Button[]        buttons;
        private ButtonHover[]   hovers;
        //SE再生用クラス
        private SEHandler       seHandler;

        private bool            decideFlag;
        private void Awake()
        {
            Button[] b = GetComponentsInChildren<Button>();
            buttons = b;
            ButtonHover[] h = GetComponentsInChildren<ButtonHover>();
            hovers = h;


            seHandler = GetComponent<SEHandler>();
            if(seHandler == null)
            {
                seHandler = GetComponentInParent<SEHandler>();
            }
        }

        private void Start()
        {
            decideFlag = false;
            selectIndex = 0;
            SetSelectImagePosition(selectIndex);
        }
        private void SetSelectImagePosition(int index)
        {
            if (!activateSelect || selectImage == null) { return; }
            if(index < 0) {  return; }
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
            if (nativeSize)
            {
                selectImage.SetNativeSize();
            }
            else
            {
                selectImage.rectTransform.sizeDelta = hovers[index].RectTransform.sizeDelta;
            }
            selectImage.rectTransform.localScale = hovers[index].RectTransform.localScale;
        }

        private void SetActivateSelectImage(bool b)
        {
            selectImage.enabled = b;
        }
        private void Update()
        {
            if(InputManager.GetDeviceInput() == DeviceInput.Key)
            {
                MouseInput();
            }
            else if(InputManager.GetDeviceInput() == DeviceInput.Controller)
            {
                GamePadInput();
            }
        }

        private void MouseInput()
        {
            for(int i = 0; i < hovers.Length; i++)
            {
                if (hovers[i].IsHovering)
                {
                    if(selectIndex != i)
                    {
                        seHandler.Play((int)ButtonSETag.Select);
                        selectIndex = i;
                        SetSelectImagePosition(selectIndex);
                        SetActivateSelectImage(true);
                    }
                }
            }
        }

        private void GamePadInput()
        {
            float select;
            if(InputUIAction.Instance.Select.x != 0 || InputUIAction.Instance.Select.y != 0)
            {
                Vector2 selectVec2 = InputUIAction.Instance.Select;
                SelectVec2Input(selectVec2);
            }
            if (InputUIAction.Instance.Decide)
            {
                EnumeratorDecide();
            }
        }

        private void SelectVec2Input(Vector2 select)
        {
            int currentIndex = selectIndex;
            List<int> selectIndexs = new List<int>();

            int decideIndex = -1;
            for (int i = 0;i < hovers.Length; i++)
            {
                if(select.x > 0)
                {
                    if(hovers[currentIndex].RectTransform.anchoredPosition.x < hovers[i].RectTransform.anchoredPosition.x)
                    {
                        decideIndex = CheckDecideIndex(currentIndex, decideIndex, i);
                    }
                }
                else if (select.x < 0)
                {
                    if (hovers[currentIndex].RectTransform.anchoredPosition.x > hovers[i].RectTransform.anchoredPosition.x)
                    {
                        decideIndex = CheckDecideIndex(currentIndex, decideIndex, i);
                    }
                }

                if (select.y > 0)
                {
                    if (hovers[currentIndex].RectTransform.anchoredPosition.y < hovers[i].RectTransform.anchoredPosition.y)
                    {
                        decideIndex = CheckDecideIndex(currentIndex, decideIndex, i);
                    }
                }
                else if (select.y < 0)
                {
                    if (hovers[currentIndex].RectTransform.anchoredPosition.y > hovers[i].RectTransform.anchoredPosition.y)
                    {
                        decideIndex = CheckDecideIndex(currentIndex, decideIndex, i);
                    }
                }
            }
            if(decideIndex < 0) { return; }
            selectIndex = decideIndex;
            SetSelectImagePosition(selectIndex);
        }

        private int CheckDecideIndex(int currentNum,int decideNum,int newNum)
        {
            if(decideNum < 0) { return newNum; }
            Vector2 currentSub = hovers[currentNum].RectTransform.anchoredPosition - hovers[decideNum].RectTransform.anchoredPosition;
            Vector2 newSub = hovers[currentNum].RectTransform.anchoredPosition - hovers[newNum].RectTransform.anchoredPosition;
            
            if(Mathf.Abs(currentSub.magnitude) > Mathf.Abs(newSub.magnitude))
            {
                return newNum;
            }
            return decideNum;
        }

        private void EnumeratorDecide()
        {
            if (decideFlag) { return; }
            StartCoroutine(DecideUpdate());
        }

        private IEnumerator DecideUpdate()
        {
            decideFlag = true;
            yield return null;
            buttons[selectIndex].onClick?.Invoke();
            decideFlag = false;
        }

        public void ActivateStart()
        {
            this.enabled = true;
        }
    }
}
