using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class LockAtUI
    {
        private Transform transform;

        private SpriteRenderer spriteRenderer;

        public void Setup(Transform t)
        {
            transform = t;
            spriteRenderer = t.GetComponent<SpriteRenderer>();
        }

        public void DoUpdate()
        {
            transform.LookAt(Camera.main.transform.position);
        }
    }
}
