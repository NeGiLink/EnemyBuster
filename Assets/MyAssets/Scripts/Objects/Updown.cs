using UnityEngine;

namespace MyAssets
{
    public class Updown : MonoBehaviour
    {
        [SerializeField]
        private float amplitude = 0.05f; // 振幅

        [SerializeField]
        private Vector3 startPos;

        private static float globalSinValue;


        private void OnEnable()
        {
            startPos = transform.position;
        }
        private void Update()
        {
            if (Time.frameCount % 2 == 0) // 毎フレーム更新せず間引く（負荷軽減）
            {
                globalSinValue = Mathf.Sin(Time.timeSinceLevelLoad);
            }

            transform.position = startPos + new Vector3(0, globalSinValue * amplitude, 0);
        }
    }
}
