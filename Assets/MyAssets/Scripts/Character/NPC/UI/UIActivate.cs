using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class UIActivate : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Sprite sprite;

        [SerializeField]
        private LockAtUI lockAtUI;

        public void SetAwake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            lockAtUI.Setup(transform);
        }

        public void Setup()
        {
            spriteRenderer.sprite = null;
        }

        public void EventUpdate()
        {
            spriteRenderer.sprite = sprite;
            lockAtUI.DoUpdate();
        }

        public void NoEvent()
        {
            spriteRenderer.sprite = null;
        }
    }
}
