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

        public void DoSetup(IPlayerSetup player)
        {
            thisAnimator = player.gameObject.GetComponent<Animator>();
            Assert.IsNotNull(thisAnimator);
        }
    }
}
