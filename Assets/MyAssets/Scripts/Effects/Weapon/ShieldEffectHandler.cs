using UnityEngine;

namespace MyAssets
{
    public enum ShieldEffectType
    {
        Hit
    }
    /*
     * �V�[���h�̃G�t�F�N�g���܂Ƃ߂��N���X
     */
    public class ShieldEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger EffectLedger => effectLedger;
    }
}
