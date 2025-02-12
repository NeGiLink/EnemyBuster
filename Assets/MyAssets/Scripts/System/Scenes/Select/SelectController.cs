using UnityEngine;

namespace MyAssets
{
    public class SelectController : MonoBehaviour
    {
        /*
         * セレクトシーンの処理を行うクラス
         * セレクトシーンで1つなのでシングルトンパターン
         */
        private static SelectController     instance;
        public static SelectController      Instance => instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                Debug.LogWarning(string.Format("{0}はすでに存在するため削除されました。", gameObject.name), gameObject);
                return;
            }
            instance = this;
        }

        private void Start()
        {
            GameManager.Instance.SetSceneList(SceneList.Select);
            InputManager.SetFreeCursor();
        }
    }
}
