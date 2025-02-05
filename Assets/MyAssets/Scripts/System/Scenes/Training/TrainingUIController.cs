using UnityEngine;

namespace MyAssets
{
    public class TrainingUIController : MonoBehaviour
    {
        private static TrainingUIController instance;
        public static TrainingUIController Instance => instance;

        [SerializeField]
        private UIData uiData;

        [SerializeField]
        private OptionInput optionSystem;

        [SerializeField]
        private FadeInText fadeInText;

        [SerializeField]
        private ResultSystem resultSystem;


        private DamageTextCreator damageTextCreator;
        public DamageTextCreator DamageTextCreator => damageTextCreator;

        private TimerCountUI timerCountUI;
        public TimerCountUI TimerCountUI => timerCountUI;

        private EnemyKillCountUI enemyKillCountUI;
        public EnemyKillCountUI EnemyKillCountUI => enemyKillCountUI;

        private PlayerUIHandler playerUIHandler;

        private void Awake()
        {
            instance = this;

            damageTextCreator = GetComponent<DamageTextCreator>();

            playerUIHandler = FindObjectOfType<PlayerUIHandler>();
        }

        private void Start()
        {
            //�ݒ肷��e���擾
            Transform parent;
            parent = GameCanvas.Instance.UILayer[(int)UILayer.System].transform;

            //�I�v�V�����@�\����
            Instantiate(optionSystem);
            //�e���Đݒ�
            parent = GameCanvas.Instance.UILayer[(int)UILayer.Player].transform;
            //����UI����
            Instantiate(uiData[(int)UITag.Input], parent);
            //�v���C���[UI����
            playerUIHandler.Create();
        }
    }
}
