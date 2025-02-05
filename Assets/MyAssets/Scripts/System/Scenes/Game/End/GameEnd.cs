using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
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
