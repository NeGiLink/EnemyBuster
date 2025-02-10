using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * マッシュルームの巡回状態
     */
    [System.Serializable]
    public class MushroomPatrolState : MushroomStateBase
    {
        private IMovement               movement;

        private IVelocityComponent      velocity;

        private Transform               thisTransform;

        private IMushroomAnimator       animator;


        [SerializeField]
        private PatrplPointContainer    patrplPointContainer;

        [SerializeField]
        private　float                   moveSpeed = 3;
        [SerializeField]
        private　float                   rotationSpeed = 8;
        [SerializeField]
        private float                   moveSpeedChangeRate = 8;
        [SerializeField]
        private float                   gravityMultiply;

        public static readonly string   StateKey = "Patrol";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IMushroomSetup enemy)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(MushroomIdleState.StateKey)) { re.Add(new IsNotPatrolTransition(enemy, StateChanger, MushroomIdleState.StateKey)); }
            if (StateChanger.IsContain(MushroomChaseState.StateKey)) { re.Add(new IsTargetInViewTransition(enemy, StateChanger, MushroomChaseState.StateKey)); }
            if (StateChanger.IsContain(MushroomDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(enemy, StateChanger, MushroomDamageState.StateKey)); }
            if (StateChanger.IsContain(MushroomDeathState.StateKey)) { re.Add(new IsDeathTransition(enemy, StateChanger, MushroomDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IMushroomSetup actor)
        {
            base.DoSetup(actor);
            thisTransform = actor.gameObject.transform;
            movement = actor.Movement;
            velocity = actor.Velocity;
            animator = actor.MushroomAnimator;
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
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
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
