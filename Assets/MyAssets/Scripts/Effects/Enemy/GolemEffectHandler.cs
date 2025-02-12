using UnityEngine;

namespace MyAssets
{
    public enum GolemEffectType
    {
        GroundImpact,
        Death
    }
    /*
     * ゴーレムのエフェクトをまとめたクラス
     */
    public class GolemEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger EffectLedger => effectLedger;
    }
}
