using UnityEngine;

namespace MyAssets
{
    /*
     * ゲームを終了させる処理を行うクラス
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
