using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public enum PhysicMaterialTag
    {
        Landing,
        NoLanding
    }

    [CreateAssetMenu(fileName = "PhysicMaterialData", menuName = "ScriptableObjects/PhysicMaterialData", order = 1)]
    public class PhysicMaterialData : ScriptableObject
    {
        [SerializeField]
        private PhysicMaterial[] data;
        public PhysicMaterial[] Data => data;
    }
}
