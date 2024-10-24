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
        private IObstacleJudgment cliffJudgment;
        private IStepClimberJudgment stepClimberJudgment;
        private ICharacterRotation rotation;
        private IPlayerAnimator animator;
        private IChangingState changingState;
        [SerializeField]
        private float moveSpeed = 4.0f;
        [SerializeField]
        private float moveGravityMultiply;
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
            stepClimberJudgment = player.StepClimberJudgment;
            rotation = player.Rotation;
            input = player.gameObject.GetComponent<IMoveInputProvider>();
            animator = player.PlayerAnimator;
            changingState = player.ChangingState;
        }

        public override void DoStart()
        {
            base.DoStart();
            cliffJudgment.InitRay();
            if (changingState.IsBattleMode)
            {
                animator.Animator.SetFloat(animator.AlertLevelName, 1.0f);
            }
            else
            {
                animator.Animator.SetFloat(animator.AlertLevelName, 0.0f);
            }
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            animator.Animator.SetFloat(animator.DashName, input.Dash, 0.1f, Time.deltaTime);
            animator.Animator.SetFloat(animator.MoveName, velocity.CurrentVelocity.magnitude, 0.1f, Time.deltaTime);
            cliffJudgment.RayCheck();
            stepClimberJudgment.HandleStepClimbing();
            rotation.DoUpdate();
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
            movement.StartClimbStep(stepClimberJudgment.StepGolePosition);
            velocity.Rigidbody.velocity += Physics.gravity * moveGravityMultiply * time;
            rotation.DoFixedUpdate(velocity.CurrentVelocity);
        }
    }
}
