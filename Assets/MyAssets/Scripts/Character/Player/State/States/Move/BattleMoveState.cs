using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class BattleMoveState : PlayerStateBase
    {
        private IMoveInputProvider input;

        private IFocusInputProvider focusInputProvider;

        private IVelocityComponent velocity;

        private IMovement movement;

        private IRotation rotation;

        private IObstacleJudgment cliffJudgment;

        private IStepClimberJudgment stepClimberJudgment;

        private IPlayerAnimator animator;

        private IEquipment equipment;

        private IDamageContainer damageContainer;

        private IAllIK ik;

        [SerializeField]
        private float moveSpeed = 4.0f;
        [SerializeField]
        private float moveGravityMultiply;

        public static readonly string StateKey = "BattleMove";

        public override string Key => StateKey;
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BattleIdleState.StateKey)) { re.Add(new IsNotMoveTransition(actor, StateChanger, BattleIdleState.StateKey)); }
            if (StateChanger.IsContain(RollingState.StateKey)) { re.Add(new IsRollingTransition(actor, StateChanger, RollingState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotBattleModeTransition(actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(FallState.StateKey)) { re.Add(new IsNotGroundTransition(actor, StateChanger, FallState.StateKey)); }
            if (StateChanger.IsContain(FirstAttackState.StateKey)) { re.Add(new IsFirstAttackTransition(actor, StateChanger, FirstAttackState.StateKey)); }
            if (StateChanger.IsContain(WeaponOutState.StateKey)) { re.Add(new IsWeaponOutTransition(actor, StateChanger, WeaponOutState.StateKey)); }
            if (StateChanger.IsContain(WeaponInState.StateKey)) { re.Add(new IsWeaponInTransition(actor, StateChanger, WeaponInState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
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
            focusInputProvider = player.gameObject.GetComponent<IFocusInputProvider>();
            animator = player.PlayerAnimator;
            equipment = player.gameObject.GetComponent<IEquipment>();
            damageContainer = player.DamageContainer;
            ik = player.FootIK;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetFloat(animator.DashName, 0);

            animator.Animator.SetFloat(animator.BattleModeName, Define.PressNum);

            animator.SetWeight(true, (int)Define.PressNum);
            equipment.ShieldTool.ShieldOpen();
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            animator.Animator.SetFloat(animator.VelocityX, input.Horizontal, 0.1f, Time.deltaTime);
            animator.Animator.SetFloat(animator.VelocityZ, input.Vertical, 0.1f, Time.deltaTime);

            animator.UpdateWeight();

            rotation.DoUpdate();
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            float speed = moveSpeed;

            movement.Move(speed);
            velocity.Rigidbody.velocity += Physics.gravity * moveGravityMultiply * time;
            rotation.DoFixedUpdate();
        }

        public override void DoLateUpdate(float time)
        {
            base.DoLateUpdate(time);
            ik.DoHeadIKUpdate();
        }
        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetFloat(animator.VelocityX, 0f);
            animator.Animator.SetFloat(animator.VelocityZ, 0f);
            animator.Animator.SetFloat(animator.BattleModeName, 0.0f);
            animator.SetWeight(false, 1);
            equipment.ShieldTool.ShieldClose();
        }

        public override void DoTriggerEnter(GameObject thisObject,Collider collider)
        {
            base.DoTriggerEnter(thisObject,collider);
            AttackObject data = collider.GetComponent<AttackObject>();
            if (data == null) { return; }

            if (equipment.ShieldTool.IsGuarid(collider.transform, thisObject.transform)){
                return;
            }
            
            damageContainer.SetAttackerData(data.Power, data.Type, collider.transform);
        }
    }
}
