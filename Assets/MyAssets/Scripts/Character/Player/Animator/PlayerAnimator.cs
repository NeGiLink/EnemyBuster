using UnityEngine;
using UnityEngine.Assertions;

namespace MyAssets
{
    [System.Serializable]
    public class PlayerAnimator : IPlayerAnimator,IPlayerComponent
    {
        [SerializeField]
        private Animator thisAnimator;

        public Animator Animator => thisAnimator;

        public string MoveName          => "Speed";
        public string DashName          => "Dash";
        public string AlertLevelName    => "AlertLevel";
        public string BattleModeName    => "BattleMode";
        public string JumpTypeName      => "JumpType";
        public string FallName          => "Fall";
        public string LandName          => "Land";
        public string AttacksName       => "Attacks";
        public string Weapon_In_OutName => "Weapon_In/Out";
        public string ClimbName         => "Climb";

        public void DoSetup(IPlayerSetup player)
        {
            thisAnimator = player.gameObject.GetComponent<Animator>();
            Assert.IsNotNull(thisAnimator);
        }
    }
}
