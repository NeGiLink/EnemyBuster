using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    public class EnemyKillCountUI : MonoBehaviour
    {
        private Text text;

        private int max;

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
            text.text = string.Format("{0:00}/{1:00}", current,max);
        }

        private void Update()
        {
        
        }
    }
}
