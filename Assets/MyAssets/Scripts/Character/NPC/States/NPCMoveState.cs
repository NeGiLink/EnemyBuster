using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * NPC‚ÌˆÚ“®ó‘Ô
     */
    [System.Serializable]
    public class NPCMoveState : NPCStateBase
    {
        private IMovement               movement;
        private Transform               thisTransform;

        private INPCAnimator            animator;

        [SerializeField]
        private PatrplPointContainer    patrplPointContainer;

        [SerializeField]
        float                           moveSpeed = 1;
        [SerializeField]
        float                           rotationSpeed = 8;
        [SerializeField]
        float                           moveSpeedChangeRate = 8;

        public static readonly string   StateKey = "Patrol";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(INPCSetup enemy)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(NPCIdleState.StateKey)) { re.Add(new IsNPCNotMoveTransition(enemy, StateChanger, NPCIdleState.StateKey)); }
            return re;
        }

        public override void DoSetup(INPCSetup actor)
        {
            base.DoSetup(actor);
            animator = actor.Animator;
            thisTransform = actor.gameObject.transform;
            movement = actor.Movement;
            patrplPointContainer = actor.gameObject.GetComponent<PatrplPointContainer>();
            patrplPointContainer.SetCurrentPoint(GetMinDistancePointIndex());
        }
        public override void DoStart()
        {
            patrplPointContainer.SetStop(false);


            animator.Animator.SetInteger(animator.MoveAnimationID, 1);
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
            animator.Animator.SetInteger(animator.MoveAnimationID, 0);
        }
    }
}
