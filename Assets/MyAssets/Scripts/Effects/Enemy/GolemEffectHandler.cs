using UnityEngine;

namespace MyAssets
{
    public enum GolemEffectType
    {
        GroundImpact,
        Death
    }
    /*
     * �S�[�����̃G�t�F�N�g���܂Ƃ߂��N���X
     */
    public class GolemEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger EffectLedger => effectLedger;
    }
}
