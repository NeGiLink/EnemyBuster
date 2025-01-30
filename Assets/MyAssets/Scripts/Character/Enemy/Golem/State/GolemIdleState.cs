using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class GolemIdleState : GolemStateBase
    {
        private IMovement movement;

        private Timer idleTimer = new Timer();

        private IDamageContainer damageContainer;

        public static readonly string StateKey = "Idle";
        public override string Key => StateKey;

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private float idleCount;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IGolemSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(GolemActionDecisionState.StateKey)) { re.Add(new IsTargetInViewTransition(actor, StateChanger, GolemActionDecisionState.StateKey)); }
            if (StateChanger.IsContain(GolemDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(actor, StateChanger, GolemDamageState.StateKey)); }
            if (StateChanger.IsContain(GolemDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, GolemDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(IGolemSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
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
        }
    }
}
