

using UnityEngine;

namespace MyAssets
{
    public enum EnemyTag
    {
        SlimeLv1,
        SlimeLv2,
        MushroomLv1,
        MushroomLv2,
        BullTank,
        Count
    }
    [CreateAssetMenu(fileName = "EnemyLedger", menuName = "Ledger/EnemyLedger", order = 1)]
    public class EnemyLedger : LedgerBase<CharacterBaseController>
    {
    }
}
