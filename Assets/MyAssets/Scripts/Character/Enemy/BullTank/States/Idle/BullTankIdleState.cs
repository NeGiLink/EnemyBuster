using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class BullTankIdleState : BullTankStateBase
    {
        private IMovement movement;

        private Timer idleTimer = new Timer();

        public static readonly string StateKey = "Idle";
        public override string Key => StateKey;

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private float idleCount;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BullTankActionDecisionState.StateKey)) { re.Add(new IsTargetInViewTransition(actor, StateChanger, BullTankActionDecisionState.StateKey)); }
            if (StateChanger.IsContain(BullTankDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(actor, StateChanger, BullTankDamageState.StateKey)); }
            if (StateChanger.IsContain(BullTankDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, BullTankDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(IBullTankSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
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
        }
    }
}
