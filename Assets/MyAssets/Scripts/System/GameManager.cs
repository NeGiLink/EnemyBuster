using UnityEngine;

namespace MyAssets
{
    public enum SceneList
    {
        Title,
        Select,
        Game,
        Result
    }
    public class GameManager : MonoBehaviour
    {
        private static GameManager          instance;
        public static GameManager           Instance => instance;

        private PlayerCharacterInput                 playerInput;
        private MainCameraController        mainCameraController;
        
        [SerializeField]
        private StageLedger stageLedger;
        public StageLedger StageLedger => stageLedger;

        [SerializeField]
        private GameModeLedger gameModeLedger;
        public GameModeLedger GameModeLedger => gameModeLedger;

        private int stageCount = 0;
        public int StageCount => stageCount;

        private SceneList sceneList;
        public void SetSceneList(SceneList scene) {  sceneList = scene; }


        [SerializeField]
        private ModeTag modeTag = ModeTag.AllKillEnemy;
        public ModeTag ModeTag => modeTag;
        public void SetModeTag(ModeTag tag) {  modeTag = tag; }

        [SerializeField]
        private GameLevel gameLevel = GameLevel.Easy;
        public GameLevel GameLevel => gameLevel;
        public void SetGameLevel(GameLevel level) { gameLevel = level; }

        [SerializeField]
        private bool debug = false;
        public bool Debug => debug;
        //ƒƒCƒ“‚ÌƒQ[ƒ€‚Ì‘€ì‚ğ–³Œø‚É‚·‚é‚©—LŒø‚É‚·‚é‚©‚ÌŠÖ”
        public void ActivatePlayerInput(bool a) 
        {
            if(playerInput == null||mainCameraController == null) { return; }
            playerInput.enabled = a;
            mainCameraController.ActivateAllCamera(a);
        }
        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);

            playerInput = FindObjectOfType<PlayerCharacterInput>();
            mainCameraController = FindObjectOfType<MainCameraController>();
        }

        public void SetLockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void SetFreeCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
