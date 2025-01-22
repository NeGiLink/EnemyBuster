using UnityEngine;

namespace MyAssets
{
    public enum SlimeEffectType
    {
        Death
    }
    public class SlimeEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger EffectLedger => effectLedger;
    }
}
