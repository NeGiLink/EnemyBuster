using UnityEngine;

namespace MyAssets
{
    public enum BullTankEffectType
    {
        Death
    }
    public class BullTankEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger EffectLedger => effectLedger;
    }
}
