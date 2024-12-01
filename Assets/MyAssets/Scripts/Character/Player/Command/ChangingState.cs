using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class ChangingState : IChangingState,ICharacterComponent<IPlayerSetup>
    {
        [SerializeField]
        private bool battleMode = false;
        public bool IsBattleMode => battleMode;

        public void SetBattleMode(bool b) { battleMode = b; }

        public void DoSetup(IPlayerSetup player){}
    }
}
