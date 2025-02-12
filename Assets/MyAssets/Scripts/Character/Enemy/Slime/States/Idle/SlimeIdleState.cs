using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * ƒXƒ‰ƒCƒ€‚Ì‘Ò‹@ó‘Ô
     */
    [System.Serializable]
    public class SlimeIdleState : SlimeStateBase
    {
        private IMovement               movement;

        private IVelocityComponent      velocity;

        private Timer                   idleTimer = new Timer();

        public static readonly string   StateKey = "Idle";
        public override string          Key => StateKey;

        [SerializeField]
        private float                   moveSpeed;

        [SerializeField]
        private float                   idleCount;

        [SerializeField]
        private float                   gravityMultiply;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(SlimePatrolState.StateKey)) { re.Add(new IsPatrolTransition(actor,idleTimer, StateChanger, SlimePatrolState.StateKey)); }
            if (StateChanger.IsContain(ChaseState.StateKey)) { re.Add(new IsTargetInViewTransition(actor, StateChanger, ChaseState.StateKey)); }
            if (StateChanger.IsContain(SlimeDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(actor, StateChanger, SlimeDamageState.StateKey)); }
            if (StateChanger.IsContain(SlimeDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, SlimeDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(ISlimeSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
        }

        public override void DoStart()
        {
            base.DoStart();
            idleTimer.Start(idleCount);
            
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            idleTimer.Update(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(moveSpeed);
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }
    }
}
