using UnityEngine;

namespace MyAssets
{
    public enum MushroomEffectType
    {
        Death
    }
    /*
     * �}�b�V�����[���Ŏg���G�t�F�N�g���Ǘ�����N���X
     */
    public class MushroomEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger    effectLedger;
        public EffectLedger     EffectLedger => effectLedger;
    }
}
