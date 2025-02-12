using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{
    /*
     * プレイヤーのHPやSPのUI表示に使っているクラス
     * HPやSPの現在の数値を表示するのに使用している
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
