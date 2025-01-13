using MyAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class ChaseState : SlimeStateBase
    {
        private IMovement movement;

        private IVelocityComponent velocity;
        private Transform thisTransform;
        private FieldOfView fieldOfView;

        private IDamageContainer damageContainer;

        private ISlimeAnimator animator;

        private ISlimeRotation rotation;

        [SerializeField]
        float moveSpeed = 4;
        [SerializeField]
        float rotationSpeed = 8;
        [SerializeField]
        float moveSpeedChangeRate = 8;
        [SerializeField]
        private float gravityMultiply;

        [SerializeField]
        float searchingTime = 1.0f;

        [SerializeField]
        float minChaseDistance = 2.5f;

        public static readonly string StateKey = "Chase";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup enemy)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(MushroomIdleState.StateKey)) { re.Add(new IsNoTargetInViewTransition(enemy, StateChanger, MushroomIdleState.StateKey)); }
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
            damageContainer = actor.DamageContainer;
            rotation = actor.SlimeRotation;
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);

            Vector3 targetVec = fieldOfView.TargetLastPoint - thisTransform.position;
            targetVec.y = 0.0f;
            float targetDistance = targetVec.magnitude;
            if (targetDistance < minChaseDistance)
            {
                animator.Animator.SetInteger(animator.MoveName, Define.Zero);
                if(TargetOnTheFrontCheck(fieldOfView.TargetObject.transform))
                {
                    animator.Animator.SetInteger(animator.AttacksName, Define.Zero);

                }
                else
                {
                    rotation.DoLookOnTarget(fieldOfView.TargetObject.transform);
                }
                movement.Move(0f);
            }
            else
            {
                animator.Animator.SetInteger(animator.MoveName, 1);
                movement.MoveTo(fieldOfView.TargetLastPoint, moveSpeed, moveSpeedChangeRate, rotationSpeed, time);
            }

            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        private bool TargetOnTheFrontCheck(Transform target)
        {
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
            animator.Animator.SetInteger(animator.MoveName, Define.Zero);
            movement.Move(0f);
        }

    }
}

