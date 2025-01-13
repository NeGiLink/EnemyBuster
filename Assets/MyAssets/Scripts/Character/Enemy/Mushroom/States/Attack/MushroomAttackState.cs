using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class MushroomAttackState : MushroomStateBase
    {
        private Transform thisTransform;
        private IMovement movement;
        private IVelocityComponent velocity;
        private IMushroomAnimator animator;

        private MushroomAttackController attackObject;

        [SerializeField]
        private float movePower;

        [SerializeField]
        private float gravityMultiply;

        public static readonly string StateKey = "Attack";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IMushroomSetup enemy)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(MushroomIdleState.StateKey)) { re.Add(new IsNotMushroomAttackTransition(enemy, StateChanger, MushroomIdleState.StateKey)); }
            if (StateChanger.IsContain(MushroomDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(enemy, StateChanger, MushroomDamageState.StateKey)); }
            if (StateChanger.IsContain(MushroomDeathState.StateKey)) { re.Add(new IsDeathTransition(enemy, StateChanger, MushroomDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IMushroomSetup actor)
        {
            base.DoSetup(actor);
            thisTransform = actor.gameObject.transform;
            animator = actor.MushroomAnimator;
            movement = actor.Movement;
            velocity = actor.Velocity;
            attackObject = actor.AttackObject;
        }

        public override void DoStart()
        {
            base.DoStart();

            movement.Move(0);

            attackObject.SetAttackType(AttackType.Single);
        }

        public override void DoUpdate(float time)
        {
            attackObject.EnabledCollider(0.5f, 0.8f, false);
            base.DoUpdate(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);

            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            if (animInfo.normalizedTime > 0.4f&& animInfo.normalizedTime < 0.6f)
            {
                movement.ForwardLerpMove(thisTransform.position, movePower);
            }
            else
            {
                movement.DecreaseMove(0.2f);
            }
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.AttacksName, -1);
            movement.Move(0);
            attackObject.NotEnabledCollider();
        }
    }
}
