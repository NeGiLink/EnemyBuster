using UnityEngine;
using UnityEngine.Assertions;

namespace MyAssets
{
    [System.Serializable]
    public class NPCAnimator : INPCAnimator,ICharacterComponent<INPCSetup>
    {
        [SerializeField]
        private Animator    thisAnimator;

        public Animator     Animator => thisAnimator;
        public int MoveAnimationID => Animator.StringToHash("Move");

        public void DoSetup(INPCSetup slime)
        {
            thisAnimator = slime.gameObject.GetComponentInChildren<Animator>();
            Assert.IsNotNull(thisAnimator);
        }
    }
}
