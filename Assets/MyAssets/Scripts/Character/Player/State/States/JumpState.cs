using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class JumpState : PlayerStateBase
    {
        private IPlayerStauts status;

        private IMovement movement;

        private IMoveInputProvider moveInput;

        private IVelocityComponent velocity;

        private IRotation rotation;

        private IPlayerAnimator animator;

        private IGroundCheck groundCheck;

        private IObstacleJudgment cliffJudgment;

        private SEHandler seHandler;

        [SerializeField]
        private float power;
        [SerializeField]
        private float jumpGravityMultiply;

        [SerializeField]
        private float dashMagnification = 1.5f;

        [SerializeField]
        private float jumpStartCount = 0.25f;
        private Timer jumpStartTimer = new Timer();

        public static readonly string StateKey = "Jump";
        public override string Key => StateKey;
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(FallState.StateKey)) { re.Add(new IsJumpToFallTransition(actor, StateChanger, FallState.StateKey)); }
            if (StateChanger.IsContain(LandingState.StateKey)) { re.Add(new IsNotJumpTransition(actor,jumpStartTimer, StateChanger, LandingState.StateKey)); }
            if (StateChanger.IsContain(ClimbState.StateKey)) { re.Add(new IsClimbTransition(actor, StateChanger, ClimbState.StateKey)); }
            if (StateChanger.IsContain(ReadyJumpAttack.StateKey)) { re.Add(new IsReadyJumpAttackTransition(actor,status.SpinAttackUseSP, StateChanger, ReadyJumpAttack.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            status = player.Stauts;
            base.DoSetup(player);
            movement = player.Movement;
            moveInput = player.MoveInput;
            velocity = player.Velocity;
            rotation = player.Rotation;
            animator = player.PlayerAnimator;
            groundCheck = player.GroundCheck;
            cliffJudgment = player.ObstacleJudgment;
            seHandler = player.SEHandler;
        }

        public override void DoStart()
        {
            base.DoStart();

            jumpStartTimer.Start(jumpStartCount);

            seHandler.Play((int)PlayerSETag.Jump);

            float p = power;
            if (moveInput.IsMove)
            {
                animator.Animator.SetInteger(animator.JumpTypeName, 1);
            }
            else
            {
                animator.Animator.SetInteger(animator.JumpTypeName, 0);
                p /= 1.5f;
            }

            velocity.Rigidbody.AddForce(Vector3.up * p,ForceMode.Impulse);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            jumpStartTimer.Update(time);

            groundCheck.FallTimeUpdate();
            cliffJudgment.RayCheck();
            rotation.DoUpdate();
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            if (animator.Animator.GetInteger(animator.JumpTypeName) == 1)
            {
                float speed = status.BaseSpeed;
                if (moveInput.Dash > 0)
                {
                    speed *= dashMagnification;
                }
                movement.Move(speed);
            }
            velocity.Rigidbody.velocity += Physics.gravity * jumpGravityMultiply * time;
            rotation.DoFixedUpdate();
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.JumpTypeName, -1);
        }
    }
}
