using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class GolemActionDecisionState : GolemStateBase
    {
        private IVelocityComponent velocity;
        private FieldOfView fieldOfView;

        [SerializeField]
        private float gravityMultiply;


        [SerializeField]
        private float targetDistance;

        public static readonly string StateKey = "ActionDecision";
        public override string Key => StateKey;

        private Trigger chaseTrigger = new Trigger();

        private Trigger attackTrigger = new Trigger();

        private Trigger stampTrigger = new Trigger();

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IGolemSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(GolemIdleState.StateKey)) { re.Add(new IsNoTargetInViewTransition(actor, StateChanger, GolemIdleState.StateKey)); }
            if (StateChanger.IsContain(GolemChaseState.StateKey)) { re.Add(new IsTriggerTransition(actor, chaseTrigger, StateChanger, GolemChaseState.StateKey)); }
            if (StateChanger.IsContain(GolemAttackState.StateKey)) { re.Add(new IsTriggerTransition(actor, attackTrigger, StateChanger, GolemAttackState.StateKey)); }
            if (StateChanger.IsContain(GolemStampAttackState.StateKey)) { re.Add(new IsTriggerTransition(actor, stampTrigger, StateChanger, GolemStampAttackState.StateKey)); }
            if (StateChanger.IsContain(GolemDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(actor, StateChanger, GolemDamageState.StateKey)); }
            if (StateChanger.IsContain(GolemDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, GolemDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(IGolemSetup actor)
        {
            base.DoSetup(actor);
            velocity = actor.Velocity;
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
        }

        public override void DoStart()
        {
            base.DoStart();
            targetDistance = fieldOfView.GetSubDistance.magnitude;

            if (targetDistance <= 2.5f&&fieldOfView.TargetObject != null)
            {
                int r = Random.Range(0, 2);
                switch (r)
                {
                    case 0:
                        stampTrigger.SetTrigger(true);
                        break;
                    case 1:
                        attackTrigger.SetTrigger(true);
                        break;
                }
            }
            else
            {
                chaseTrigger.SetTrigger(true);
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


            attackTrigger.SetTrigger(false);

            chaseTrigger.SetTrigger(false);
        }
    }
}
