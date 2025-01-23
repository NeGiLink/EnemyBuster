using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
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
