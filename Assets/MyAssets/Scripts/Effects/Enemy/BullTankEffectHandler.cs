using UnityEngine;

namespace MyAssets
{
    public enum BullTankEffectType
    {
        Death
    }
    /*
     * �u���^���N�̃G�t�F�N�g���܂Ƃ߂��N���X
     */
    public class BullTankEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger EffectLedger => effectLedger;
    }
}
