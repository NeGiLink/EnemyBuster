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
        public void DecreaseSP(int s)
        {
            sp -= s;
            if(sp <= 0)
            {
                sp = 0;
            }
        }
        public void RecoverySP(int s)
        {
            sp += s;
            if(sp >= maxSp)
            {
                sp = maxSp;
            }
        }
    }
}
