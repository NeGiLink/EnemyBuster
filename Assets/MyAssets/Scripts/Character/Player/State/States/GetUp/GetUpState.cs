using System.Collections.Generic;

namespace MyAssets
{
    [System.Serializable]
    public class GetUpState : PlayerStateBase
    {
        private Timer getUpTimer = new Timer();

        private IPlayerAnimator animator;

        public static readonly string StateKey = "GetUp";

        public override string Key => StateKey;
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotPlayerDamageToTransition(actor, getUpTimer, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotPlayerDamageToTransition(actor, getUpTimer, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(BattleIdleState.StateKey)) { re.Add(new IsNotPlayerDamageToBattleTransition(actor, getUpTimer, StateChanger, BattleIdleState.StateKey)); }
            if (StateChanger.IsContain(BattleMoveState.StateKey)) { re.Add(new IsNotPlayerDamageToBattleTransition(actor, getUpTimer, StateChanger, BattleMoveState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            animator = player.PlayerAnimator;
        }

        public override void DoStart()
        {
            base.DoStart();
            getUpTimer.Start(1.1f);
            animator.Animator.SetInteger("GetUp",0);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            getUpTimer.Update(time);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger("GetUp", -1);
        }
    }
}
