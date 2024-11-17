using MyAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class SlimeDamageState : SlimeStateBase
    {
        private IBaseStauts stauts;

        private Transform thisTransform;

        private IVelocityComponent velocity;

        private IMovement movement;

        private ISlimeAnimator animator;

        private IDamageContainer damageContainer;

        private IDamagement damageMove;

        private IGroundCheck groundCheck;

        private FieldOfView fieldOfView;

        private Timer damageTimer = new Timer();

        private Timer invincibilityTimer = new Timer();

        [SerializeField]
        private float knockBack = 5.0f;

        [SerializeField]
        private float decreaseForce = 0.9f;

        [SerializeField]
        private float damageGravityMultiply = 2.0f;

        [SerializeField]
        private float damageIdleCount = 0.5f;

        public static readonly string StateKey = "Damage";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(SlimeIdleState.StateKey)) { re.Add(new IsNotDamageToTransition(actor, damageTimer, StateChanger, SlimeIdleState.StateKey)); }
            if (StateChanger.IsContain(ChaseState.StateKey)) { re.Add(new IsTimerTargetInViewTransition(actor, damageTimer, StateChanger, ChaseState.StateKey)); }
            if (StateChanger.IsContain(SlimeDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, SlimeDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(ISlimeSetup enemy)
        {
            base.DoSetup(enemy);
            stauts = enemy.BaseStauts;
            thisTransform = enemy.gameObject.transform;
            velocity = enemy.Velocity;
            movement = enemy.Movement;
            animator = enemy.SlimeAnimator;
            groundCheck = enemy.GroundCheck;
            damageContainer = enemy.DamageContainer;
            damageMove = enemy.Damagement;
            fieldOfView = enemy.gameObject.GetComponent<FieldOfView>();
        }

        public override void DoStart()
        {
            base.DoStart();
            FoundTarget();

            velocity.Rigidbody.velocity = Vector3.zero;

            AttackType type = damageContainer.AttackType;
            int damageType = 0;
            damageMove.AddForceMove(thisTransform.position, damageContainer.Attacker.position, knockBack * 1.0f);
            damageTimer.Start(damageIdleCount);
            if (!stauts.DecreaseAndDeathCheck(damageContainer.Data))
            {
                animator.Animator.SetInteger("Impact", damageType);
            }
        }

        private void FoundTarget()
        {
            fieldOfView.SetAllSearch(true);
            Vector3 target = damageContainer.Attacker.position;
            target = new Vector3(target.x, thisTransform.position.y, target.z);
            thisTransform.LookAt(target);
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
            fieldOfView.SetAllSearch(false);
            damageContainer.SetData(0);
            damageContainer.SetAttacker(null);
            damageContainer.SetAttackType(AttackType.None);
            animator.Animator.SetInteger("Impact", -1);
        }
    }
}
