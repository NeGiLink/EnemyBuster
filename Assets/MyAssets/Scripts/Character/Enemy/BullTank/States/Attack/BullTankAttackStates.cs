using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class BullTankNormalAttackState : BullTankStateBase
    {
        private IMovement movement;
        private IVelocityComponent velocity;
        private Transform thisTransform;
        private FieldOfView fieldOfView;

        private IDamageContainer damageContainer;

        private IBullTankAnimator animator;

        private AxeController weapon;

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private float highMoveSpeed;

        [SerializeField]
        private float rotationSpeed = 8;
        [SerializeField]
        private float moveSpeedChangeRate = 8;

        [SerializeField]
        private float minChaseDistance = 2.5f;

        [SerializeField]
        private float maxDistance = 5f;

        [SerializeField]
        private float gravityMultiply;

        public static readonly string StateKey = "NormalAttack";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BullTankIdleState.StateKey)) { re.Add(new IsNotBullTankAttackTransition(actor, "Attack_01", StateChanger, BullTankIdleState.StateKey)); }
            if (StateChanger.IsContain(BullTankDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(actor, StateChanger, BullTankDamageState.StateKey)); }
            if (StateChanger.IsContain(BullTankDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, BullTankDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IBullTankSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
            thisTransform = actor.gameObject.transform;
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
            animator = actor.BullTankAnimator;
            damageContainer = actor.DamageContainer;
            weapon = actor.AttackObject;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger("Attack", 0);

            weapon.SetAttackType(AttackType.Single);
        }

        public override void DoUpdate(float time)
        {
            weapon.EnabledCollider(0.5f, 0.8f, false);
            base.DoUpdate(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            if (animInfo.normalizedTime < 0.6f)
            {
                movement.ForwardLerpMove(thisTransform.position, moveSpeed);
            }
            else
            {
                movement.Stop();
            }
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger("Attack", -1);
            movement.Stop();
            weapon.NotEnabledCollider();
        }

    }
}
