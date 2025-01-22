using UnityEngine;

namespace MyAssets
{
    public enum MushroomEffectType
    {
        Death
    }
    public class MushroomEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger EffectLedger => effectLedger;
    }
}
