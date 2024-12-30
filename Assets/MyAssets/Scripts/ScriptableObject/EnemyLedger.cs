

using UnityEngine;

namespace MyAssets
{
    public enum EnemyTag
    {
        SlimeLv1,
        SlimeLv2,
        Mushroom,

        Count
    }
    [CreateAssetMenu(fileName = "EnemyLedger", menuName = "Ledger/EnemyLedger", order = 1)]
    public class EnemyLedger : LedgerBase<CharacterBaseController>
    {
    }
}
