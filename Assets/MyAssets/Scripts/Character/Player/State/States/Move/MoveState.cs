using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class MoveState : PlayerStateBase
    {
        private IPlayerStauts stauts;

        private IMoveInputProvider input;

        private IFocusInputProvider focusInputProvider;
        
        private IVelocityComponent velocity;
        
        private IMovement movement;
        
        private IRotation rotation;
        
        private IStepClimberJudgment stepClimberJudgment;
        
        private IPlayerAnimator animator;

        private IAllIK ik;

        [SerializeField]
        private float moveGravityMultiply;
        [SerializeField]
        private float dashMagnification = 1.5f;

        public static readonly string StateKey = "Move";

        public override string Key => StateKey;
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotMoveTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(BattleMoveState.StateKey)) { re.Add(new IsBattleModeMoveTransition(actor, StateChanger, BattleMoveState.StateKey)); }
            if (StateChanger.IsContain(BattleIdleState.StateKey)) { re.Add(new IsBattleModeIdleTransition(actor, StateChanger, BattleIdleState.StateKey)); }
            if (StateChanger.IsContain(JumpState.StateKey)) { re.Add(new IsJumpPushTransition(actor, StateChanger, JumpState.StateKey)); }
            if (StateChanger.IsContain(FallState.StateKey)) { re.Add(new IsNotGroundTransition(actor, StateChanger, FallState.StateKey)); }
            if (StateChanger.IsContain(ClimbState.StateKey)) { re.Add(new IsClimbTransition(actor, StateChanger, ClimbState.StateKey)); }
            if (StateChanger.IsContain(FirstAttackState.StateKey)) { re.Add(new IsFirstAttackTransition(actor, StateChanger, FirstAttackState.StateKey)); }
            if (StateChanger.IsContain(PlayerChargeAttackStartState.StateKey)) { re.Add(new IsPlayerChargeStartTransition(actor, StateChanger, PlayerChargeAttackStartState.StateKey)); }
            if (StateChanger.IsContain(WeaponOutState.StateKey)) { re.Add(new IsWeaponOutTransition(actor, StateChanger, WeaponOutState.StateKey)); }
            if (StateChanger.IsContain(WeaponInState.StateKey)) { re.Add(new IsWeaponInTransition(actor, StateChanger, WeaponInState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            stauts = player.Stauts;
            movement = player.Movement;
            velocity = player.Velocity;
            //cliffJudgment = player.ObstacleJudgment;
            stepClimberJudgment = player.StepClimberJudgment;
            rotation = player.Rotation;
            input = player.gameObject.GetComponent<IMoveInputProvider>();
            focusInputProvider = player.gameObject.GetComponent<IFocusInputProvider>();
            animator = player.PlayerAnimator;
            ik = player.FootIK;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetFloat(animator.VelocityX, 0f);
            animator.Animator.SetFloat(animator.VelocityZ, 0f);
            animator.Animator.SetFloat(animator.BattleModeName, 0.0f);

            if (focusInputProvider.Foucus > 0)
            {
                animator.Animator.SetFloat(animator.BattleModeName, 1.0f);
            }
            else
            {
                animator.Animator.SetFloat(animator.BattleModeName, 0.0f);
            }
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);

            DashUpdate();
            animator.Animator.SetFloat(animator.MoveName, velocity.CurrentVelocity.magnitude, 0.1f, Time.deltaTime);

            animator.UpdateWeight();

            stepClimberJudgment.HandleStepClimbing();
            rotation.DoUpdate();
        }

        private void DashUpdate()
        {
            if(stauts.SP > 0 && input.Dash > 0)
            {
                animator.Animator.SetFloat(animator.DashName, input.Dash, 0.1f, Time.deltaTime);
                stauts.DecreaseSP(1);
            }
            else
            {
                animator.Animator.SetFloat(animator.DashName, 0, 0.1f, Time.deltaTime);
                stauts.RecoverySP(1);
            }
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            float speed = stauts.BaseSpeed;
            if(stauts.SP > 0 && input.Dash > 0)
            {
                speed *= dashMagnification;
            }
            movement.Move(speed);
            movement.StartClimbStep(stepClimberJudgment.StepGolePosition);
            velocity.Rigidbody.velocity += Physics.gravity * moveGravityMultiply * time;
            rotation.DoFixedUpdate();
        }

        public override void DoLateUpdate(float time)
        {
            base.DoLateUpdate(time);
            ik.DoHeadIKUpdate();
        }

    }
}
