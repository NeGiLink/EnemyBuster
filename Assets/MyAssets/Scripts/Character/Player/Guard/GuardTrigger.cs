using UnityEngine;

namespace MyAssets
{
    /*
     * キャラクターが盾でガード状態かをフラグで管理しているクラス
     */
    [System.Serializable]
    public class GuardTrigger : IGuardTrigger, ICharacterComponent<ICharacterSetup>
    {

        [SerializeField]
        private bool        guard;
        public bool         IsGuard => guard;

        public void SetGuardFlag(bool flag) { guard = flag; }

        public void DoSetup(ICharacterSetup actor)
        {

        }
    }
}
