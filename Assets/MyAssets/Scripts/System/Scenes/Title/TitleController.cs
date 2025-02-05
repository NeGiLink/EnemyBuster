using UnityEngine;

namespace MyAssets
{
    public class TitleController : MonoBehaviour
    {
        private static TitleController  instance;
        public static TitleController   Instance => instance;

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                Debug.LogWarning(string.Format("{0}�͂��łɑ��݂��邽�ߍ폜����܂����B", gameObject.name), gameObject);
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
