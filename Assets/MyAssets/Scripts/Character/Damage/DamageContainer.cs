using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace MyAssets
{
    [System.Serializable]
    public class DamageContainer : IDamageContainer,ICharacterComponent
    {
        [SerializeField]
        private float data = 0.0f;
        public void SetData(float d) { data = d; }
        public float Data => data;

        private AttackType attackType = AttackType.None;
        public void SetAttackType(AttackType type) { attackType = type; }
        public AttackType AttackType => attackType;

        private Transform attacker;
        public void SetAttacker(Transform t) { attacker = t; }
        public Transform Attacker => attacker;

        public void DoSetup(ICharacterSetup chara){}
    }
}
