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
        // Start is called before the first frame update
        private void Start()
        {
            SetModeText();
            SetLevelText();
        }

        public void SetModeText()
        {
            texts[(int)ModeInfoTextTag.Mode].text = GameManager.Instance.GetModeText();
        }

        public void SetLevelText()
        {
            texts[(int)ModeInfoTextTag.Level].text = GameManager.Instance.GetGameLevelText();
        }
    }
}
