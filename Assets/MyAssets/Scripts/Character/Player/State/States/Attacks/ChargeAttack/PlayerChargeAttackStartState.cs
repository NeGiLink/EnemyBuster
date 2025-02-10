using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class PlayerChargeAttackStartState : PlayerStateBase
    {
        private IVelocityComponent velocity;

        private IPlayerAnimator animator;

        private IBattleFlagger battleFlagger;

        private IEquipment equipment;

        [SerializeField]
        private float attacksGravityMultiply;

        private IRotation rotation;

        public static readonly string StateKey = "ChargeAttackStart";
        public override string Key => StateKey;

        private readonly string currentMotionName = "ChargeAttackStart";
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(PlayerChargingAttackState.StateKey)) { re.Add(new IsPlayerEndMotionTransition(actor, currentMotionName, StateChanger, PlayerChargingAttackState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            velocity = actor.Velocity;
            rotation = actor.Rotation;
            animator = actor.PlayerAnimator;
            battleFlagger = actor.BattleFlagger;
            equipment = actor.Equipment;
        }
        public override void DoStart()
        {
            base.DoStart();
            if (!battleFlagger.IsBattleMode)
            {
                equipment.SetOutWeapon();
                battleFlagger.SetBattleMode(true);
                animator.Animator.SetFloat(animator.ToolLevel, 1.0f);
            }
            animator.Animator.SetInteger("ChargeAttack",0);
            velocity.Rigidbody.velocity = Vector3.zero;

        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            rotation.DoUpdate();
        }
        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            rotation.DoFixedUpdate();
        }

        public override void DoExit()
        {
            base.DoExit();
        }
    }
}
