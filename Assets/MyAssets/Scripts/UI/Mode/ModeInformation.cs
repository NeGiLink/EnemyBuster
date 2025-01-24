using System.Collections;
using System.Collections.Generic;
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
    public class ModeInformation : MonoBehaviour
    {
        [SerializeField]
        private Text[] texts;


        private string timeAttackModeInfo = "èoóàÇÈÇæÇØëÅÇ≠\nìGÇì|Çπ";

        private string escapeModeInfo = "éûä‘êßå¿Ç‹Ç≈ê∂Ç´écÇÍ";

        private string endlessModeInfo = "éûä‘êßå¿Ç»ÇµÅB\nêÌÇ¢ë±ÇØÇÎ";


        // Start is called before the first frame update
        private void Start()
        {
            SetModeText();
            SetModeInfoText(ModeTag.AllKillEnemy);
            SetLevelText();
        }

        public void SetModeText()
        {
            texts[(int)ModeInfoTextTag.Mode].text = GameManager.Instance.GetModeText();
        }

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

        public void SetLevelText()
        {
            texts[(int)ModeInfoTextTag.Level].text = GameManager.Instance.GetGameLevelText();
        }
    }
}
