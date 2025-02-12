using UnityEngine;

namespace MyAssets
{
    /*
     * プレイヤーの戦闘状態を切り替える処理を行うクラス
     */
    [System.Serializable]
    public class BattleFlagger : IBattleFlagger,ICharacterComponent<IPlayerSetup>
    {
        [SerializeField]
        private bool            battleMode = false;
        public bool             IsBattleMode => battleMode;

        public void SetBattleMode(bool b) { battleMode = b; }

        public void DoSetup(IPlayerSetup actor){}
    }
}
