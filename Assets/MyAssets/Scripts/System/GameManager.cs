using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance;

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
