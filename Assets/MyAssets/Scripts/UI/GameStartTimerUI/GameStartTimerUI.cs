using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * �Q�[���X�^�[�g���ɍs���J�E���g��UI�Ƃ��ĕ\���E�X�V����N���X
     * GameStartTimer�̒��Ŏg�p���Ă���
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
