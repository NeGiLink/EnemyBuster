using UnityEngine;

namespace MyAssets
{
    /*
     * �v���C���[�̐퓬��Ԃ�؂�ւ��鏈�����s���N���X
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
