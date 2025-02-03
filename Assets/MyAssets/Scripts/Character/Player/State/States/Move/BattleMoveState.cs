using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class BattleMoveState : PlayerStateBase
    {
        private IPlayerStauts stauts;

        private IMoveInputProvider input;


        private IVelocityComponent velocity;

        private IMovement movement;

        private IRotation rotation;

        private IPlayerAnimator animator;

        private IEquipment equipment;

        private IAllIK ik;


        [SerializeField]
        private float moveGravityMultiply;

        public static readonly string StateKey = "BattleMove";

        public override string Key => StateKey;
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BattleIdleState.StateKey)) { re.Add(new IsNotMoveTransition(actor, StateChanger, BattleIdleState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotBattleModeMoveTransition(actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotBattleModeIdleTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(RollingState.StateKey)) { re.Add(new IsRollingTransition(actor, StateChanger, RollingState.StateKey)); }
            if (StateChanger.IsContain(FallState.StateKey)) { re.Add(new IsNotGroundTransition(actor, StateChanger, FallState.StateKey)); }
            if (StateChanger.IsContain(FirstAttackState.StateKey)) { re.Add(new IsFirstAttackTransition(actor, StateChanger, FirstAttackState.StateKey)); }
            if (StateChanger.IsContain(WeaponOutState.StateKey)) { re.Add(new IsWeaponOutTransition(actor, StateChanger, WeaponOutState.StateKey)); }
            if (StateChanger.IsContain(WeaponInState.StateKey)) { re.Add(new IsWeaponInTransition(actor, StateChanger, WeaponInState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }

            if (StateChanger.IsContain(GuardState.StateKey)) { re.Add(new IsGuardTransition(actor, StateChanger, GuardState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            stauts = player.Stauts;
            base.DoSetup(player);
            movement = player.Movement;
            velocity = player.Velocity;
            rotation = player.Rotation;
            input = player.gameObject.GetComponent<IMoveInputProvider>();
            animator = player.PlayerAnimator;
            equipment = player.gameObject.GetComponent<IEquipment>();
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
            animator.Animator.SetFloat(animator.VelocityX, input.Horizontal, 0.1f, Time.deltaTime);
            animator.Animator.SetFloat(animator.VelocityZ, input.Vertical, 0.1f, Time.deltaTime);

            animator.UpdateWeight();

            if (stauts.SP > 0 && input.Dash > 0)
            {
                stauts.DecreaseSP(1);
            }
            else
            {
                stauts.RecoverySP(1);
            }

            rotation.DoUpdate();
            base.DoUpdate(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            float speed = stauts.BaseSpeed;

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
    }
}
