using UnityEngine;
using UnityEngine.UI;

namespace MyAssets
{

    public enum ModeInfoTextTag
    {
        Mode,
        ModeInfo,
        Level
    }
    /*
     * モードの設定した内容を表示するクラス
     */
    public class ModeInformation : MonoBehaviour
    {
        [SerializeField]
        private Text[] texts;


        private string timeAttackModeInfo = "出来るだけ早く\n敵を倒せ";

        private string escapeModeInfo = "時間制限まで生き残れ";

        private string endlessModeInfo = "時間制限なし。\n戦い続けろ";



        private void Start()
        {
            SetModeText();
            SetModeInfoText(ModeTag.AllKillEnemy);
            SetLevelText();
        }
        //モードの名前を設定
        public void SetModeText()
        {
            texts[(int)ModeInfoTextTag.Mode].text = GameManager.Instance.GetModeText();
        }
        //モードの詳細を設定
        public void SetModeInfoText(ModeTag tag)
        {
            string text = "";
            switch (tag)
            {
                case ModeTag.AllKillEnemy:
                    text = timeAttackModeInfo;
                    break;
                case ModeTag.TimeAttack:
                    text = escapeModeInfo;
                    break;
                case ModeTag.Endless:
                    text = endlessModeInfo;
                    break;
            }
            texts[(int)ModeInfoTextTag.ModeInfo].text = text;
        }
        //ゲームの難易度のテキストを設定
        public void SetLevelText()
        {
            texts[(int)ModeInfoTextTag.Level].text = GameManager.Instance.GetGameLevelText();
        }
    }
}
