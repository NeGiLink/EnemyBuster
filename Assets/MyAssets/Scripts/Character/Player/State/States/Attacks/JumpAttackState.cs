using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class JumpAttackState : PlayerStateBase
    {
        private IVelocityComponent velocity;

        private IMovement movement;

        private IPlayerAnimator animator;

        private IDamageContainer damageContainer;

        [SerializeField]
        private float jumpAttackGravityMultiply = 1.5f;

        [SerializeField]
        private float moveSpeed;

        public static readonly string StateKey = "JumpAttack";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(JumpAttackLandingState.StateKey)) { re.Add(new IsGroundTransition(actor, StateChanger, JumpAttackLandingState.StateKey)); }
            //if (StateChanger.IsContain(DamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, DamageState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            velocity = player.Velocity;
            movement = player.Movement;
            animator = player.PlayerAnimator;
            damageContainer = player.DamageContainer;
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);

        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(moveSpeed);
            velocity.Rigidbody.velocity += Physics.gravity * jumpAttackGravityMultiply * time;
        }
        public override void DoTriggerEnter(GameObject thisObject,Collider collider)
        {
            base.DoTriggerEnter(thisObject,collider);
            AttackObject data = collider.GetComponent<AttackObject>();
            if (data == null) { return; }
            damageContainer.SetAttackType(AttackType.Small);
            damageContainer.SetData(data.Power);
            damageContainer.SetAttacker(collider.transform);
        }

    }
}
