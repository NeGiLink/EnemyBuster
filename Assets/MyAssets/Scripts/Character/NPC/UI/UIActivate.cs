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

        private IFieldOfView fieldOfView;

        [SerializeField]
        private LockAtUI lockAtUI;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            fieldOfView = GetComponentInParent<IFieldOfView>();

            lockAtUI.Setup(transform);
        }

        private void Start()
        {
            spriteRenderer.sprite = null;
        }

        public void Update()
        {
            if (fieldOfView.TargetObject != null)
            {
                spriteRenderer.sprite = sprite;
                lockAtUI.DoUpdate();
            }
            else
            {
                spriteRenderer.sprite = null;
            }
        }
    }
}
