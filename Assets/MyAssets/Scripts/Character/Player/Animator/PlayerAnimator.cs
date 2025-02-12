using UnityEngine;
using UnityEngine.Assertions;

namespace MyAssets
{
    /*
     * プレイヤーのアニメーション関連をまとめたクラス
     */
    [System.Serializable]
    public class PlayerAnimator : IPlayerAnimator, ICharacterComponent<IPlayerSetup>
    {
        [SerializeField]
        private Animator            thisAnimator;

        public Animator             Animator => thisAnimator;

        public AnimatorStateInfo    AnimatorStateInfo => thisAnimator.GetCurrentAnimatorStateInfo(0);

        public bool IsEndMotion()
        {
            return AnimatorStateInfo.normalizedTime >= 1.0f;
        }

        public int VelocityXAnimationID => Animator.StringToHash("VelocityX");
        public int VelocityZAnimationID => Animator.StringToHash("VelocityZ");
        public int MoveAnimationID => Animator.StringToHash("Speed");
        public int DashAnimationID => Animator.StringToHash("Dash");
        public int AlertLevelAnimationID => Animator.StringToHash("AlertLevel");
        public int BattleModeAnimationID => Animator.StringToHash("BattleMode");
        public int ToolLevelAnimationID => Animator.StringToHash("ToolLevel");
        public int JumpAnimationID => Animator.StringToHash("JumpType");
        public int FallAnimationID => Animator.StringToHash("Fall");
        public int LandAnimationID => Animator.StringToHash("Land");
        public int AttackAnimationID => Animator.StringToHash("Attacks");
        public int ChargeAttackAnimationID => Animator.StringToHash("ChargeAttack");
        public int Weapon_In_OutAnimationID => Animator.StringToHash("Weapon_In/Out");
        public int ClimbAnimationID => Animator.StringToHash("Climb");
        public int ImpactAnimationID => Animator.StringToHash("Impact");

        private bool enabled = false;
        private int layer = 0;

        public void DoSetup(IPlayerSetup actor)
        {
            thisAnimator = actor.gameObject.GetComponent<Animator>();
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
