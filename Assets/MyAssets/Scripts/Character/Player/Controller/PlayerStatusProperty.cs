using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public enum PlayerSETag
    {
        Jump,
        Avoid,
        Damage,
        Recovery
    }

    /*
     * �v���C���[�̃X�e�[�^�X���܂Ƃ߂��N���X
     * PlayerController��Serializable���s���Ă���
     */
    [System.Serializable]
    public class PlayerStatusProperty : BaseStautsProperty,IPlayerStauts
    {
        [SerializeField]
        private int             maxSp;
        public int              MaxSP => maxSp;
        [SerializeField]
        private int             sp;
        public int              SP => sp;

        [SerializeField]
        private int             rollingUseSP;
        public int              RollingUseSP => rollingUseSP;

        [SerializeField]
        private int             spinAttackUseSP;
        public int              SpinAttackUseSP => spinAttackUseSP;

        [SerializeField]
        private int             guardUseSP;
        public int              GuardUseSP => guardUseSP;

        [SerializeField]
        private int             counterAttackUseSP;
        public int              CounterAttackUseSP => counterAttackUseSP;

        [SerializeField]
        private int             chargeAttackUseSP;
        public int              ChargeAttackUseSP => chargeAttackUseSP;



        private Timer           spRecoveryCoolDown = new Timer();

        //canvas�Ƀv���C���[�̃X�e�[�^�X(HP�ASP)��UI��\�����邽�߂̐錾
        private PlayerUIHandler playerUIHandler;

        private SaveManager     saveManager;

        public void DoSetup(IPlayerSetup actor)
        {
            playerUIHandler = actor.gameObject.GetComponent<PlayerUIHandler>();
            saveManager = actor.gameObject.GetComponent<SaveManager>();
        }

        public void Initilaize()
        {
            SetStatus();
            base.Setup();
            sp = maxSp;
            saveManager.MyDestory();
        }
        /*
         * �Q�[���J�n���ɃZ���N�g��ʂŐݒ肵�Ă���X�e�[�^�X��
         * �ǉ�
         */
        private void SetStatus()
        {
            List<SaveStatusData> datas = saveManager.LoadGame();
            for(int i = 0; i < datas.Count; i++)
            {
                switch (datas[i].type)
                {
                    case StatusType.HP:
                        float hp = maxHP;
                        hp *= datas[i].hpRatio;
                        maxHP += (int)hp;
                        break;
                    case StatusType.SP:
                        float sp = maxSp;
                        sp *= datas[i].spRatio;
                        maxSp += (int)sp;
                        break;
                    case StatusType.Speed:
                        float speed = baseSpeed;
                        speed *= datas[i].speedRatio;
                        baseSpeed += speed;
                        break;
                    case StatusType.Power:
                        float power = basePower;
                        power *= datas[i].powerRatio;
                        basePower += power;
                        break;
                    case StatusType.Defense:
                        float defense = baseDefense;
                        defense *= datas[i].defenseRatio;
                        baseDefense += defense;
                        break;
                }
            }
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
