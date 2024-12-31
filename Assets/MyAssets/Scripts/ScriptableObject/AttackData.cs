using System;
using UnityEngine;

namespace MyAssets
{
    [Serializable]
    public struct AttackDataInfo
    {
        public int power;

        public DamageType attackType;
    }

    [CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObjects/AttackData", order = 1)]
    public class AttackData : ScriptableObject
    {
        [SerializeField]
        private AttackDataInfo[] dataInfo;
        public AttackDataInfo[] AttackDataInfo => dataInfo;
    }
}