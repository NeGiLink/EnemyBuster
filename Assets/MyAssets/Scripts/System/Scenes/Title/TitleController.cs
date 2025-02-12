using UnityEngine;

namespace MyAssets
{
    /*
     * タイトルシーンの処理を行うクラス
     * タイトルシーンで1つだけなのでシングルトンパターン
     */
    public class TitleController : MonoBehaviour
    {
        private static TitleController  instance;
        public static TitleController   Instance => instance;

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                Debug.LogWarning(string.Format("{0}はすでに存在するため削除されました。", gameObject.name), gameObject);
                return;
            }
            instance = this;
        }

        private void Start()
        {
            GameManager.Instance.SetSceneList(SceneList.Title);
            InputManager.SetFreeCursor();
        }
    }
}
