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
    /// Canvas�ւ̒ǉ��E�ύX�E�폜���s���N���X
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
            //�ݒ肷��e���擾
            Transform parent = GameCanvas.Instance.UILayer[(int)UILayer.System].transform;
            //�^�C�}�[UI�𐶐�
            timerCountUI = Instantiate(uiData[(int)UITag.TimerCount], parent).GetComponent<TimerCountUI>();

            //�G���j��UI�̐���
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
            //�I�v�V�����@�\����
            Instantiate(optionSystem);
            //�e���Đݒ�
            parent = GameCanvas.Instance.UILayer[(int)UILayer.Player].transform;
            //����UI����
            Instantiate(uiData[(int)UITag.Input], parent);
            //�v���C���[UI����
            playerUIHandler.Create();
        }

        public void CreateFadeResultTextUI()
        {
            Transform parent = GameCanvas.Instance.UILayer[(int)UILayer.System].transform;
            FadeInText text = Instantiate(fadeInText, parent);
            text.gameObject.AddComponent<ResultCreater>();
            if(GameModeController.Instance.AbstractGameMode.CurrentEnemyKillCount >= GameModeController.Instance.AbstractGameMode.MaxEnemyKillCount)
            {
                text.SetOutputText("�Q�[���N���A");
            }
            else
            {
                text.SetOutputText("�Q�[���I�[�o�[");
            }
        }

        public void CreateResultUI()
        {
            InputManager.SetFreeCursor();
            Instantiate(resultSystem);
        }
    }
}
