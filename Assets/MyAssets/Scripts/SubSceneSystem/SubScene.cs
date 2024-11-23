using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

namespace MyAssets
{
    public class SubScene : MonoBehaviour
    {
        [SerializeField]
        private AssetReference reference;

        //オブジェクトのロードハンドルを格納する
        private AsyncOperationHandle handle;

        bool load = false;

        [SerializeField]
        private float dis = 300.0f;

        void LoadAsset()
        {
            if (load) { return; }
            handle = reference.InstantiateAsync();
            load = true;
        }

        void UnloadAsset()
        {
            if (!handle.IsValid())
            {
                return;
            }

            Addressables.ReleaseInstance(handle);
            load = false;
        }

        public void DoUpdate(Transform player)
        {
            Vector3 loader = player.position;
            loader.y = 0;
            Vector3 mine = transform.position;
            mine.y = 0;
            Vector3 sub = mine - loader;
            if (sub.magnitude <= dis)
            {
                LoadAsset();
            }
            else
            {
                UnloadAsset();
            }
        }
    }
}
