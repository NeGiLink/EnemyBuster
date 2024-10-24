using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public enum WeaponPositionTag
    {
        None = -1,
        Hand,
        Receipt,
        Count
    }

    public class WeaponPosition : MonoBehaviour
    {
        [SerializeField]
        private new WeaponPositionTag tag = WeaponPositionTag.None;
        public WeaponPositionTag Tag => tag;

        public GameObject ThisObject => gameObject;
    }
}
