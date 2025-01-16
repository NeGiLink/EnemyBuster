using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

namespace MyAssets
{
    [System.Serializable]
    public class ReadyRushAttackStartState : BullTankStateBase
    {
        private IMovement movement;
        private IVelocityComponent velocity;
        private Transform thisTransform;
        private FieldOfView fieldOfView;

        private IDamageContainer damageContainer;

        private IBullTankAnimator animator;

        [SerializeField]
        private float gravityMultiply;

        public static readonly string StateKey = "ReadyRushStart";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(ReadyRushAttackLoopState.StateKey)) { re.Add(new IsNotBullTankAttackTransition(actor, "Attack02_Charge_Start", StateChanger, ReadyRushAttackLoopState.StateKey)); }
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
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger("ReadyAttack", 0);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.MoveTo(fieldOfView.TargetLastPoint, 0, 0, 8, time);
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger("ReadyAttack", -1);
            movement.Stop();
        }
    }
}
