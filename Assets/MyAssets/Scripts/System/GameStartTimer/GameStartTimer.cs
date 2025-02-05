using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class GameStartTimer : MonoBehaviour
    {
        private Canvas canvas;

        private GamePreparation gamePreparation;
        public void SetGamePreparation(GamePreparation g)
        {
            gamePreparation = g;
        }

        [SerializeField]
        private GameStartTimerUI gameStartTimerUI;

        private Timer timer;
        [SerializeField]
        private float interval = 1f;
        [SerializeField]
        private int count = 3;

        public void SetTimerCount(float i,int c)
        {
            interval = i;
            count = c;
        }

        private void Awake()
        {
            canvas = FindObjectOfType<Canvas>();
            gameStartTimerUI = Instantiate(gameStartTimerUI, canvas.transform);
            gameStartTimerUI.SetTimer(timer);
        }

        private void Start()
        {
            timer = new Timer();
            timer.Start(interval);
            timer.OnEnd += CountDown;

            gameStartTimerUI.CountUI(count);
        }

        public void CountDown()
        {
            count--;
            if(count > 0)
            {
                gameStartTimerUI.CountUI(count);
                timer.Start(interval);
            }
            else
            {
                InputManager.SetInputStop(false);
                gameStartTimerUI.CountEndUI();
                gamePreparation.GameStart();
                Destroy(gameStartTimerUI.gameObject, 2f);
                Destroy(gameObject);
            }
        }


        private void Update()
        {
            timer.Update(Time.deltaTime);
        }
    }
}
