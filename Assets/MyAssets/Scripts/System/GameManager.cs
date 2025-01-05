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

        private PlayerInput                 playerInput;
        private MainCameraController        mainCameraController;
        
        [SerializeField]
        private StageLedger stageLedger;
        public StageLedger StageLedger => stageLedger;

        private int stageCount = 0;
        public int StageCount => stageCount;

        private SceneList sceneList;
        public void SetSceneList(SceneList scene) {  sceneList = scene; }

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

            playerInput = FindObjectOfType<PlayerInput>();
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
