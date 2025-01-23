using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField]
        private Text timeText;
        [SerializeField]
        private string timeString = "���� : ";

        [SerializeField]
        private Text enemyText;
        [SerializeField]
        private string enemyString = "�|�����G : ";

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
