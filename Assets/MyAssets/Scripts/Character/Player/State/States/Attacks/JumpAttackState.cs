using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class JumpAttackState : PlayerStateBase
    {
        private IVelocityComponent velocity;

        private IMovement movement;

        private IPlayerAnimator animator;

        [SerializeField]
        private float jumpAttackGravityMultiply = 1.5f;

        [SerializeField]
        private float moveSpeed;

        public static readonly string StateKey = "JumpAttack";
        public override string Key => StateKey;

        public override List<IPlayerStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<IPlayerStateTransition<string>> re = new List<IPlayerStateTransition<string>>();
            if (StateChanger.IsContain(JumpAttackLandingState.StateKey)) { re.Add(new IsGroundTransition(actor, StateChanger, JumpAttackLandingState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            velocity = player.Velocity;
            movement = player.Movement;
            animator = player.PlayerAnimator;
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);

        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(moveSpeed);
            velocity.Rigidbody.velocity += Physics.gravity * jumpAttackGravityMultiply * time;
        }
    }
}
