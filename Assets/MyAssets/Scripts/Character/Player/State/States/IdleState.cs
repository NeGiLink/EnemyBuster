using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class IdleState : PlayerStateBase
    {
        private IMovement movement;

        private IVelocityComponent velocity;

        private IObstacleJudgment cliffJudgment;

        private ICharacterRotation rotation;

        private IFootIK footIK;

        private IPlayerAnimator animator;

        [SerializeField]
        private float moveSpeed = 4.0f;
        [SerializeField]
        private float idleGravityMultiply;

        public static readonly string StateKey = "Idle";
        public override string Key => StateKey;

        public override List<IPlayerStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<IPlayerStateTransition<string>> re = new List<IPlayerStateTransition<string>>();
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsMoveTransition(actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(JumpState.StateKey)) { re.Add(new IsJumpPushTransition(actor, StateChanger, JumpState.StateKey)); }
            if (StateChanger.IsContain(FallState.StateKey)) { re.Add(new IsNotGroundTransition(actor, StateChanger, FallState.StateKey)); }
            if (StateChanger.IsContain(ClimbState.StateKey)) { re.Add(new IsClimbTransition(actor, StateChanger, ClimbState.StateKey)); }
            if (StateChanger.IsContain(FirstAttackState.StateKey)) { re.Add(new IsFirstAttackTransition(actor, StateChanger, FirstAttackState.StateKey)); }
            if (StateChanger.IsContain(WeaponOutState.StateKey)) { re.Add(new IsWeaponOutTransition(actor, StateChanger, WeaponOutState.StateKey)); }
            if (StateChanger.IsContain(WeaponInState.StateKey)) { re.Add(new IsWeaponInTransition(actor, StateChanger, WeaponInState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            movement = player.Movement;
            velocity = player.Velocity;
            cliffJudgment = player.ObstacleJudgment;
            rotation = player.Rotation;
            footIK = player.FootIK;
            animator = player.PlayerAnimator;
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            animator.Animator.SetFloat(animator.MoveName, velocity.CurrentVelocity.magnitude, 0.1f, Time.deltaTime);
            cliffJudgment.RayCheck();
            rotation.DoUpdate();
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(moveSpeed);
            velocity.Rigidbody.velocity += Physics.gravity * idleGravityMultiply * time;
            rotation.DoFixedUpdate(velocity.CurrentVelocity);
        }

        public override void DoAnimatorIKUpdate()
        {
            base.DoAnimatorIKUpdate();
            footIK.DoUpdate();
        }
    }
}
