using UnityEngine;

namespace MyAssets
{
    public interface INPCCommandPanel
    {
        bool IsStopflag { get; }

        void SetStopFlag(bool flag);
    }
    /*
     * NPCの状態を決めるためのクラス
     * 主にフラグで決定できるようにしている
     */
    [System.Serializable]
    public class NPCCommandPanel : INPCCommandPanel,ICharacterComponent<INPCSetup>
    {
        [SerializeField]
        private bool    stopflag = false;

        public bool     IsStopflag => stopflag;

        public void SetStopFlag(bool flag) {  stopflag = flag; }

        public void DoSetup(INPCSetup setup)
        {

        }
    }
}
