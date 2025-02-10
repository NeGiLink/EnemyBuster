using UnityEngine;

namespace MyAssets
{
    public enum GolemEffectType
    {
        GroundImpact,
        Death
    }
    public class GolemEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger EffectLedger => effectLedger;
    }
}
