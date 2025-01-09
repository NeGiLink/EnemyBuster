using UnityEngine;
using UnityEngine.Assertions;

namespace MyAssets
{
    public class BullTankAnimator : IBullTankAnimator, ICharacterComponent<IBullTankSetup>
    {
        [SerializeField]
        private Animator thisAnimator;

        public Animator Animator => thisAnimator;

        public string AttacksName => "Attack";

        public void DoSetup(IBullTankSetup actor)
        {
            thisAnimator = actor.gameObject.GetComponentInChildren<Animator>();
            Assert.IsNotNull(thisAnimator);
        }
    }

}
