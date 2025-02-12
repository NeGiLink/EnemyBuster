using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class ChaseState : SlimeStateBase
    {
        private IMovement               movement;

        private IVelocityComponent      velocity;

        private Transform               thisTransform;
        
        private FieldOfView             fieldOfView;

        private ISlimeAnimator          animator;

        private SEHandler               seHandler;

        [SerializeField]
        private float                   moveSpeed = 4;
        [SerializeField]
        private float                   rotationSpeed = 8;
        [SerializeField]
        private float                   moveSpeedChangeRate = 8;
        [SerializeField]
        private float                   gravityMultiply;

        [SerializeField]
        private float                   minChaseDistance = 2.5f;

        public static readonly string   StateKey = "Chase";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup enemy)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(SlimeIdleState.StateKey)) { re.Add(new IsNoTargetInViewTransition(enemy, StateChanger, SlimeIdleState.StateKey)); }
            if (StateChanger.IsContain(SlimeDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(enemy, StateChanger, SlimeDamageState.StateKey)); }
            if (StateChanger.IsContain(ReadySlimeAttackState.StateKey)) { re.Add(new IsReadyAttackTransition(enemy, StateChanger, ReadySlimeAttackState.StateKey)); }
            if (StateChanger.IsContain(SlimeDeathState.StateKey)) { re.Add(new IsDeathTransition(enemy, StateChanger, SlimeDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(ISlimeSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
            thisTransform = actor.gameObject.transform;
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
            animator = actor.SlimeAnimator;
            seHandler = actor.SEHandler;
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            seHandler.PlayFootstepSound();
        }
        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);

            Vector3 targetVec = fieldOfView.TargetLastPoint - thisTransform.position;
            targetVec.y = 0.0f;
            float targetDistance = targetVec.magnitude;
            if (targetDistance < minChaseDistance)
            {
                animator.Animator.SetInteger(animator.MoveAnimationID, Define.Zero);
                animator.Animator.SetInteger(animator.AttackAnimationID, Define.Zero);
                movement.Stop();
            }
            else
            {
                animator.Animator.SetInteger(animator.MoveAnimationID, 1);
                movement.MoveTo(fieldOfView.TargetLastPoint, moveSpeed, moveSpeedChangeRate, rotationSpeed, time);
            }

            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        private bool TargetOnTheFrontCheck(Transform target)
        {
            if(target == null) { return true; }
            Vector3 ev = thisTransform.position - target.position;
            ev.Normalize();
            Vector3 worldFrontDirection = target.transform.TransformDirection(Vector3.forward).normalized;

            // “àÏ‚ðŒvŽZ
            float dotFront = Vector3.Dot(worldFrontDirection, ev);
            return dotFront > 0.25;
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.MoveAnimationID, Define.Zero);
            movement.Stop();
        }

    }
}

