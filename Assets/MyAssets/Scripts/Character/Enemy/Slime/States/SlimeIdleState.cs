using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class SlimeIdleState : SlimeStateBase
    {
        private ICharacterMovement movement;

        private Timer idleTimer = new Timer();

        public static readonly string StateKey = "Idle";
        public override string Key => StateKey;

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private float idleCount;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(PatrolState.StateKey)) { re.Add(new IsPatrolTransition(actor,idleTimer, StateChanger, PatrolState.StateKey)); }
            return re;
        }
        public override void DoSetup(ISlimeSetup slime)
        {
            base.DoSetup(slime);
            movement = slime.Movement;
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
