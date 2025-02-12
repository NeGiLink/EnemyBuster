using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * プレイヤーのジャンプ攻撃着地状態
     */
    [System.Serializable]
    public class JumpAttackLandingState : PlayerStateBase
    {

        private IVelocityComponent          velocity;

        private IPlayerAnimator             animator;

        private PlayerEffectController      effectController;

        private Timer                       playerTimer = new Timer();

        [SerializeField]
        private float                       LandingActionTime;

        public static readonly string       StateKey = "JumpAttackLanding";
        public override string              Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsTimerTransition(actor, playerTimer, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsTimerTransition(actor, playerTimer, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            velocity = actor.Velocity;
            animator = actor.PlayerAnimator;
            effectController = actor.gameObject.GetComponent<PlayerEffectController>();
        }

        public override void DoStart()
        {
            base.DoStart();

            playerTimer.Start(LandingActionTime);

            velocity.Rigidbody.velocity = Vector3.zero;

            animator.Animator.SetInteger(animator.LandAnimationID, 0);

            effectController.Create(PlayerEffectType.GroundHit);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            playerTimer.Update(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            
        }

        public override void DoExit()
        {
            base.DoExit();
            playerTimer.End();
            animator.Animator.SetInteger(animator.LandAnimationID, -1);
            animator.Animator.SetInteger(animator.AttackAnimationID, -1);
        }
    }
}

