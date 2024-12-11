using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace MyAssets
{
    [System.Serializable]
    public class MushroomAnimator : IMushroomAnimator,ICharacterComponent<IMushroomSetup>
    {
        [SerializeField]
        private Animator thisAnimator;

        public Animator Animator => thisAnimator;

        public string AttacksName => "AttackLevel";

        public void DoSetup(IMushroomSetup mushroom)
        {
            thisAnimator = mushroom.gameObject.GetComponentInChildren<Animator>();
            Assert.IsNotNull(thisAnimator);
        }
    }
}
