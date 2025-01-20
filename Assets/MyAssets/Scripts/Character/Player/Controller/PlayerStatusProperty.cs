using System.Collections.Generic;
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

        //canvasにプレイヤーのステータス(HP、SP)のUIを表示するための宣言
        private PlayerUIHandler playerUIHandler;

        private SaveManager saveManager;

        public void Setup(IPlayerSetup actor)
        {
            playerUIHandler = actor.gameObject.GetComponent<PlayerUIHandler>();
            saveManager = actor.gameObject.GetComponent<SaveManager>();
        }

        public void Initilaize()
        {
            SetStatus();
            base.Setup();
            saveManager.MyDestory();
        }

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
                        maxHP = (int)hp;
                        break;
                    case StatusType.SP:
                        float sp = maxSp;
                        sp *= datas[i].spRatio;
                        maxSp = (int)sp;
                        break;
                    case StatusType.Speed:
                        float speed = baseSpeed;
                        speed *= datas[i].speedRatio;
                        baseSpeed = speed;
                        break;
                    case StatusType.Power:
                        float power = basePower;
                        power *= datas[i].powerRatio;
                        basePower = power;
                        break;
                    case StatusType.Defense:
                        float defense = baseDefense;
                        defense *= datas[i].defenseRatio;
                        baseDefense = defense;
                        break;
                }
            }
        }

        //各自値を更新した時にUIも変更
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
