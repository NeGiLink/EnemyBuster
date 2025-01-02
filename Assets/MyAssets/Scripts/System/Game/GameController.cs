using UnityEngine;

namespace MyAssets
{
    public class GameController : MonoBehaviour
    {
        private static GameController instance;
        public static GameController Instance => instance;

        private void Awake()
        {
            instance = this;
        }


        private void Update()
        {
        
        }
    }
}
