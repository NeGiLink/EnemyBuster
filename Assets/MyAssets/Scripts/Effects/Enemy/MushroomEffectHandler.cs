using UnityEngine;

namespace MyAssets
{
    public enum MushroomEffectType
    {
        Death
    }
    /*
     * マッシュルームで使うエフェクトを管理するクラス
     */
    public class MushroomEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger    effectLedger;
        public EffectLedger     EffectLedger => effectLedger;
    }
}
