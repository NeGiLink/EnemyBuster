using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * ゲームの結果のUIクラス
     * ResultSystemの処理を元にUIを設定
     */
    public class ResultUI : MonoBehaviour
    {
        //時間のテキスト
        [SerializeField]
        private Text    timeText;
        [SerializeField]
        private string  timeString = "時間 : ";
        //敵の討伐数テキスト
        [SerializeField]
        private Text    enemyText;
        [SerializeField]
        private string  enemyString = "倒した敵 : ";

        public void TimeTextOutput(string time)
        {
            timeText.text = timeString + time;
        }

        public void EnemyTextOutput(int num)
        {
            enemyText.text = enemyString + num.ToString();
        }
    }
}
