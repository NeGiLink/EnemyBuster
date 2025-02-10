using UnityEngine;

namespace MyAssets
{
    //敵の大きさレベルの小サイズの敵が使うエフェクトクラス
    public class SmallEnemyEffectHandller : MonoBehaviour
    {
        [SerializeField]
        private EffectLedger effectLedger;
        public EffectLedger  EffectLedger => effectLedger;
    }
}
