using UnityEngine;

namespace MyAssets
{
    public class GameCanvas : MonoBehaviour
    {
        private static GameCanvas instance;
        public static GameCanvas Instance => instance;


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }
    }
}
