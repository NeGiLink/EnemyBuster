using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class GolemChaseState : GolemStateBase
    {
        private IMovement               movement;
        private IVelocityComponent      velocity;
        private FieldOfView             fieldOfView;

        private IGolemAnimator          animator;

        private IBaseStatus             stauts;


        [SerializeField]
        private float                   rotationSpeed = 8;
        [SerializeField]
        private float                   moveSpeedChangeRate = 8;

        [SerializeField]
        private float                   minChaseDistance = 2.5f;

        [SerializeField]
        private float                   gravityMultiply;

        public static readonly string   StateKey = "Chase";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IGolemSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(GolemIdleState.StateKey)) { re.Add(new IsNoTargetInViewTransition(actor, StateChanger, GolemIdleState.StateKey)); }
            if (StateChanger.IsContain(GolemActionDecisionState.StateKey)) { re.Add(new IsMinDistanceTransition(actor, minChaseDistance, StateChanger, GolemActionDecisionState.StateKey)); }
            if (StateChanger.IsContain(GolemDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(actor, StateChanger, GolemDamageState.StateKey)); }
            if (StateChanger.IsContain(GolemDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, GolemDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IGolemSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
            animator = actor.GolemAnimator;
            stauts = actor.BaseStatus;
        }

        public override void DoStart()
        {
            base.DoStart();

            animator.Animator.SetInteger(animator.MoveAnimationID, 1);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.MoveTo(fieldOfView.TargetLastPoint, stauts.BaseSpeed, moveSpeedChangeRate, rotationSpeed, time);
            //èdóÕÇâ¡éZ
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.MoveAnimationID, 0);
            movement.Stop();
        }
    }
}
