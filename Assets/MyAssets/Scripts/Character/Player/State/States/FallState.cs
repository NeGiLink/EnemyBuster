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
        private ICharacterRotation rotation;
        private IObstacleJudgment cliffJudgment;
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
            if (StateChanger.IsContain(LandingState.StateKey)) { re.Add(new IsGroundTransition(actor, StateChanger, LandingState.StateKey)); }
            if (StateChanger.IsContain(ClimbState.StateKey)) { re.Add(new IsClimbTransition(actor, StateChanger, ClimbState.StateKey)); }
            return re;
        }


        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            movement = player.Movement;
            velocity = player.Velocity;
            rotation = player.Rotation;
            cliffJudgment = player.ObstacleJudgment;
            animator = player.PlayerAnimator;
        }
        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger(animator.FallName, 1);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            cliffJudgment.RayCheck();
            rotation.DoUpdate();
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(moveSpeed);
            velocity.Rigidbody.velocity += Physics.gravity * fallGravityMultiply * time;
            rotation.DoFixedUpdate(velocity.CurrentVelocity);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.FallName, 0);
        }
    }
}
