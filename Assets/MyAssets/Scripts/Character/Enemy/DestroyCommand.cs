using UnityEngine;

namespace MyAssets
{
    public class DestroyCommand : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject);
        }
    }
}
