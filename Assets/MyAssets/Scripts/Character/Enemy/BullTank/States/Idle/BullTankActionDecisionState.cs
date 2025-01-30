using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class BullTankActionDecisionState : BullTankStateBase
    {
        private IVelocityComponent velocity;
        private FieldOfView fieldOfView;

        [SerializeField]
        private float maxDistance = 5f;

        [SerializeField]
        private float gravityMultiply;


        [SerializeField]
        private float targetDistance;

        public static readonly string StateKey = "ActionDecision";
        public override string Key => StateKey;

        private Trigger chaseTrigger = new Trigger();

        private Trigger attackTrigger = new Trigger();

        private Trigger rushTrigger = new Trigger();

        private Trigger sideMoveTrigger = new Trigger();

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BullTankIdleState.StateKey)) { re.Add(new IsNoTargetInViewTransition(actor, StateChanger, BullTankIdleState.StateKey)); }
            if (StateChanger.IsContain(BullTankChaseState.StateKey)) { re.Add(new IsTriggerTransition(actor, chaseTrigger, StateChanger, BullTankChaseState.StateKey)); }
            if (StateChanger.IsContain(BullTankSideMoveState.StateKey)) { re.Add(new IsTriggerTransition(actor, sideMoveTrigger, StateChanger, BullTankSideMoveState.StateKey)); }
            if (StateChanger.IsContain(BullTankNormalAttackState.StateKey)) { re.Add(new IsTriggerTransition(actor, attackTrigger, StateChanger, BullTankNormalAttackState.StateKey)); }
            if (StateChanger.IsContain(ReadyRushAttackStartState.StateKey)) { re.Add(new IsTriggerTransition(actor, rushTrigger, StateChanger, ReadyRushAttackStartState.StateKey)); }
            if (StateChanger.IsContain(BullTankDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(actor, StateChanger, BullTankDamageState.StateKey)); }
            if (StateChanger.IsContain(BullTankDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, BullTankDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(IBullTankSetup actor)
        {
            base.DoSetup(actor);
            velocity = actor.Velocity;
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
        }

        public override void DoStart()
        {
            base.DoStart();
            targetDistance = fieldOfView.GetSubDistance.magnitude;

            if (targetDistance <= 2.5f)
            {
                int r = Random.Range(0, 2);
                switch (r)
                {
                    case 0:
                        sideMoveTrigger.SetTrigger(true);
                        break;
                    case 1:
                        attackTrigger.SetTrigger(true);
                        break;
                }
            }
            else
            {
                float r = Random.Range(0, 1.0f);
                if (r <= 0.05f)
                {
                    rushTrigger.SetTrigger(true);
                }
                else
                {
                    chaseTrigger.SetTrigger(true);
                }
            }
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            //d—Í‚ð‰ÁŽZ
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();

            sideMoveTrigger.SetTrigger(false);
            attackTrigger.SetTrigger(false);
            rushTrigger.SetTrigger(false);
            chaseTrigger.SetTrigger(false);
        }
    }

}
