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
        private GameLevel levelTag;

        private int levelCount = 0;

        private void Start()
        {
            levelTag = GameLevel.Easy;
            SetLevel(levelCount);
        }

        public void SetLevel(int level)
        {
            levelTag = (GameLevel)level;
            GameManager.Instance.SetGameLevel(levelTag);
        }
    }
}
