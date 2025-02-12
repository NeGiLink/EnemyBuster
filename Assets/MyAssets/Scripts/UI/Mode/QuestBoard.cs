using UnityEngine;

namespace MyAssets
{
    public enum ModeTag
    {
        None = -1,
        AllKillEnemy,
        TimeAttack,
        Endless
    }
    /*
     * モードのUIにアタッチしているクラス
     * ボタンのコールバックで呼び出して使う
     */
    public class QuestBoard : MonoBehaviour
    {
        //モードタグ
        [SerializeField]
        private new ModeTag     tag;
        //設定したモードの内容を設定するクラス
        private ModeInformation modeInformation;

        private void Awake()
        {
            modeInformation = GetComponentInChildren<ModeInformation>();
        }
        //コールバックで呼び出して使うクラス
        public void SetInformation(string name)
        {
            tag = ModeTag.None;
            switch (name)
            {
                case "AllKillEnemy":
                    tag = ModeTag.AllKillEnemy;
                    break;
                case "TimeAttack":
                    tag = ModeTag.TimeAttack;
                    break;
                case "Endless":
                    tag = ModeTag.Endless;
                    break;
            }
            GameManager.Instance.SetModeTag(tag);
            modeInformation.SetModeText();
            modeInformation.SetModeInfoText(tag);
        }
    }
}
