using UnityEngine;

namespace MyAssets
{
    public enum SlimeEffectType
    {
        Death
    }
    /*
     * �X���C���̃G�t�F�N�g���܂Ƃ߂��N���X
     */
    public class SlimeEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger EffectLedger => effectLedger;
    }
}
