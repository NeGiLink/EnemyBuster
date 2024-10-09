using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class JumpState : PlayerStateBase
    {
        private IMovement movement;

        private IMoveInputProvider moveInput;

        private IVelocityComponent velocity;

        private IPlayerAnimator animator;

        [SerializeField]
        private float power;
        [SerializeField]
        private float jumpGravityMultiply;
        [SerializeField]
        private float moveSpeed;

        public static readonly string StateKey = "Jump";
        public override string Key => StateKey;
        public override List<IPlayerStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<IPlayerStateTransition<string>> re = new List<IPlayerStateTransition<string>>();
            if (StateChanger.IsContain(IdleState.StateKey)) { re.Add(new IsNotJumpTransition(actor, StateChanger, IdleState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotJumpTransition(actor, StateChanger, MoveState.StateKey)); }
            //if (StateChanger.IsContain(FallState.StateKey)) { re.Add(new IsFallVelocityTransition(actor, StateChanger, FallState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            movement = player.Movement;
            moveInput = player.MoveInput;
            velocity = player.Velocity;
            animator = player.PlayerAnimator;
        }

        public override void DoStart()
        {
            base.DoStart();
            if (moveInput.IsMove)
            {
                animator.Animator.SetInteger("JumpType", 1);
            }
            else
            {
                animator.Animator.SetInteger("JumpType", 0);
            }

            velocity.Rigidbody.AddForce(Vector3.up * power,ForceMode.Impulse);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(moveSpeed);
            velocity.Rigidbody.velocity += Physics.gravity * jumpGravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger("JumpType", -1);
        }
    }
}
