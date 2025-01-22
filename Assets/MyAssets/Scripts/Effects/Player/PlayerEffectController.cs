using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public enum PlayerEffectType
    {
        GroundHit,
        Damage,
    }

    public class PlayerEffectController : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger EffectLedger => effectLedger;

        public void Create(PlayerEffectType type)
        {
            Instantiate(effectLedger[(int)type],transform.position, effectLedger[(int)type].transform.rotation);
        }
    }
}

