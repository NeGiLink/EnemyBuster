using UnityEngine;

namespace MyAssets
{
    public interface INPCCommandPanel
    {
        bool IsStopflag { get; }

        void SetStopFlag(bool flag);
    }
    /*
     * NPC�̏�Ԃ����߂邽�߂̃N���X
     * ��Ƀt���O�Ō���ł���悤�ɂ��Ă���
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
