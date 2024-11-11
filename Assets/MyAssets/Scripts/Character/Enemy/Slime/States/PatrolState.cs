using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class PatrolController : MonoBehaviour
    {
        public List<PatrolState> patrolStates;
    }

    [System.Serializable]
    public class PatrolState : SlimeStateBase
    {
        private ICharacterMovement movement;
        private Transform thisTransform;

        private ISlimeAnimator animator;

        [SerializeField]
        private PatrplPointContainer patrplPointContainer;

        [SerializeField]
        float moveSpeed = 3;
        [SerializeField]
        float rotationSpeed = 8;
        [SerializeField]
        float moveSpeedChangeRate = 8;

        public static readonly string StateKey = "Patrol";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(SlimeIdleState.StateKey)) { re.Add(new IsNotPatrolTransition(actor, StateChanger, SlimeIdleState.StateKey)); }
            if (StateChanger.IsContain(ChaseState.StateKey)) { re.Add(new IsTargetInViewTransition(actor, StateChanger, ChaseState.StateKey)); }
            return re;
        }

        public override void DoSetup(ISlimeSetup actor)
        {
            base.DoSetup(actor);
            thisTransform = actor.gameObject.transform;
            movement = actor.Movement;
            animator = actor.SlimeAnimator;
            patrplPointContainer = actor.gameObject.GetComponent<PatrplPointContainer>();
            patrplPointContainer.SetCurrentPoint(GetMinDistancePointIndex());
        }
        public override void DoStart()
        {
            patrplPointContainer.SetStop(false);
            

            animator.Animator.SetInteger("Move", 1);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.MoveTo(patrplPointContainer.PatrolPoints[patrplPointContainer.CurrentPoint], moveSpeed, moveSpeedChangeRate, rotationSpeed, time);

            Vector3 targetVec = patrplPointContainer.PatrolPoints[patrplPointContainer.CurrentPoint] - thisTransform.position;
            targetVec.y = 0.0f;
            float targetDistance = targetVec.magnitude;
            const float minPatrolDistance = 0.1f;
            if (targetDistance < minPatrolDistance + moveSpeed * time)
            {
                NextPoint();
            }
        }

        void NextPoint()
        {
            patrplPointContainer.SetCurrentPoint(patrplPointContainer.CurrentPoint + 1);
            if (patrplPointContainer.CurrentPoint >= patrplPointContainer.PatrolPoints.Length)
            {
                patrplPointContainer.SetCurrentPoint(0);
                patrplPointContainer.SetStop(true);
            }
        }

        int GetMinDistancePointIndex()
        {
            int re = patrplPointContainer.CurrentPoint;
            float minDistance = (patrplPointContainer.PatrolPoints[patrplPointContainer.CurrentPoint] - thisTransform.position).magnitude;
            for (int i = 0; i < patrplPointContainer.PatrolPoints.Length; i++)
            {
                Vector3 targetVec = patrplPointContainer.PatrolPoints[i] - thisTransform.position;
                float targetDistance = targetVec.magnitude;
                if (targetDistance < minDistance)
                {
                    minDistance = targetDistance;
                    re = i;
                }
            }
            return re;
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger("Move", 0);
        }
    }

    public class IsTargetInViewTransition : CharacterStateTransitionBase
    {
        readonly FieldOfView fieldOfView;
        public IsTargetInViewTransition(ICharacterSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
        }
        public override bool IsTransition() => fieldOfView.TryGetFirstObject(out var obj);
    }

    public class IsNoTargetInViewTransition : CharacterStateTransitionBase
    {
        readonly FieldOfView fieldOfView;
        public IsNoTargetInViewTransition(ICharacterSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
        }
        public override bool IsTransition() => !fieldOfView.TryGetFirstObject(out var obj);
    }
}
