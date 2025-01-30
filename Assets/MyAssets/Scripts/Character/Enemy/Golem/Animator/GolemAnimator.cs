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

        public string AttacksName => "Attack";

        public void DoSetup(IGolemSetup actor)
        {
            thisAnimator = actor.gameObject.GetComponentInChildren<Animator>();
            Assert.IsNotNull(thisAnimator);
        }
    }
}
