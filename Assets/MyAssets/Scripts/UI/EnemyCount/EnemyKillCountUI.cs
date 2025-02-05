using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    public class EnemyKillCountUI : MonoBehaviour
    {
        private Text text;

        private int max;

        private bool infinite = false;

        private bool noMax = false;
        public void SetInfinite(bool i)
        {
            infinite = i;
        }

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

        public void CountRefresh(int count)
        {
            if(text == null) { return; }
            int current = count;
            if (infinite)
            {
                text.text = string.Format("{0:00}/Åá", current);
            }
            else if (noMax)
            {
                text.text = "Åá";
            }
            else
            {
                text.text = string.Format("{0:00}/{1:00}", current,max);
            }
        }

        public void InfiniteCount(int count)
        {
            text.text = "Åá";
        }

        private void Update()
        {
        
        }
    }
}
