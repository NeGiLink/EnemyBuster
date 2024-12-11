using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace MyAssets
{
    public class EnemyAnimator : IEnemyAnimator,ICharacterComponent<IEnemySetup>
    {
        [SerializeField]
        private Animator thisAnimator;

        public Animator Animator => thisAnimator;

        public void DoSetup(IEnemySetup enemy)
        {
            thisAnimator = enemy.gameObject.GetComponentInChildren<Animator>();
            Assert.IsNotNull(thisAnimator);
        }
    }
}
