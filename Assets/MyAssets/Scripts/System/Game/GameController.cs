using UnityEngine;

namespace MyAssets
{
    public enum GameResultType
    {
        Null,
        GameOver,
        GameClear
    }

    public class GameController : MonoBehaviour
    {
        private static GameController instance;
        public static GameController Instance => instance;


        private GameResultType gameResultType;
        public GameResultType GameResultType => gameResultType;

        private Timer timer = new Timer();
        public Timer Timer => timer;

        public void SetGameResultType(GameResultType type)
        {
            gameResultType = type;
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            GameManager.Instance.SetLockCursor();
            SetGameResultType(GameResultType.Null);
        }

        public void TimerStart(float count,bool up)
        {
            timer.SetCountUp(up);
            timer.Start(count);
        }

        private void Update()
        {
            if(gameResultType != GameResultType.Null) { return; }
            timer.Update(Time.deltaTime);
            GameUIController.Instance.TimerCountUI?.CountRefresh();
        }
    }
}
