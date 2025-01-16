using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class BullTankDeathState : BullTankStateBase
    {
        private Transform thisTransform;

        private IBullTankAnimator animator;

        private IMovement movement;

        private IVelocityComponent velocity;

        private Timer destroyTimer = new Timer();

        [SerializeField]
        private float destroyCount;

        public static readonly string StateKey = "Death";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();

            return re;
        }
        public override void DoSetup(IBullTankSetup actor)
        {
            base.DoSetup(actor);
            thisTransform = actor.gameObject.transform;
            animator = actor.BullTankAnimator;
            movement = actor.Movement;
            velocity = actor.Velocity;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetTrigger("Death");
            destroyTimer.Start(destroyCount);
            destroyTimer.OnEnd += DestroyUpdate;
            velocity.DeathCollider();
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            destroyTimer.Update(time);
        }

        private void DestroyUpdate()
        {
            thisTransform.gameObject.AddComponent<DestroyCommand>();
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Stop();
            velocity.Rigidbody.velocity = Vector3.zero;
        }
    }
}
