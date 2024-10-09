using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class FallState : PlayerStateBase
    {
        private IMovement movement;
        private IVelocityComponent velocity;
        private IMoveInputProvider input;

        private IPlayerAnimator animator;

        [SerializeField]
        float moveSpeed;
        [SerializeField]
        float fallGravityMultiply;

        public static readonly string StateKey = "Fall";
        public override string Key => StateKey;

        public override List<IPlayerStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<IPlayerStateTransition<string>> re = new List<IPlayerStateTransition<string>>();
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsGroundTransition(actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(IdleState.StateKey)) { re.Add(new IsGroundTransition(actor, StateChanger, IdleState.StateKey)); }
            //if (StateChanger.IsContain(LandingState.StateKey)) { re.Add(new IsGroundTransition(actor, StateChanger, LandingState.StateKey)); }
            return re;
        }


        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            movement = player.Movement;
            velocity = player.Velocity;
            animator = player.PlayerAnimator;
        }
        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger("Fall", 1);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(moveSpeed);
            velocity.Rigidbody.velocity += Physics.gravity * fallGravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger("Fall", 0);
        }
    }
}
