using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public enum GameLevel
    {
        Easy,
        Normal,
        Hard
    }

    public class GameLevelController : MonoBehaviour
    {
        [SerializeField]
        private GameLevel levelTag = GameLevel.Easy;

        private ModeInformation modeInformation;

        private void Awake()
        {
            modeInformation = GetComponentInChildren<ModeInformation>();
        }

        private void Start()
        {
            modeInformation.SetLevelText();
        }

        public void SetLevel(int level)
        {
            levelTag = (GameLevel)level;
            GameManager.Instance.SetGameLevel(levelTag);
            modeInformation.SetLevelText();
        }
    }
}
