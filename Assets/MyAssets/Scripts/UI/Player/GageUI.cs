using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
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
