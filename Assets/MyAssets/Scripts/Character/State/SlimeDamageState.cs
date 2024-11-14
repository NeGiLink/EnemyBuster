using MyAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class SlimeDamageState : SlimeStateBase
    {
        private Transform thisTransform;

        private IVelocityComponent velocity;

        private IMovement movement;

        private ISlimeAnimator animator;

        private IDamageContainer damageContainer;

        private IDamagement damageMove;

        private IGroundCheck groundCheck;

        private Timer damageTimer = new Timer();

        private Timer invincibilityTimer = new Timer();

        [SerializeField]
        private float knockBack = 5.0f;

        [SerializeField]
        private float decreaseForce = 0.9f;

        [SerializeField]
        private float damageGravityMultiply = 2.0f;

        public static readonly string StateKey = "Damage";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();

            if (StateChanger.IsContain(SlimeIdleState.StateKey)) { re.Add(new IsNotDamageToTransition(actor, damageTimer, StateChanger, SlimeIdleState.StateKey)); }

            return re;
        }
        public override void DoSetup(ISlimeSetup player)
        {
            base.DoSetup(player);
            thisTransform = player.gameObject.transform;
            velocity = player.Velocity;
            movement = player.Movement;
            animator = player.SlimeAnimator;
            groundCheck = player.GroundCheck;
            damageContainer = player.DamageContainer;
            damageMove = player.Damagement;
        }

        public override void DoStart()
        {
            base.DoStart();
            velocity.Rigidbody.velocity = Vector3.zero;

            AttackType type = damageContainer.AttackType;
            int damageType = 0;
            damageMove.AddForceMove(thisTransform.position, damageContainer.Attacker.position, knockBack * 1.5f);
            damageTimer.Start(0.25f);
            animator.Animator.SetInteger("Impact", damageType);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            damageTimer.Update(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            //AddForceÇ≈ó^Ç¶ÇΩï™Çè≠ÇµÇ∏Ç¬å∏è≠
            Vector3 v = velocity.Rigidbody.velocity;
            velocity.Rigidbody.velocity = v * decreaseForce;
            velocity.Rigidbody.velocity += Physics.gravity * damageGravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            damageContainer.SetData(0);
            damageContainer.SetAttacker(null);
            damageContainer.SetAttackType(AttackType.None);
            animator.Animator.SetInteger("Impact", -1);
        }
    }
}
