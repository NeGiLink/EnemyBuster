using UnityEngine;

namespace MyAssets
{
    //�G�̑傫�����x���̏��T�C�Y�̓G���g���G�t�F�N�g�N���X
    public class SmallEnemyEffectHandller : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger  EffectLedger => effectLedger;
    }
}
