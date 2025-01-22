using UnityEngine;

namespace MyAssets
{
    public enum ShieldEffectType
    {
        Hit
    }
    public class ShieldEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger EffectLedger => effectLedger;
    }
}
