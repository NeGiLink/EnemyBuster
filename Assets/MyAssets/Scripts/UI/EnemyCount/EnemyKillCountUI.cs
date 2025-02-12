using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * 敵を倒した数をUIとして表示する処理をするクラス
     */
    public class EnemyKillCountUI : MonoBehaviour
    {
        private Text    text;

        private int     max;

        private bool    infinite = false;

        private bool    noMax = false;
        //敵を倒す最大数を無限にするかしないかのクラス
        public void SetInfinite(bool i)
        {
            infinite = i;
        }
        //敵を倒す最大数をなしにするかしないかのクラス
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
        //カウントの更新を行う
        public void CountRefresh(int count)
        {
            if(text == null) { return; }
            int current = count;
            if (infinite)
            {
                text.text = string.Format("{0:00}/∞", current);
            }
            else if (noMax)
            {
                text.text = "∞";
            }
            else
            {
                text.text = string.Format("{0:00}/{1:00}", current,max);
            }
        }
    }
}
