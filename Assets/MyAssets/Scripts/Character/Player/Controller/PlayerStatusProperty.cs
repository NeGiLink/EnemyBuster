using Unity.VisualScripting;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class PlayerStatusProperty : BaseStautsProperty,IPlayerStauts
    {
        [SerializeField]
        private int maxSp;
        public int MaxSP => maxSp;
        [SerializeField]
        private int sp;
        public int SP => sp;

        private Timer spRecoveryCoolDown = new Timer();

        public void DecreaseSP(int s)
        {
            if (!spRecoveryCoolDown.IsEnd()) { return; }
            sp -= s;
            if(sp <= 0)
            {
                sp = 0;
                spRecoveryCoolDown.Start(2);
            }
        }
        public void RecoverySP(int s)
        {
            if (!spRecoveryCoolDown.IsEnd()) { return; }
            sp += s;
            if(sp >= maxSp)
            {
                sp = maxSp;
            }
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            spRecoveryCoolDown.Update(time);
        }
    }
}
