using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MyAssets
{
    //オブジェクトをロードするオブジェクトにアタッチするクラス
    //AssetReferenceにセットしたオブジェクトをSubSceneLoaderとの処理で読み込み&解放する
    public class SubScene : MonoBehaviour
    {
        [SerializeField]
        private AssetReference reference;

        //オブジェクトのロードハンドルを格納する
        private AsyncOperationHandle handle;

        bool load = false;
        public bool IsLoading => load;

        // オブジェクトの四角形の幅
        [SerializeField]
        private float objectRectWidth = 100f; 
        //ロード&アンロードを行う処理を少し送らせるのに使っているタイマー
        private Timer timer = new Timer();

        private float count = 0.25f;

        public void DoUpdate()
        {
            timer.Update(Time.deltaTime);
        }

        public void LoadAsset()
        {
            if (!timer.IsEnd()) { return; }
            if (handle.IsValid()&& load) { return; }
            handle = reference.InstantiateAsync();
            load = true;
            timer.Start(count);
        }

        public void UnloadAsset()
        {
            if (!timer.IsEnd()) { return; }
            if (!handle.IsValid())
            {
                return;
            }

            Addressables.ReleaseInstance(handle);
            load = false;
            timer.Start(count);
        }

        /// <summary>
        /// オブジェクトの四角形を取得
        /// </summary>
        /// <returns>オブジェクトの四角形（中心とサイズ）</returns>
        public Rect GetObjectRectangle(SubScene targetSubScene)
        {
            // オブジェクトのローカル座標系での中心
            Vector3 localCenter = targetSubScene.transform.position;

            Rect rect = new Rect(
                localCenter.x - objectRectWidth / 2,
                localCenter.z - objectRectWidth / 2,
                objectRectWidth,
                objectRectWidth
            );
            return rect;
        }
    }
}
