using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class GuardState : PlayerStateBase
    {

        private IVelocityComponent velocity;

        private IMovement movement;

        private IRotation rotation;

        private IPlayerAnimator animator;

        private IEquipment equipment;

        [SerializeField]
        private float guardSpeed = 0.0f;
        [SerializeField]
        private float idleGravityMultiply;

        public static readonly string StateKey = "Guard";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            //if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsEndGuardTransition(actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(BattleIdleState.StateKey)) { re.Add(new IsEndGuardTransition(actor, StateChanger, BattleIdleState.StateKey)); }
            //if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsEndGuardTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            
            
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            velocity = player.Velocity;
            movement = player.Movement;
            rotation = player.Rotation;
            animator = player.PlayerAnimator;
            equipment = player.gameObject.GetComponent<IEquipment>();
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger("SuccessGuard",0);
            equipment.ShieldTool.ShieldOpen();
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            rotation.DoUpdate();
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);

            movement.Move(guardSpeed);

            velocity.Rigidbody.velocity += Physics.gravity * idleGravityMultiply * time;

            rotation.DoFixedUpdate();
        }

        public override void DoExit()
        {
            base.DoExit();
            equipment.ShieldTool.SetSuccess(false);
            animator.Animator.SetInteger("SuccessGuard", -1);
        }
    }
}

