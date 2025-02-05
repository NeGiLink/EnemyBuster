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
            //設定する親を取得
            Transform parent;
            parent = GameCanvas.Instance.UILayer[(int)UILayer.System].transform;

            //オプション機能生成
            Instantiate(optionSystem);
            //親を再設定
            parent = GameCanvas.Instance.UILayer[(int)UILayer.Player].transform;
            //入力UI生成
            Instantiate(uiData[(int)UITag.Input], parent);
            //プレイヤーUI生成
            playerUIHandler.Create();
        }
    }
}
