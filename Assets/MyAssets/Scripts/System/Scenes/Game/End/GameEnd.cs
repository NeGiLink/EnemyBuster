using UnityEngine;

namespace MyAssets
{
    /*
     * �Q�[�����I�������鏈�����s���N���X
     */
    public class GameEnd : MonoBehaviour
    {
        public void Execute()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
        }
    }
}
