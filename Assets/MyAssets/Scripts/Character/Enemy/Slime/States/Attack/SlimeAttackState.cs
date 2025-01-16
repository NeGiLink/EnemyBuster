using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

namespace MyAssets
{
    [System.Serializable]
    public class SlimeAttackState : SlimeStateBase
    {
        private Transform thisTransform;
        private IMovement movement;
        private IVelocityComponent velocity;
        private ISlimeAnimator animator;

        private SlimeBodyAttackController attackObject;

        [SerializeField]
        private float movePower;

        [SerializeField]
        private float gravityMultiply;

        public static readonly string StateKey = "Attack";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup enemy)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(SlimeIdleState.StateKey)) { re.Add(new IsNotSlimeAttackTransition(enemy, StateChanger, SlimeIdleState.StateKey)); }
            if (StateChanger.IsContain(SlimeDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(enemy, StateChanger, SlimeDamageState.StateKey)); }
            if (StateChanger.IsContain(SlimeDeathState.StateKey)) { re.Add(new IsDeathTransition(enemy, StateChanger, SlimeDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(ISlimeSetup actor)
        {
            base.DoSetup(actor);
            thisTransform = actor.gameObject.transform;
            animator = actor.SlimeAnimator;
            movement = actor.Movement;
            velocity = actor.Velocity;
            attackObject = actor.AttackObject;
        }

        public override void DoStart()
        {
            base.DoStart();

            movement.Stop();

            animator.Animator.SetInteger(animator.AttacksName, 1);

            attackObject.SetAttackType(AttackType.Single);
        }

        public override void DoUpdate(float time)
        {
            attackObject.EnabledCollider(0.2f, 0.6f, false);
            base.DoUpdate(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);

            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            if(animInfo.normalizedTime < 0.6f)
            {
                movement.ForwardLerpMove(thisTransform.position, movePower);
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
            animator.Animator.SetInteger(animator.AttacksName, -1);
            movement.Stop();
            attackObject.NotEnabledCollider();
        }
    }
}
