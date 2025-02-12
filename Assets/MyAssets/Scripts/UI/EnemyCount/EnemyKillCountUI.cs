using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * �G��|��������UI�Ƃ��ĕ\�����鏈��������N���X
     */
    public class EnemyKillCountUI : MonoBehaviour
    {
        private Text    text;

        private int     max;

        private bool    infinite = false;

        private bool    noMax = false;
        //�G��|���ő吔�𖳌��ɂ��邩���Ȃ����̃N���X
        public void SetInfinite(bool i)
        {
            infinite = i;
        }
        //�G��|���ő吔���Ȃ��ɂ��邩���Ȃ����̃N���X
        public void SetNoMax(bool m)
        {
            noMax = m;
        }

        private void Awake()
        {
            text = GetComponentInChildren<Text>();
        }

        public void SetMaxCount(int m)
        {
            max = m;
        }
        //�J�E���g�̍X�V���s��
        public void CountRefresh(int count)
        {
            if(text == null) { return; }
            int current = count;
            if (infinite)
            {
                text.text = string.Format("{0:00}/��", current);
            }
            else if (noMax)
            {
                text.text = "��";
            }
            else
            {
                text.text = string.Format("{0:00}/{1:00}", current,max);
            }
        }
    }
}
