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
        private ISlimeAnimator animator;

        private SlimeBodyAttackController attackObject;

        [SerializeField]
        private float movePower;

        public static readonly string StateKey = "Attack";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup enemy)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(SlimeIdleState.StateKey)) { re.Add(new IsNotSlimeAttackTransition(enemy, StateChanger, SlimeIdleState.StateKey)); }
            if (StateChanger.IsContain(SlimeDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(enemy, StateChanger, SlimeDamageState.StateKey)); }
            return re;
        }

        public override void DoSetup(ISlimeSetup actor)
        {
            base.DoSetup(actor);
            thisTransform = actor.gameObject.transform;
            animator = actor.SlimeAnimator;
            movement = actor.Movement;
            attackObject = actor.AttackObject;
        }

        public override void DoStart()
        {
            base.DoStart();

            movement.Move(0);

            animator.Animator.SetInteger(animator.AttacksName, 1);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            attackObject.EnabledCollider(0f, 0f, true);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.ForwardLerpMove(thisTransform.position, movePower);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.AttacksName, -1);
            movement.Move(0);
        }
    }
}
