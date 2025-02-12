using UnityEngine;

namespace MyAssets
{
    /*
     * テストシーン専用の制御クラス
     */
    public class TestSceneController : MonoBehaviour
    {
        private static TestSceneController  instance;
        public static TestSceneController   Instance => instance;

        private PlayerUIHandler playerUIHandler;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                Debug.LogWarning(string.Format("{0}はすでに存在するため削除されました。", gameObject.name), gameObject);
                return;
            }
            instance = this;

            playerUIHandler = FindObjectOfType<PlayerUIHandler>();
        }

        private void Start()
        {
            GameManager.Instance.SetSceneList(SceneList.Game);
            InputManager.SetLockCursor();

            playerUIHandler.Create();
        }
    }
}

