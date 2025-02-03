using UnityEngine;

namespace MyAssets
{
    public enum UITag
    {
        HP,
        SP,
        Input,
        TimerCount,
        EnemyCount
    }

    /// <summary>
    /// Canvasへの追加・変更・削除を行うクラス
    /// </summary>
    public class GameUIController : MonoBehaviour
    {
        private static GameUIController instance;
        public static GameUIController Instance => instance;

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
            Transform parent = GameCanvas.Instance.UILayer[(int)UILayer.System].transform;
            //タイマーUIを生成
            timerCountUI = Instantiate(uiData[(int)UITag.TimerCount], parent).GetComponent<TimerCountUI>();

            //敵撃破数UIの生成
            enemyKillCountUI = Instantiate(uiData[(int)UITag.EnemyCount], parent).GetComponent<EnemyKillCountUI>();
            if(GameManager.Instance.ModeTag == ModeTag.Endless)
            {
                enemyKillCountUI.SetInfinite(true);
            }
            else
            {
                enemyKillCountUI.SetInfinite(false);
            }
            enemyKillCountUI.SetMaxCount(GameModeController.Instance.MaxEnemyCount);
            int count = GameModeController.Instance.AbstractGameMode.CurrentEnemyKillCount;
            enemyKillCountUI.CountRefresh(count);
            //オプション機能生成
            Instantiate(optionSystem);
            //親を再設定
            parent = GameCanvas.Instance.UILayer[(int)UILayer.Player].transform;
            //入力UI生成
            Instantiate(uiData[(int)UITag.Input], parent);
            //プレイヤーUI生成
            playerUIHandler.Create();
        }

        public void CreateFadeResultTextUI()
        {
            Transform parent = GameCanvas.Instance.UILayer[(int)UILayer.System].transform;
            FadeInText text = Instantiate(fadeInText, parent);
            text.gameObject.AddComponent<ResultCreater>();
            if(GameModeController.Instance.AbstractGameMode.CurrentEnemyKillCount >= GameModeController.Instance.AbstractGameMode.MaxEnemyKillCount)
            {
                text.SetOutputText("ゲームクリア");
            }
            else
            {
                text.SetOutputText("ゲームオーバー");
            }
        }

        public void CreateResultUI()
        {
            InputManager.SetFreeCursor();
            Instantiate(resultSystem);
        }
    }
}
