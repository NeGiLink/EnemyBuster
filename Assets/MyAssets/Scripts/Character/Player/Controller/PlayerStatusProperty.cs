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

        //canvas�Ƀv���C���[�̃X�e�[�^�X(HP�ASP)��UI��\�����邽�߂̐錾
        private PlayerUIHandler playerUIHandler;

        public void Setup(IPlayerSetup actor)
        {
            playerUIHandler = actor.gameObject.GetComponent<PlayerUIHandler>();
        }
        //�e���l���X�V��������UI���ύX
        public override void RecoveryHP(int h)
        {
            base.RecoveryHP(h);
            playerUIHandler.HPgage.GageUpdate(hp, maxHP);
        }

        protected override void HPUIUpdate()
        {
            playerUIHandler.HPgage.GageUpdate(hp, maxHP);
        }

        public void DecreaseSP(int s)
        {
            if (!spRecoveryCoolDown.IsEnd()) { return; }
            sp -= s;
            if(sp <= 0)
            {
                sp = 0;
                spRecoveryCoolDown.Start(2);
            }
            playerUIHandler.SpGage.GageUpdate(sp, maxSp);
        }
        public void RecoverySP(int s)
        {
            if (!spRecoveryCoolDown.IsEnd()) { return; }
            sp += s;
            if(sp >= maxSp)
            {
                sp = maxSp;
            }
            playerUIHandler.SpGage.GageUpdate(sp,maxSp);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            spRecoveryCoolDown.Update(time);
        }
    }
}
