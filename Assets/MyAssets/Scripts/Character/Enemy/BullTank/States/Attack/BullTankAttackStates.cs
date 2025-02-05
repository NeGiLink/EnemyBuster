
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

        private IBullTankAnimator animator;

        private AxeController weapon;

        [SerializeField]
        private float moveSpeed;


        [SerializeField]
        private float gravityMultiply;

        private Timer timer = new Timer();
        [SerializeField]
        private float count = 1.0f;

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
            animator = actor.BullTankAnimator;
            weapon = actor.AttackObject;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger(animator.AttackAnimationID, 0);

            weapon.SetAttackType(AttackType.Normal);
            timer.Start(count);
            timer.OnEnd += ActivationSE;
        }

        private void ActivationSE()
        {
            weapon.SlashSE();
        }

        public override void DoUpdate(float time)
        {
            timer.Update(time);
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
            animator.Animator.SetInteger(animator.AttackAnimationID, -1);
            movement.Stop();
            weapon.NotEnabledCollider();
            timer.OnEnd -= ActivationSE;
        }

    }
}
