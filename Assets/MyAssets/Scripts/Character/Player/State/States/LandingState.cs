using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class LandingState : PlayerStateBase
    {
        private Timer playerTimer = new Timer();

        private IMovement movement;

        private IVelocityComponent velocity;

        private IPlayerAnimator animator;

        [SerializeField]
        private float moveSpeed;

        public static readonly string StateKey = "Landing";
        public override string Key => StateKey;

        public override List<IPlayerStateTransition<string>> CreateTransitionList(IPlayerSetup player)
        {
            List<IPlayerStateTransition<string>> re = new List<IPlayerStateTransition<string>>();
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsTimerAndMoveTransition(player, playerTimer, StateChanger, MoveState.StateKey)); }
            else if (StateChanger.IsContain(IdleState.StateKey)) { re.Add(new IsTimerAndMoveTransition(player, playerTimer, StateChanger, IdleState.StateKey)); }
            if (StateChanger.IsContain(IdleState.StateKey)) { re.Add(new IsTimerAndNotMoveTransition(player, playerTimer, StateChanger, IdleState.StateKey)); }
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

            const float LandingActionTime = 0.1f;
            playerTimer.Start(LandingActionTime);

            velocity.Rigidbody.velocity = Vector3.zero;

            animator.Animator.SetInteger("Land", 0);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            playerTimer.Update(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(moveSpeed);
        }

        public override void DoExit()
        {
            base.DoExit();
            playerTimer.End();
            animator.Animator.SetInteger("Land", -1);
        }
    }
}
