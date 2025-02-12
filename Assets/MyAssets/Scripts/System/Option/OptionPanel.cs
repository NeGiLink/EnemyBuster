using UnityEngine;

namespace MyAssets
{
    /*
     * オプションのUIが同時に2つ以上存在する場合に使用するクラス
     */
    public class OptionPanel : MonoBehaviour
    {
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
