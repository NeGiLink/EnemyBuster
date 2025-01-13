using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class MushroomDeathState : MushroomStateBase
    {
        private Transform thisTransform;

        private IMushroomAnimator animator;

        private IMovement movement;

        private IVelocityComponent velocity;

        private Timer destroyTimer = new Timer();

        [SerializeField]
        private float destroyCount;

        public static readonly string StateKey = "Death";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IMushroomSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();

            return re;
        }
        public override void DoSetup(IMushroomSetup actor)
        {
            base.DoSetup(actor);
            thisTransform = actor.gameObject.transform;
            animator = actor.MushroomAnimator;
            movement = actor.Movement;
            velocity = actor.Velocity;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetBool("Death", true);
            velocity.DeathCollider();
            destroyTimer.Start(destroyCount);
            destroyTimer.OnEnd += DestroyUpdate;
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
            movement.Move(0);
            velocity.Rigidbody.velocity = Vector3.zero;
        }
    }
}
