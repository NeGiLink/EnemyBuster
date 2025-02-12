using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * ゲームスタート時に行うカウントをUIとして表示・更新するクラス
     * GameStartTimerの中で使用している
     */
    public class GameStartTimerUI : MonoBehaviour
    {
        private Image countImage;

        private Timer timer;
        public void SetTimer(Timer t) {  timer = t; }
        [SerializeField]
        private Sprite[] sprites;

        private void Awake()
        {
            countImage = GetComponentInChildren<Image>();
        }

        public void CountUI(int count)
        {
            countImage.sprite = sprites[count];
        }

        public void CountEndUI()
        {
            countImage.sprite = sprites[0];
            countImage.SetNativeSize();
        }
    }
}
