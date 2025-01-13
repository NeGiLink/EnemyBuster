using MyAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class MushroomIdleState : MushroomStateBase
    {
        private IMovement movement;

        private IVelocityComponent velocity;

        private Timer idleTimer = new Timer();

        private IDamageContainer damageContainer;

        public static readonly string StateKey = "Idle";
        public override string Key => StateKey;

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private float idleCount;

        [SerializeField]
        private float gravityMultiply;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IMushroomSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(MushroomPatrolState.StateKey)) { re.Add(new IsPatrolTransition(actor, idleTimer, StateChanger, MushroomPatrolState.StateKey)); }
            if (StateChanger.IsContain(MushroomChaseState.StateKey)) { re.Add(new IsTargetInViewTransition(actor, StateChanger, MushroomChaseState.StateKey)); }
            if (StateChanger.IsContain(MushroomDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(actor, StateChanger, MushroomDamageState.StateKey)); }
            if (StateChanger.IsContain(MushroomDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, MushroomDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(IMushroomSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
            damageContainer = actor.DamageContainer;
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
