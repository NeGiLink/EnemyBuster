using UnityEngine;

namespace MyAssets
{
    public class TrainingController : MonoBehaviour
    {
        private static TrainingController instance;
        public static TrainingController Instance => instance;


        private GameResultType gameResultType;
        public GameResultType GameResultType => gameResultType;

        [SerializeField]
        private TrainingUIController nextCreateManager;

        public void SetGameResultType(GameResultType type)
        {
            gameResultType = type;
        }

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
            GameManager.Instance.SetSceneList(SceneList.Game);
            InputManager.SetLockCursor();
            SetGameResultType(GameResultType.Null);
            CreateUIManager();
            BGMHandler.Instance.SetPlayer(true, false, 0, true);
        }

        public void CreateUIManager()
        {
            Instantiate(nextCreateManager);
        }
    }
}
