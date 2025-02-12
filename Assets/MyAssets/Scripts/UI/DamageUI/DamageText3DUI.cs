using UnityEngine;

namespace MyAssets
{
    /*
     * ダメージ発生時にダメージの数値をTextMeshで出力するクラス
     */
    public class DamageText3DUI : MonoBehaviour
    {
        //消えるまでの時間
        [SerializeField]
        private float       DeleteTime = 1.0f;
        //Y方向に動く範囲
        [SerializeField]
        private float       MoveRange = 1.0f;
        //消える時のアルファ値
        [SerializeField]
        private float       EndAlpha = 0;
        //現在の経過時間を取得するための変更
        private float       timeCnt;
        //テキストメッシュを取得
        private TextMesh    nowText;
        public TextMesh     NowText => nowText;

        public void Setup()
        {
            timeCnt = 0.0f;
            Destroy(gameObject, DeleteTime);
            nowText = GetComponent<TextMesh>();
        }


        private void Update()
        {
            this.transform.LookAt(Camera.main.transform);
            timeCnt += Time.deltaTime;
            gameObject.transform.localPosition += new Vector3(0, MoveRange / DeleteTime * Time.deltaTime, 0);
            gameObject.transform.Rotate(0, -180.0f, 0);
            float _alpha = 1.0f - (1.0f - EndAlpha) * (timeCnt / DeleteTime);
            if (_alpha <= 0.0f) _alpha = 0.0f;
            nowText.color = new Color(nowText.color.r, nowText.color.g, nowText.color.b, _alpha);
        }
    }
}

