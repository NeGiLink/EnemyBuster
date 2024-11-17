using System;
using System.Collections;
using UnityEngine;

namespace MyAssets
{
    public enum AttackType
    {
        None = -1,
        Small,
        Middle,
        Big
    }
    public class AttackObject : MonoBehaviour
    {
        [SerializeField]
        private int attackTypeCount = 0;
        public void SetAttackTypeCount(int c) { attackTypeCount = c; }

        public int Power => data.AttackDataInfo[attackTypeCount].power;

        public AttackType Type => data.AttackDataInfo[attackTypeCount].attackType;

        [SerializeField]
        private AttackData data;
    }
}
