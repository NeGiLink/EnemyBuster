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

        public string VelocityX         => "VelocityX";
        public string VelocityZ         => "VelocityZ";

        public string MoveName          => "Speed";
        public string DashName          => "Dash";
        public string AlertLevelName    => "AlertLevel";
        public string BattleModeName    => "BattleMode";
        public string ToolLevel         => "ToolLevel";
        public string JumpTypeName      => "JumpType";
        public string FallName          => "Fall";
        public string LandName          => "Land";
        public string AttacksName       => "Attacks";
        public string Weapon_In_OutName => "Weapon_In/Out";
        public string ClimbName         => "Climb";

        private bool enabled = false;
        private int layer = 0;

        public void DoSetup(IPlayerSetup player)
        {
            thisAnimator = player.gameObject.GetComponent<Animator>();
            Assert.IsNotNull(thisAnimator);
        }

        public void SetWeight(bool e, int l)
        {
            enabled = e;
            layer = l;
        }

        public void UpdateWeight()
        {
            float num = 10.0f;
            if (enabled)
            {
                if(thisAnimator.GetLayerWeight(layer) >= 1.0f) { return; }
                thisAnimator.SetLayerWeight(layer, thisAnimator.GetLayerWeight(layer) + num * Time.deltaTime);
                if (thisAnimator.GetLayerWeight(layer) >= 1.0f)
                {
                    thisAnimator.SetLayerWeight(layer, 1.0f);
                }
            }
            else
            {
                if(thisAnimator.GetLayerWeight(layer) <= 0.0f) { return; }
                thisAnimator.SetLayerWeight(layer, thisAnimator.GetLayerWeight(layer) - num * Time.deltaTime);
                if(thisAnimator.GetLayerWeight(layer) <= 0.0f)
                {
                    thisAnimator.SetLayerWeight(layer, 0.0f);
                }
            }

        }
    }
}
