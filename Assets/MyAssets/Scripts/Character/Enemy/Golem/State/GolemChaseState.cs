using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class GolemChaseState : GolemStateBase
    {
        private IMovement movement;
        private IVelocityComponent velocity;
        private Transform thisTransform;
        private FieldOfView fieldOfView;

        private IGolemAnimator animator;

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private float highMoveSpeed;

        [SerializeField]
        private float rotationSpeed = 8;
        [SerializeField]
        private float moveSpeedChangeRate = 8;

        [SerializeField]
        private float minChaseDistance = 2.5f;

        [SerializeField]
        private float maxDistance = 5f;

        [SerializeField]
        private float gravityMultiply;

        [SerializeField]
        [Range(0f, 1f)]
        private float moveInput = 0f;

        [SerializeField]
        private float targetDistance;

        public static readonly string StateKey = "Chase";
        public override string Key => StateKey;

        public static readonly int MoveAnimationID = Animator.StringToHash("Move");

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
            thisTransform = actor.gameObject.transform;
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
            animator = actor.GolemAnimator;
        }

        public override void DoStart()
        {
            base.DoStart();
            targetDistance = fieldOfView.GetSubDistance.magnitude;

            animator.Animator.SetInteger(MoveAnimationID, 1);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.MoveTo(fieldOfView.TargetLastPoint, highMoveSpeed, moveSpeedChangeRate, rotationSpeed, time);
            //èdóÕÇâ¡éZ
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            moveInput = 0f;
            animator.Animator.SetInteger(MoveAnimationID, 0);
            movement.Stop();
        }
    }
}
