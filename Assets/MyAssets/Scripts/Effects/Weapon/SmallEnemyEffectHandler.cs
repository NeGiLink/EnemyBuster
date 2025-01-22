using UnityEngine;

namespace MyAssets
{
    public class SmallEnemyEffectHandller : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger EffectLedger => effectLedger;
    }
}
