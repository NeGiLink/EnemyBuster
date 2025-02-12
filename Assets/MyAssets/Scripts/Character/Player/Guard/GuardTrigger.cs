using UnityEngine;

namespace MyAssets
{
    /*
     * �L�����N�^�[�����ŃK�[�h��Ԃ����t���O�ŊǗ����Ă���N���X
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
