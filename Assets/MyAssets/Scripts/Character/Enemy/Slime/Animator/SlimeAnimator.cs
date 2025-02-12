using UnityEngine;
using UnityEngine.Assertions;

namespace MyAssets
{
    [System.Serializable]
    public class SlimeAnimator : ISlimeAnimator, ICharacterComponent<ISlimeSetup>
    {
        [SerializeField]
        private Animator    thisAnimator;

        public Animator     Animator => thisAnimator;

        public int          MoveAnimationID => Animator.StringToHash("Move");
        public int          AttackAnimationID => Animator.StringToHash("AttackLevel");
        public int          AttackTriggerAnimationID => Animator.StringToHash("AttackStart");

        public int          DeathAnimationID => Animator.StringToHash("Death");

        public void DoSetup(ISlimeSetup slime)
        {
            thisAnimator = slime.gameObject.GetComponentInChildren<Animator>();
            Assert.IsNotNull(thisAnimator);
        }
    }

}
