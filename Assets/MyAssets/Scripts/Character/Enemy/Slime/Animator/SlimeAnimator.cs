using UnityEngine;
using UnityEngine.Assertions;

namespace MyAssets
{
    [System.Serializable]
    public class SlimeAnimator : ISlimeAnimator, ICharacterComponent<ISlimeSetup>
    {
        [SerializeField]
        private Animator thisAnimator;

        public Animator Animator => thisAnimator;

        public string MoveName => "Move";

        public string AttacksName => "AttackLevel";

        public void DoSetup(ISlimeSetup slime)
        {
            thisAnimator = slime.gameObject.GetComponentInChildren<Animator>();
            Assert.IsNotNull(thisAnimator);
        }
    }

}
