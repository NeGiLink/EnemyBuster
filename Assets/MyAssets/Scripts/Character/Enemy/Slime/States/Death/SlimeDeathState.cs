using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class SlimeDeathState : SlimeStateBase
    {
        private Transform thisTransform;

        private ISlimeAnimator animator;

        private IMovement movement;

        private IVelocityComponent velocity;

        private SlimeEffectHandler effectHandler;

        private Timer destroyTimer = new Timer();

        [SerializeField]
        private float destroyCount;

        public static readonly string StateKey = "Death";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();

            return re;
        }
        public override void DoSetup(ISlimeSetup slime)
        {
            base.DoSetup(slime);
            thisTransform = slime.gameObject.transform;
            effectHandler = slime.EffectHandler;
            animator = slime.SlimeAnimator;
            movement = slime.Movement;
            velocity = slime.Velocity;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetBool("Death", true);
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
                (int)SlimeEffectType.Death,
                thisTransform.position,
                effectHandler.EffectLedger[(int)SlimeEffectType.Death].transform.rotation);
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
