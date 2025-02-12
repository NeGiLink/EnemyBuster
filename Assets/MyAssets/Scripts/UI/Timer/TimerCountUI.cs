using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * ゲームの経過時間をUIで表示するクラス
     */
    public class TimerCountUI : MonoBehaviour
    {
        private Text text;

        private void Awake()
        {
            text = GetComponentInChildren<Text>();
        }
        public void CountRefresh()
        {
            if (GameController.Instance.Timer.IsEnd()) { return; }
            Timer timer = GameController.Instance.Timer;
            int m = timer.GetMinutes();
            int s = timer.GetSecond();
            SetTimeText(m, s);
        }

        private void SetTimeText(int m, int s)
        {
            text.text = string.Format("{0:00}:{1:00}", m, s);
        }

        public string OutputTimeText()
        {
            Timer timer = GameController.Instance.Timer;
            int m = timer.GetMinutes();
            int s = timer.GetSecond();
            return text.text = string.Format("{0:00}:{1:00}", m, s);
        }
    }
}
