using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * プレイヤーの待機状態
     */
    [System.Serializable]
    public class PlayerIdleState : PlayerStateBase
    {
        private IPlayerStauts           stauts;

        private IMoveInputProvider      input;
        
        private IFocusInputProvider     focusInputProvider;
        
        private IVelocityComponent      velocity;

        private IMovement               movement;
        
        private IRotation               rotation;

        private IObstacleJudgment       cliffJudgment;

        private IPlayerAnimator         animator;

        private IAllIK                  ik;
        
        private FieldOfView             fieldOfView;

        private IEquipment              equipment;

        private IDamageContainer        damageContainer;

        [SerializeField]
        private float                   idleGravityMultiply;

        public static readonly string   StateKey = "Idle";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsMoveTransition(actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(BattleIdleState.StateKey)) { re.Add(new IsBattleModeIdleTransition(actor, StateChanger, BattleIdleState.StateKey)); }
            if (StateChanger.IsContain(BattleMoveState.StateKey)) { re.Add(new IsBattleModeMoveTransition(actor, StateChanger, BattleMoveState.StateKey)); }
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
        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            stauts = actor.Stauts;
            focusInputProvider = actor.gameObject.GetComponent<IFocusInputProvider>();
            input = actor.gameObject.GetComponent<IMoveInputProvider>();
            velocity = actor.Velocity;
            movement = actor.Movement;
            rotation = actor.Rotation;
            cliffJudgment = actor.ObstacleJudgment;
            ik = actor.IkController;
            animator = actor.PlayerAnimator;
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
            equipment = actor.gameObject.GetComponent<IEquipment>();
            damageContainer = actor.DamageContainer;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetFloat(animator.VelocityXAnimationID, 0f);
            animator.Animator.SetFloat(animator.VelocityZAnimationID, 0f);
            animator.Animator.SetFloat(animator.BattleModeAnimationID, 0.0f);
        }

        public override void DoUpdate(float time)
        {
            AnimationUpdate();
            cliffJudgment.RayCheck();
            rotation.DoUpdate();

            if (stauts.SP > 0 && input.Dash > 0 && input.IsMove)
            {
                stauts.DecreaseSP(1);
            }
            else
            {
                stauts.RecoverySP(1);
            }
            base.DoUpdate(time);
        }

        private void AnimationUpdate()
        {
            animator.Animator.SetFloat(animator.MoveAnimationID, velocity.CurrentVelocity.magnitude, 0.1f, Time.deltaTime);

            animator.UpdateWeight();

            if (fieldOfView.FindTarget)
            {
                animator.Animator.SetFloat(animator.AlertLevelAnimationID, 1.0f,0.1f,Time.deltaTime);
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
    }
}
