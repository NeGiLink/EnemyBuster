using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class RotateUI : MonoBehaviour
    {
        private RectTransform rectTransform;

        private Vector3 rotationRatio = Vector3.zero;

        [SerializeField]
        private bool rect = true;

        [SerializeField]
        private bool rotX;
        [SerializeField]
        private bool rotY;
        [SerializeField]
        private bool rotZ;

        [SerializeField]
        private float speed;

        private void Awake()
        {
            if (rect)
            {
                rectTransform = GetComponent<RectTransform>();
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            if (rotX)
            {
                rotationRatio.x = 1;
            }
            if (rotY)
            {
                rotationRatio.y = 1;
            }
            if (rotZ)
            {
                rotationRatio.z = 1;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (rect)
            {
                rectTransform.Rotate(rotationRatio * speed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(rotationRatio * speed * Time.deltaTime);
            }
        }
    }
}
