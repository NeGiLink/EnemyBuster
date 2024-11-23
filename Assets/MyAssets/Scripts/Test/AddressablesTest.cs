using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MyAssets
{
    public class AddressablesTest : MonoBehaviour
    {
        [SerializeField]
        AssetReference reference;

        [SerializeField]
        private List<AssetReference> references;

        // 各オブジェクトのロードハンドルを格納するリスト
        private List<AsyncOperationHandle<GameObject>> handles = new List<AsyncOperationHandle<GameObject>>();

        void Start()
        {
            LoadAssets();
        }

        void Update()
        {
            // "Delete"キーが押されたら全てのアセットをアンロード
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                UnloadAssets();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                LoadAssets();
            }
        }

        // 複数のアセットをロード
        void LoadAssets()
        {
            for (int i = 0; i < references.Count; i++)
            {
                // 各アセットのインスタンス化を開始し、ハンドルをリストに追加
                var handle = references[i].InstantiateAsync();
                handles.Add(handle);
            }
        }

        // 複数のアセットをアンロード
        void UnloadAssets()
        {
            // ロードされたすべてのハンドルを解放
            foreach (var handle in handles)
            {
                if (handle.IsValid())
                {
                    Addressables.ReleaseInstance(handle);
                }
            }

            // ハンドルリストをクリア
            handles.Clear();
        }
    }
}
