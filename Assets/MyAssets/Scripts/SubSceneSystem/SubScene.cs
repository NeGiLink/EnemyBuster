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
        private float objectRectWidth = 100f; // オブジェクトの四角形の幅

        public void LoadAsset()
        {
            if (handle.IsValid()&& load) { return; }
            handle = reference.InstantiateAsync();
            load = true;
        }

        public void UnloadAsset()
        {
            if (!handle.IsValid())
            {
                return;
            }

            Addressables.ReleaseInstance(handle);
            load = false;
        }

        /// <summary>
        /// オブジェクトの四角形を取得
        /// </summary>
        /// <returns>オブジェクトの四角形（中心とサイズ）</returns>
        public Rect GetObjectRectangle(SubScene targetSubScene)
        {
            // オブジェクトのローカル座標系での中心
            Vector3 localCenter = targetSubScene.transform.position;

            Rect rect = new Rect(localCenter.x - objectRectWidth / 2,
                localCenter.z - objectRectWidth / 2,
                objectRectWidth,
                objectRectWidth
            );
            return rect;
        }
    }
}
