using UnityEngine;

namespace MyAssets
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager          instance;
        public static GameManager           Instance => instance;

        private DamageTextCreator           damageTextCreator;
        public DamageTextCreator            DamageTextCreator => damageTextCreator;

        private PlayerInput                 playerInput;
        private MainCameraController        mainCameraController;


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

            damageTextCreator = GetComponent<DamageTextCreator>();
            playerInput = FindObjectOfType<PlayerInput>();
            mainCameraController = FindObjectOfType<MainCameraController>();
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
