using UnityEngine;
using UnityEngine.EventSystems;

namespace MyAssets
{
    public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private RectTransform rectTransform;
        public RectTransform RectTransform => rectTransform;
        [SerializeField]
        private bool isHovering;
        public bool IsHovering => isHovering;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovering = true;
            //Debug.Log("カーソルがボタンに乗りました: " + gameObject.name);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovering = false;
            //Debug.Log("カーソルがボタンから離れました: " + gameObject.name);
        }

        public void SetHovering(bool h)
        {
            isHovering = h;
        }
        /*
        void Update()
        {
            if (isHovering)
            {
                Debug.Log("カーソルがまだボタンの上にあります");
            }
        }
         */
    }
}
