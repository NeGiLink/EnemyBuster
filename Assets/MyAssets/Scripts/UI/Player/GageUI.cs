using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * �v���C���[��HP��SP��UI�\���Ɏg���Ă���N���X
     * HP��SP�̌��݂̐��l��\������̂Ɏg�p���Ă���
     */
    public class GageUI : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        public void GageUpdate(float current,float max)
        {
            image.fillAmount = current / max;
        }
    }
}
