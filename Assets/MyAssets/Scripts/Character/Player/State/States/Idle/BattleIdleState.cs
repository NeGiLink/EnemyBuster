using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class BattleIdleState : PlayerStateBase
    {
        private IMoveInputProvider input;

        private IFocusInputProvider focusInputProvider;

        private IVelocityComponent velocity;

        private IMovement movement;

        private IRotation rotation;

        private IObstacleJudgment cliffJudgment;

        private IPlayerAnimator animator;

        private IFootIK footIK;

        private FieldOfView fieldOfView;

        private IEquipment equipment;

        private IDamageContainer damageContainer;

        [SerializeField]
        private float moveSpeed = 4.0f;
        [SerializeField]
        private float idleGravityMultiply;

        public static readonly string StateKey = "BattleIdle";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BattleMoveState.StateKey)) { re.Add(new IsMoveTransition(actor, StateChanger, BattleMoveState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotBattleModeTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            
            if (StateChanger.IsContain(JumpState.StateKey)) { re.Add(new IsJumpPushTransition(actor, StateChanger, JumpState.StateKey)); }
            if (StateChanger.IsContain(FallState.StateKey)) { re.Add(new IsNotGroundTransition(actor, StateChanger, FallState.StateKey)); }
            
            if (StateChanger.IsContain(FirstAttackState.StateKey)) { re.Add(new IsFirstAttackTransition(actor, StateChanger, FirstAttackState.StateKey)); }
            if (StateChanger.IsContain(WeaponOutState.StateKey)) { re.Add(new IsWeaponOutTransition(actor, StateChanger, WeaponOutState.StateKey)); }
            if (StateChanger.IsContain(WeaponInState.StateKey)) { re.Add(new IsWeaponInTransition(actor, StateChanger, WeaponInState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            focusInputProvider = player.gameObject.GetComponent<IFocusInputProvider>();
            input = player.gameObject.GetComponent<IMoveInputProvider>();
            velocity = player.Velocity;
            movement = player.Movement;
            rotation = player.Rotation;
            cliffJudgment = player.ObstacleJudgment;
            footIK = player.FootIK;
            animator = player.PlayerAnimator;
            fieldOfView = player.gameObject.GetComponent<FieldOfView>();
            equipment = player.gameObject.GetComponent<IEquipment>();
            damageContainer = player.DamageContainer;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetFloat(animator.BattleModeName, 1.0f);

            //animator.Animator.SetFloat(animator.BattleModeName, 1.0f, 0.1f, Time.deltaTime);
            animator.SetWeight(true, 1);
            equipment.ShieldTool.ShieldOpen();
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            AnimationUpdate();
            rotation.DoUpdate();
        }

        private void AnimationUpdate()
        {
            animator.Animator.SetFloat(animator.MoveName, velocity.CurrentVelocity.magnitude, 0.1f, Time.deltaTime);
            animator.Animator.SetFloat(animator.VelocityX, input.Horizontal, 0.1f, Time.deltaTime);
            animator.Animator.SetFloat(animator.VelocityZ, input.Vertical, 0.1f, Time.deltaTime);


            animator.UpdateWeight();

            if (fieldOfView.TargetObject != null)
            {
                animator.Animator.SetFloat(animator.AlertLevelName, 1.0f, 0.1f, Time.deltaTime);
            }
            else
            {
                animator.Animator.SetFloat(animator.AlertLevelName, 0.0f, 0.1f, Time.deltaTime);
            }
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);

            movement.Move(moveSpeed);

            velocity.Rigidbody.velocity += Physics.gravity * idleGravityMultiply * time;

            rotation.DoFixedUpdate();
        }

        public override void DoAnimatorIKUpdate()
        {
            base.DoAnimatorIKUpdate();
            footIK.DoUpdate();
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetFloat(animator.BattleModeName, 0.0f);
            animator.SetWeight(false, 1);
            equipment.ShieldTool.ShieldClose();
        }
        public override void DoTriggerEnter(Collider collider)
        {
            base.DoTriggerEnter(collider);
            AttackObject data = collider.GetComponent<AttackObject>();
            if (data == null) { return; }
            damageContainer.SetAttackType(data.Type);
            damageContainer.SetData(data.Power);
            damageContainer.SetAttacker(collider.transform);
        }
    }
}
