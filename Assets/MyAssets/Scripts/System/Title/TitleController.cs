using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class TitleController : MonoBehaviour
    {
        private static TitleController instance;
        public static TitleController Instance => instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            GameManager.Instance.SetSceneList(SceneList.Title);
            GameManager.Instance.SetFreeCursor();
        }
    }
}
