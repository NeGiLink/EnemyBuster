using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * �Q�[���̌��ʂ�UI�N���X
     * ResultSystem�̏���������UI��ݒ�
     */
    public class ResultUI : MonoBehaviour
    {
        //���Ԃ̃e�L�X�g
        [SerializeField]
        private Text    timeText;
        [SerializeField]
        private string  timeString = "���� : ";
        //�G�̓������e�L�X�g
        [SerializeField]
        private Text    enemyText;
        [SerializeField]
        private string  enemyString = "�|�����G : ";

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
