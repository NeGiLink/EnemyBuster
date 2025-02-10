using UnityEngine;
using UnityEngine.Assertions;

namespace MyAssets
{
    [System.Serializable]
    public class GolemAnimator : IGolemAnimator, ICharacterComponent<IGolemSetup>
    {
        [SerializeField]
        private Animator thisAnimator;

        public Animator Animator => thisAnimator;


        public int MoveAnimationID => Animator.StringToHash("Move");
        public int AttackAnimationID => Animator.StringToHash("Attack");
        public int ImpactAnimationID => Animator.StringToHash("Impact");
        public int DeathAnimationID => Animator.StringToHash("Death");

        public void DoSetup(IGolemSetup actor)
        {
            thisAnimator = actor.gameObject.GetComponentInChildren<Animator>();
            Assert.IsNotNull(thisAnimator);
        }
    }
}
