using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class BullTankChaseState : BullTankStateBase
    {
        private IMovement               movement;

        private IVelocityComponent      velocity;
        
        private FieldOfView             fieldOfView;

        private IBullTankAnimator       animator;

        private IBaseStauts             stauts;

        [SerializeField]
        private float                   highMoveSpeed;

        [SerializeField]
        private float                   rotationSpeed = 8;
        [SerializeField]
        private float                   moveSpeedChangeRate = 8;

        [SerializeField]
        private float                   minChaseDistance = 2.5f;

        [SerializeField]
        private float                   maxDistance = 5f;

        [SerializeField]
        private float                   gravityMultiply;

        [SerializeField]
        [Range(0f, 1f)]
        private float                   moveInput = 0f;

        [SerializeField]
        private float                   targetDistance;

        public static readonly string   StateKey = "Chase";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BullTankIdleState.StateKey)) { re.Add(new IsNoTargetInViewTransition(actor, StateChanger, BullTankIdleState.StateKey)); }
            if (StateChanger.IsContain(BullTankActionDecisionState.StateKey)) { re.Add(new IsMinDistanceTransition(actor, minChaseDistance, StateChanger, BullTankActionDecisionState.StateKey)); }
            if (StateChanger.IsContain(BullTankDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(actor, StateChanger, BullTankDamageState.StateKey)); }
            if (StateChanger.IsContain(BullTankDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, BullTankDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IBullTankSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
            animator = actor.BullTankAnimator;
            stauts = actor.BaseStauts;
        }

        public override void DoStart()
        {
            base.DoStart();
            targetDistance = fieldOfView.GetSubDistance.magnitude;
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            moveInput = Move(time);
            animator.Animator.SetFloat(animator.MoveAnimationID, moveInput);
            //èdóÕÇâ¡éZ
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        private float Move(float time)
        {
            float input = moveInput;
            //ãóó£Ç…ÇÊÇ¡Çƒà⁄ìÆÇïœÇ¶ÇÈ
            Vector3 targetVec = fieldOfView.GetSubDistance;
            targetDistance = targetVec.magnitude;
            if (targetDistance < minChaseDistance)
            {
                input -= Time.deltaTime;
                animator.Animator.SetFloat(animator.MoveAnimationID, Define.Zero);
                movement.Stop();
            }
            else if (minChaseDistance < targetDistance && targetDistance < maxDistance)
            {
                input += Time.deltaTime;
                if (input >= 0.5f)
                {
                    input = 0.5f;
                }
                movement.MoveTo(fieldOfView.TargetLastPoint, stauts.BaseSpeed, moveSpeedChangeRate, rotationSpeed, time);
            }
            else if (targetDistance > maxDistance)
            {
                input += Time.deltaTime;
                movement.MoveTo(fieldOfView.TargetLastPoint, stauts.BaseSpeed + highMoveSpeed, moveSpeedChangeRate, rotationSpeed, time);
            }
            return input;
        }

        public override void DoExit()
        {
            base.DoExit();
            moveInput = 0f;
            animator.Animator.SetFloat(animator.MoveAnimationID, moveInput);
            movement.Stop();
        }
    }
}
