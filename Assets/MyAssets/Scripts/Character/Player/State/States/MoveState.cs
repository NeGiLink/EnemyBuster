using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class MoveState : PlayerStateBase
    {
        private IMovement movement;
        private IMoveInputProvider input;
        private IVelocityComponent velocity;
        private IPlayerAnimator animator;
        [SerializeField]
        private float moveSpeed = 4.0f;

        [SerializeField]
        private float dashMagnification = 1.5f;

        public static readonly string StateKey = "Move";

        public override string Key => StateKey;
        public override List<IPlayerStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<IPlayerStateTransition<string>> re = new List<IPlayerStateTransition<string>>();
            if (StateChanger.IsContain(IdleState.StateKey)) { re.Add(new IsNotMoveTransition(actor, StateChanger, IdleState.StateKey)); }
            if (StateChanger.IsContain(JumpState.StateKey)) { re.Add(new IsJumpPushTransition(actor, StateChanger, JumpState.StateKey)); }
            if (StateChanger.IsContain(FallState.StateKey)) { re.Add(new IsNotGroundTransition(actor, StateChanger, FallState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            movement = player.Movement;
            velocity = player.Velocity;
            input = player.gameObject.GetComponent<IMoveInputProvider>();
            animator = player.PlayerAnimator;
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            animator.Animator.SetFloat("Dash", input.Dash, 0.1f, Time.deltaTime);
            animator.Animator.SetFloat("Speed", velocity.CurrentVelocity.magnitude, 0.1f, Time.deltaTime);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            float speed = moveSpeed;
            if(input.Dash > 0)
            {
                speed *= dashMagnification;
            }
            movement.Move(speed);
        }
    }
}
