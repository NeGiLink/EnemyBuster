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

        private IGuardTrigger guardTrigger;

        private IPlayerStauts stauts;

        private PlayerEffectController effectController;

        [SerializeField]
        private float guardSpeed = 0.0f;
        [SerializeField]
        private float idleGravityMultiply;

        public static readonly string StateKey = "Guard";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(CounterAttackState.StateKey)) { re.Add(new IsCounterAttackTransition(actor,stauts.CounterAttackUseSP, "Shield Impact", StateChanger, CounterAttackState.StateKey)); }
            if (StateChanger.IsContain(BattleIdleState.StateKey)) { re.Add(new IsEndGuardTransition(actor, StateChanger, BattleIdleState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); } 
            return re;
        }
        public override void DoSetup(IPlayerSetup actor)
        {
            stauts = actor.Stauts;
            base.DoSetup(actor);
            velocity = actor.Velocity;
            movement = actor.Movement;
            rotation = actor.Rotation;
            animator = actor.PlayerAnimator;
            equipment = actor.gameObject.GetComponent<IEquipment>();
            effectController = actor.gameObject.GetComponent<PlayerEffectController>();
            guardTrigger = actor.GuardTrigger;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger("SuccessGuard", 0);

            equipment.ShieldTool.ShieldOpen();

            stauts.DecreaseSP(stauts.GuardUseSP);
            
            effectController.Create(PlayerEffectType.GroundHit);
        }

        public override void DoUpdate(float time)
        {
            rotation.DoUpdate();
            base.DoUpdate(time);
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
            guardTrigger.SetGuardFlag(false);
            animator.Animator.SetInteger("SuccessGuard", -1);
        }
    }
}

