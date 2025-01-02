using UnityEngine;

namespace MyAssets
{
    public class DeathCollider : MonoBehaviour
    {
        private Collider[] colliders;

        private void Awake()
        {
            colliders = GetComponentsInChildren<Collider>();
        }
        public void DestroyCollider()
        {
            for(int i = 0; i < colliders.Length; i++)
            {
                Destroy(colliders[i]);
            }
        }
    }
}
