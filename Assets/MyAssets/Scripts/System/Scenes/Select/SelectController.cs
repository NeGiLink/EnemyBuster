using UnityEngine;

namespace MyAssets
{
    public class SelectController : MonoBehaviour
    {
        /*
         * �Z���N�g�V�[���̏������s���N���X
         * �Z���N�g�V�[����1�Ȃ̂ŃV���O���g���p�^�[��
         */
        private static SelectController     instance;
        public static SelectController      Instance => instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                Debug.LogWarning(string.Format("{0}�͂��łɑ��݂��邽�ߍ폜����܂����B", gameObject.name), gameObject);
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
