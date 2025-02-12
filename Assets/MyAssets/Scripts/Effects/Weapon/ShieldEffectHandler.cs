using UnityEngine;

namespace MyAssets
{
    public enum ShieldEffectType
    {
        Hit
    }
    /*
     * シールドのエフェクトをまとめたクラス
     */
    public class ShieldEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger EffectLedger => effectLedger;
    }
}
