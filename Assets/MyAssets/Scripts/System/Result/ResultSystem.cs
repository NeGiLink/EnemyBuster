using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class ResultSystem : MonoBehaviour
    {
        private AbstractGameMode gameMode;
        [SerializeField]
        private ResultUI resultUI;

        private void Awake()
        {
            gameMode = FindObjectOfType<AbstractGameMode>();
            Transform parent = GameCanvas.Instance.UILayer[(int)UILayer.System].transform;
            resultUI = Instantiate(resultUI,parent);
        }

        private void Start()
        {
            GameManager.Instance.SetSceneList(SceneList.Result);

            resultUI.EnemyTextOutput(gameMode.CurrentEnemyKillCount);

            resultUI.TimeTextOutput(GameUIController.Instance.TimerCountUI?.OutputTimeText());
        }
    }
}
