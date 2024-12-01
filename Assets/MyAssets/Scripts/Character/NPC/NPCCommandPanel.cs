using UnityEngine;

namespace MyAssets
{
    public interface INPCCommandPanel
    {
        bool IsFixedflag { get; }

        void SetFixedFlag(bool flag);
    }

    [System.Serializable]
    public class NPCCommandPanel : INPCCommandPanel,ICharacterComponent<INPCSetup>
    {
        [SerializeField]
        private bool fixedflag = false;

        public bool IsFixedflag => fixedflag;

        public void SetFixedFlag(bool flag) {  fixedflag = flag; }

        public void DoSetup(INPCSetup setup)
        {

        }
    }
}
