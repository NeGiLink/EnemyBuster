using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class BullTankChaseState : BullTankStateBase
    {
        private IMovement movement;
        private IVelocityComponent velocity;
        private Transform thisTransform;
        private FieldOfView fieldOfView;

        private IDamageContainer damageContainer;

        private IBullTankAnimator animator;

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

        private Trigger attackTrigger = new Trigger();

        private Trigger rushTrigger = new Trigger();

        private Trigger sideMoveTrigger = new Trigger();

        public static readonly string StateKey = "Chase";
        public override string Key => StateKey;

        public static readonly int MoveAnimationID = Animator.StringToHash("Move");

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BullTankIdleState.StateKey)) { re.Add(new IsNoTargetInViewTransition(actor, StateChanger, BullTankIdleState.StateKey)); }
            if (StateChanger.IsContain(BullTankSideMoveState.StateKey)) { re.Add(new IsTriggerTransition(actor,sideMoveTrigger, StateChanger, BullTankSideMoveState.StateKey)); }
            if (StateChanger.IsContain(BullTankNormalAttackState.StateKey)) { re.Add(new IsTriggerTransition(actor,attackTrigger, StateChanger, BullTankNormalAttackState.StateKey)); }
            if (StateChanger.IsContain(ReadyRushAttackStartState.StateKey)) { re.Add(new IsTriggerTransition(actor,rushTrigger, StateChanger, ReadyRushAttackStartState.StateKey)); }
            if (StateChanger.IsContain(BullTankDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(actor, StateChanger, BullTankDamageState.StateKey)); }
            if (StateChanger.IsContain(BullTankDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, BullTankDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IBullTankSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
            thisTransform = actor.gameObject.transform;
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
            animator = actor.BullTankAnimator;
            damageContainer = actor.DamageContainer;
        }

        public override void DoStart()
        {
            base.DoStart();
            targetDistance = fieldOfView.GetSubDistance.magnitude;
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            if (targetDistance <= 2.5f)
            {
                int r = Random.Range(0, 3);
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
            else if(targetDistance >= maxDistance)
            {
                /*
                 */
                int r = Random.Range(0, 3);
                if(r == 2)
                {
                    rushTrigger.SetTrigger(true);
                }
            }
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            moveInput = Move(time);
            animator.Animator.SetFloat(MoveAnimationID, moveInput);
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
                animator.Animator.SetFloat(MoveAnimationID, Define.Zero);
                movement.Stop();
            }
            else if (minChaseDistance < targetDistance && targetDistance < maxDistance)
            {
                input += Time.deltaTime;
                if (input >= 0.5f)
                {
                    input = 0.5f;
                }
                movement.MoveTo(fieldOfView.TargetLastPoint, moveSpeed, moveSpeedChangeRate, rotationSpeed, time);
            }
            else if (targetDistance > maxDistance)
            {
                input += Time.deltaTime;
                movement.MoveTo(fieldOfView.TargetLastPoint, highMoveSpeed, moveSpeedChangeRate, rotationSpeed, time);
            }
            return input;
        }

        public override void DoExit()
        {
            base.DoExit();
            moveInput = 0f;
            animator.Animator.SetFloat(MoveAnimationID, moveInput);
            movement.Stop();

            sideMoveTrigger.SetTrigger(false);
            attackTrigger.SetTrigger(false);
            rushTrigger.SetTrigger(false);
        }
    }
}
