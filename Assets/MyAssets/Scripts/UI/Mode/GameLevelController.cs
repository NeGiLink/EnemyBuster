using UnityEngine;

namespace MyAssets
{
    public enum GameLevel
    {
        Easy,
        Normal,
        Hard
    }
    /*
     * �Q�[���̓�Փx��ݒ肷��N���X
     * �{�^���̃R�[���o�b�N�ɐݒ肵�Ďg��
     */
    public class GameLevelController : MonoBehaviour
    {
        [SerializeField]
        private GameLevel       levelTag = GameLevel.Easy;

        private ModeInformation modeInformation;

        private void Awake()
        {
            modeInformation = GetComponentInChildren<ModeInformation>();
        }

        private void Start()
        {
            modeInformation.SetLevelText();
        }
        //�R�[���o�b�N�ŌĂяo���֐�
        public void SetLevel(int level)
        {
            levelTag = (GameLevel)level;
            GameManager.Instance.SetGameLevel(levelTag);
            modeInformation.SetLevelText();
        }
    }
}
