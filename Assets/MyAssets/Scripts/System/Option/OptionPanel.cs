using UnityEngine;

namespace MyAssets
{
    /*
     * �I�v�V������UI��������2�ȏ㑶�݂���ꍇ�Ɏg�p����N���X
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
