using UnityEngine;

namespace MyAssets
{
    /*
     * �e�X�g�V�[����p�̐���N���X
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
                Debug.LogWarning(string.Format("{0}�͂��łɑ��݂��邽�ߍ폜����܂����B", gameObject.name), gameObject);
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

