using UnityEngine;

namespace MyAssets
{
    public enum SceneList
    {
        Title,
        Select,
        Game,
        Credit,
        Training,
        Result
    }
    public class GameManager : MonoBehaviour
    {
        private static GameManager          instance;
        public static GameManager           Instance => instance;

        private PlayerActionInput        playerInput;
        private MainCameraController        mainCameraController;
        
        [SerializeField]
        private StageLedger                 stageLedger;
        public StageLedger                  StageLedger => stageLedger;

        [SerializeField]
        private GameModeLedger              gameModeLedger;
        public GameModeLedger               GameModeLedger => gameModeLedger;

        [SerializeField]
        private ModeTag                     modeTag = ModeTag.AllKillEnemy;
        public ModeTag                      ModeTag => modeTag;

        [SerializeField]
        private GameLevel                   gameLevel = GameLevel.Easy;
        public GameLevel                    GameLevel => gameLevel;

        [SerializeField]
        private bool                        debug = false;
        public bool                         Debug => debug;

        private SceneList                   sceneList;
        public SceneList                    SceneList => sceneList;


        public void SetSceneList(SceneList scene) {  sceneList = scene; }
        public void SetModeTag(ModeTag tag) {  modeTag = tag; }
        public void SetGameLevel(GameLevel level) { gameLevel = level; }

        //TODO : モードのテキスト出力
        public string GetModeText()
        {
            string text = "";
            switch (modeTag)
            {
                case ModeTag.AllKillEnemy:
                    text = "タイムアタック";
                    break;
                case ModeTag.TimeAttack:
                    text = "エスケープ";
                    break;
                case ModeTag.Endless:
                    text = "エンドレス";
                    break;
            }
            return text;
        }

        //TODO : 難易度のテキスト出力
        public string GetGameLevelText()
        {
            string text = "";
            switch (gameLevel)
            {
                case GameLevel.Easy:
                    text = "簡単";
                    break;
                case GameLevel.Normal:
                    text = "普通";
                    break;
                case GameLevel.Hard:
                    text = "難しい";
                    break;
            }
            return text;
        }

        public void SetDebug(bool d)
        {
            debug = d;
        }
        //メインのゲームの操作を無効にするか有効にするかの関数
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

            playerInput = FindObjectOfType<PlayerActionInput>();
            mainCameraController = FindObjectOfType<MainCameraController>();
        }
    }
}
