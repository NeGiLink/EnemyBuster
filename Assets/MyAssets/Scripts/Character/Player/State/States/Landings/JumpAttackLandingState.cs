using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class JumpAttackLandingState : PlayerStateBase
    {
        private Timer playerTimer = new Timer();

        private IMovement movement;

        private IVelocityComponent velocity;

        private IPlayerAnimator animator;

        [SerializeField]
        private float LandingActionTime;

        public static readonly string StateKey = "JumpAttackLanding";
        public override string Key => StateKey;

        public override List<IPlayerStateTransition<string>> CreateTransitionList(IPlayerSetup player)
        {
            List<IPlayerStateTransition<string>> re = new List<IPlayerStateTransition<string>>();
            if (StateChanger.IsContain(IdleState.StateKey)) { re.Add(new IsTimerTransition(player, playerTimer, StateChanger, IdleState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsTimerTransition(player, playerTimer, StateChanger, MoveState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
            animator = actor.PlayerAnimator;
        }

        public override void DoStart()
        {
            base.DoStart();

            playerTimer.Start(LandingActionTime);

            velocity.Rigidbody.velocity = Vector3.zero;

            animator.Animator.SetInteger(animator.LandName, 0);
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
            animator.Animator.SetInteger(animator.LandName, -1);
            animator.Animator.SetInteger(animator.AttacksName, -1);
        }
    }
}

