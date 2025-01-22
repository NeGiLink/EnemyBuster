using UnityEngine;

namespace MyAssets
{
    [CreateAssetMenu(fileName = "EffectLedger", menuName = "Ledger/EffectLedger", order = 1)]
    public class EffectLedger : LedgerBase<ParticleSystem>
    {

        public void SetPosAndRotCreate(int type, Vector3 pos, Quaternion rot)
        {
            Instantiate(Values[type], pos, rot);
        }
    }
}
