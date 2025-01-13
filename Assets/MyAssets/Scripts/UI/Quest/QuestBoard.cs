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
    public class QuestBoard : MonoBehaviour
    {
        [SerializeField]
        private new ModeTag tag;

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
            Destroy(gameObject);
        }
    }
}
