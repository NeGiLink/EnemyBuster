using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * プレイヤーの戦闘待機状態
     */
    [System.Serializable]
    public class BattleIdleState : PlayerStateBase
    {
        private IPlayerStauts       stauts;

        private IMoveInputProvider  input;

        private IVelocityComponent  velocity;

        private IMovement           movement;

        private IRotation           rotation;

        private IPlayerAnimator     animator;

        private IAllIK              ik;

        private FieldOfView         fieldOfView;

        private IEquipment          equipment;

        [SerializeField]
        private float               idleGravityMultiply;

        public static readonly string StateKey = "BattleIdle";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BattleMoveState.StateKey)) { re.Add(new IsMoveTransition(actor, StateChanger, BattleMoveState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotBattleModeIdleTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(GuardState.StateKey)) { re.Add(new IsGuardTransition(actor, StateChanger, GuardState.StateKey)); }
            if (StateChanger.IsContain(FirstAttackState.StateKey)) { re.Add(new IsFirstAttackTransition(actor, StateChanger, FirstAttackState.StateKey)); }
            if (StateChanger.IsContain(WeaponOutState.StateKey)) { re.Add(new IsWeaponOutTransition(actor, StateChanger, WeaponOutState.StateKey)); }
            if (StateChanger.IsContain(WeaponInState.StateKey)) { re.Add(new IsWeaponInTransition(actor, StateChanger, WeaponInState.StateKey)); }
            if (StateChanger.IsContain(PlayerChargeAttackStartState.StateKey)) { re.Add(new IsPlayerChargeStartTransition(actor, StateChanger, PlayerChargeAttackStartState.StateKey)); }
            if (StateChanger.IsContain(FallState.StateKey)) { re.Add(new IsNotGroundTransition(actor, StateChanger, FallState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }

            return re;
        }
        public override void DoSetup(IPlayerSetup actor)
        {
            stauts = actor.Stauts;
            base.DoSetup(actor);
            input = actor.gameObject.GetComponent<IMoveInputProvider>();
            velocity = actor.Velocity;
            movement = actor.Movement;
            rotation = actor.Rotation;
            ik = actor.IkController;
            animator = actor.PlayerAnimator;
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
            equipment = actor.gameObject.GetComponent<IEquipment>();
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetFloat(animator.BattleModeAnimationID, 1.0f);

            animator.SetWeight(true, 1);
            equipment.ShieldTool.ShieldOpen();
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            AnimationUpdate();

            if (stauts.SP > 0 && input.Dash > 0 && input.IsMove)
            {
                stauts.DecreaseSP(1);
            }
            else
            {
                stauts.RecoverySP(1);
            }

            rotation.DoUpdate();
        }

        private void AnimationUpdate()
        {
            animator.Animator.SetFloat(animator.MoveAnimationID, velocity.CurrentVelocity.magnitude, 0.1f, Time.deltaTime);
            animator.Animator.SetFloat(animator.VelocityXAnimationID, input.Horizontal, 0.1f, Time.deltaTime);
            animator.Animator.SetFloat(animator.VelocityZAnimationID, input.Vertical, 0.1f, Time.deltaTime);


            animator.UpdateWeight();

            if (fieldOfView.FindTarget)
            {
                animator.Animator.SetFloat(animator.AlertLevelAnimationID, 1.0f, 0.1f, Time.deltaTime);
            }
            else
            {
                animator.Animator.SetFloat(animator.AlertLevelAnimationID, 0.0f, 0.1f, Time.deltaTime);
            }
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);

            movement.Move(stauts.BaseSpeed);

            velocity.Rigidbody.velocity += Physics.gravity * idleGravityMultiply * time;

            rotation.DoFixedUpdate();
        }

        public override void DoLateUpdate(float time)
        {
            base.DoLateUpdate(time);
            ik.DoHeadIKUpdate();
        }

        public override void DoAnimatorIKUpdate()
        {
            base.DoAnimatorIKUpdate();
            ik.DoFootIKUpdate();
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetFloat(animator.BattleModeAnimationID, 0.0f);
            animator.SetWeight(false, 1);
            equipment.ShieldTool.ShieldClose();
        }
    }
}
