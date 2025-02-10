using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * ƒS[ƒŒƒ€‚ÌŽ€–Só‘Ô
     */
    [System.Serializable]
    public class GolemDeathState : GolemStateBase
    {
        private Transform               thisTransform;

        private IGolemAnimator          animator;

        private IMovement               movement;

        private IVelocityComponent      velocity;

        private GolemEffectHandler      effectHandler;

        private Timer                   destroyTimer = new Timer();

        [SerializeField]
        private float                   destroyCount;

        public static readonly string   StateKey = "Death";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IGolemSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();

            return re;
        }
        public override void DoSetup(IGolemSetup actor)
        {
            base.DoSetup(actor);
            thisTransform = actor.gameObject.transform;
            effectHandler = actor.EffectHandler;
            animator = actor.GolemAnimator;
            movement = actor.Movement;
            velocity = actor.Velocity;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetTrigger(animator.DeathAnimationID);
            destroyTimer.Start(destroyCount);
            destroyTimer.OnceEnd += DestroyUpdate;
            velocity.DeathCollider();
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            destroyTimer.Update(time);
        }

        private void DestroyUpdate()
        {
            effectHandler.EffectLedger.SetPosAndRotCreate(
                (int)GolemEffectType.Death,
                thisTransform.position,
                effectHandler.EffectLedger[(int)BullTankEffectType.Death].transform.rotation);
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
