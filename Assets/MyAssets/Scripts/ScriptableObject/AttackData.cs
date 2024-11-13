using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [Serializable]
    public struct AttackDataInfo
    {
        public float power;

        public AttackType attackType;
    }

    [CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObjects/AttackData", order = 1)]
    public class AttackData : ScriptableObject
    {
        [SerializeField]
        private AttackDataInfo[] dataInfo;
        public AttackDataInfo[] AttackDataInfo => dataInfo;
    }
}