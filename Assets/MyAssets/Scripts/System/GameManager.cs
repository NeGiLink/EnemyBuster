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
    /*
     * �Q�[���̑S�̂Ɋւ�鏈�����s���N���X
     * �S�̂�1�Ȃ̂ŃV���O���g���p�^�[��
     * MonoBehaviour���p�����Ă��闝�R��Unity��GUI�Ŋm�F���₷���悤�ɂ��邽��
     */
    public class GameManager : MonoBehaviour
    {
        private static GameManager          instance;
        public static GameManager           Instance => instance;

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

        //TODO : ���[�h�̃e�L�X�g�o��
        public string GetModeText()
        {
            string text = "";
            switch (modeTag)
            {
                case ModeTag.AllKillEnemy:
                    text = "�^�C���A�^�b�N";
                    break;
                case ModeTag.TimeAttack:
                    text = "�G�X�P�[�v";
                    break;
                case ModeTag.Endless:
                    text = "�G���h���X";
                    break;
            }
            return text;
        }

        //TODO : ��Փx�̃e�L�X�g�o��
        public string GetGameLevelText()
        {
            string text = "";
            switch (gameLevel)
            {
                case GameLevel.Easy:
                    text = "�ȒP";
                    break;
                case GameLevel.Normal:
                    text = "����";
                    break;
                case GameLevel.Hard:
                    text = "���";
                    break;
            }
            return text;
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
        }
    }
}
