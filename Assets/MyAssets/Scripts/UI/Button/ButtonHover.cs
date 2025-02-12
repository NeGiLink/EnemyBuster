using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyAssets
{
    /*
     * �{�^���ɃA�^�b�`����N���X
     * �}�E�X���{�^���ɐG��Ă��邩���Ȃ����𒲂ׂ�
     */
    public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private RectTransform   rectTransform;
        public RectTransform    RectTransform => rectTransform;
        [SerializeField]
        private bool            isHovering;
        public bool             IsHovering => isHovering;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovering = true;
        }

        private IEnumerator OnEnter()
        {
            yield return null;
            isHovering = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovering = false;
        }

        public void SetHovering(bool h)
        {
            isHovering = h;
        }

        private void OnDisable()
        {
            isHovering = false;
        }
    }
}
