using System;
using System.Collections;
using UnityEngine;

namespace MyAssets
{
    public enum DamageType
    {
        None = -1,
        Small,
        Middle,
        Big
    }
    public class AttackObject : MonoBehaviour
    {
        [SerializeField]
        private AttackData data;

        [SerializeField]
        private int attackTypeCount = 0;
        public void SetAttackTypeCount(int c) { attackTypeCount = c; }

        public int Power => data.AttackDataInfo[attackTypeCount].power;

        public float KnockBack => data.AttackDataInfo[attackTypeCount].knockBack;

        public DamageType Type => data.AttackDataInfo[attackTypeCount].attackType;

    }
}
