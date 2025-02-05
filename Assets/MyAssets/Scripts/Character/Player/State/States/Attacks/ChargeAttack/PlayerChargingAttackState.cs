using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class PlayerChargingAttackState : PlayerStateBase
    {
        private IVelocityComponent      velocity;

        private IMovement               movement;
        
        private IPlayerAnimator         animator;

        private IRotation               rotation;

        private SwordController         sword;

        private PlayerEffectController  effectController;

        private Timer                   chargeTimer = new Timer();

        [SerializeField]
        private float                   attacksGravityMultiply;

        [SerializeField]
        private float                   count = 3.0f;

        public static readonly string   StateKey = "ChargingAttack";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            
            if (StateChanger.IsContain(PlayerChargeAttackState.StateKey)) { re.Add(new IsPlayerChargeAttackTransition(actor, StateChanger, PlayerChargeAttackState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            velocity = player.Velocity;
            movement = player.Movement;
            animator = player.PlayerAnimator;
            rotation = player.Rotation;
            sword = player.Equipment.HaveWeapon?.GetComponent<SwordController>();
            effectController = player.gameObject.GetComponent<PlayerEffectController>();
        }
        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger("ChargeAttack", 1);
            velocity.Rigidbody.velocity = Vector3.zero;
            chargeTimer.Start(count);
        }

        public override void DoUpdate(float time)
        {
            chargeTimer.Update(time);

            if (chargeTimer.Current < 3.0f && chargeTimer.Current > 2.0f)
            {
                effectController.SetChargeEffect(true,Color.white);
            }
            else if (chargeTimer.Current < 2.0f && chargeTimer.Current > 1.0f)
            {
                effectController.SetChargeEffect(true,Color.yellow);
            }
            else if(chargeTimer.Current < 1.0f && chargeTimer.Current > 0.0f)
            {
                effectController.SetChargeEffect(true,Color.red);
            }

            base.DoUpdate(time);
            rotation.DoUpdate();
        }
        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Stop();
            velocity.Rigidbody.velocity += Physics.gravity * attacksGravityMultiply * time;
            rotation.DoFixedUpdate();
        }

        public override void DoExit()
        {
            base.DoExit();
            float ratio = chargeTimer.Current;
            ratio -= count;
            ratio = Mathf.Abs(ratio);
            sword.SetRatioPower(ratio + 1.0f);
            effectController.SetChargeEffect(false, Color.white);
        }
    }
}
