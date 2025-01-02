using UnityEngine;

namespace MyAssets
{
    public enum UILayer
    {
        Player,
        System
    }

    public class GameCanvas : MonoBehaviour
    {
        private static GameCanvas instance;
        public static GameCanvas Instance => instance;

        [SerializeField]
        private GameObject[] uiLayer;
        public GameObject[] UILayer => uiLayer;


        private void Awake()
        {
            instance = this;
        }
    }
}
