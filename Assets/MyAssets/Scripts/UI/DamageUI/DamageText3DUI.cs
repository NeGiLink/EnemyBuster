using UnityEngine;

namespace MyAssets
{
    public class DamageText3DUI : MonoBehaviour
    {
        [SerializeField]
        private float DeleteTime = 1.0f;
        [SerializeField]
        private float MoveRange = 1.0f;
        [SerializeField]
        private float EndAlpha = 0;

        private float TimeCnt;
        private TextMesh nowText;
        public TextMesh NowText => nowText;

        public void Setup()
        {
            TimeCnt = 0.0f;
            Destroy(gameObject, DeleteTime);
            nowText = GetComponent<TextMesh>();
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.LookAt(Camera.main.transform);
            TimeCnt += Time.deltaTime;
            gameObject.transform.localPosition += new Vector3(0, MoveRange / DeleteTime * Time.deltaTime, 0);
            gameObject.transform.Rotate(0, -180.0f, 0);
            float _alpha = 1.0f - (1.0f - EndAlpha) * (TimeCnt / DeleteTime);
            if (_alpha <= 0.0f) _alpha = 0.0f;
            nowText.color = new Color(nowText.color.r, nowText.color.g, nowText.color.b, _alpha);
        }
    }
}

