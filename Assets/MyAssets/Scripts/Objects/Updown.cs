using UnityEngine;

namespace MyAssets
{
    public class Updown : MonoBehaviour
    {
        [SerializeField]
        private float amplitude = 0.05f; // �U��

        [SerializeField]
        private Vector3 startPos;

        private static float globalSinValue;


        private void OnEnable()
        {
            startPos = transform.position;
        }
        private void Update()
        {
            if (Time.frameCount % 2 == 0) // ���t���[���X�V�����Ԉ����i���׌y���j
            {
                globalSinValue = Mathf.Sin(Time.timeSinceLevelLoad);
            }

            transform.position = startPos + new Vector3(0, globalSinValue * amplitude, 0);
        }
    }
}
