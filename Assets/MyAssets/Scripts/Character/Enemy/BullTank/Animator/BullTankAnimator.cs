using UnityEngine;
using UnityEngine.Assertions;

namespace MyAssets
{
    [System.Serializable]
    public class BullTankAnimator : IBullTankAnimator, ICharacterComponent<IBullTankSetup>
    {
        [SerializeField]
        private Animator thisAnimator;

        public Animator Animator => thisAnimator;

        public int MoveAnimationID => Animator.StringToHash("Move");
        public int SideMoveAnimationID => Animator.StringToHash("SideMove");
        public int AttackAnimationID => Animator.StringToHash("Attack");
        public int ReadyAttackAnimationID => Animator.StringToHash("ReadyAttack");
        public int ImpactAnimationID => Animator.StringToHash("Impact");
        public int DeathAnimationID => Animator.StringToHash("Death");


        public void DoSetup(IBullTankSetup actor)
        {
            thisAnimator = actor.gameObject.GetComponentInChildren<Animator>();
            Assert.IsNotNull(thisAnimator);
        }
    }

}
