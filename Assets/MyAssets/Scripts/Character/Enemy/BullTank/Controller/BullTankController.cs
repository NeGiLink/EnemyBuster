using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class BullTankController : CharacterBaseController, IBullTankSetup
    {
        [SerializeField]
        private MushroomStatusProperty property;
        public IBaseStauts BaseStauts => property;

        [SerializeField]
        private Movement movement;
        public IMovement Movement => movement;

        private StepClimberJudgment stepClimberJudgment;
        public IStepClimberJudgment StepClimberJudgment => stepClimberJudgment;


        public IRotation Rotation => null;

        [SerializeField]
        private BullTankAnimator animator;
        public IEnemyAnimator EnemyAnimator => animator;
        public IBullTankAnimator BullTankAnimator => animator;

        private FieldOfView fieldOfView;
        public IFieldOfView FieldOfView => fieldOfView;
        /*
        [SerializeField]
        private MushroomAttackController attackObject;
        public MushroomAttackController AttackObject => attackObject;
         */

        [SerializeField]
        private StateMachine<string> stateMachine;
        public IStateMachine StateMachine => stateMachine;

        [SerializeField]
        private string defaultStateKey;

        [SerializeField]
        private BullTankIdleState idleState;


        [SerializeField]
        private MushroomPatrolState patrolState;

        [SerializeField]
        private MushroomChaseState chaseState;

        [SerializeField]
        private MushroomAttackState attackState;

        [SerializeField]
        private MushroomDamageState damageState;

        [SerializeField]
        private MushroomDeathState deathState;

        IBullTankState<string>[] states;

        public override CharacterType CharaType => CharacterType.Enemy;

        protected override void Awake()
        {
            fieldOfView = GetComponent<FieldOfView>();
            //attackObject = GetComponentInChildren<MushroomAttackController>();

            animator.DoSetup(this);
            velocity.DoSetup(this);
            movement.DoSetup(this);
            //rotation.DoSetup(this);
            damageContainer.DoSetup(this);
            damagement.DoSetup(this);

            states = new IBullTankState<string>[]
            {
                idleState
            };
            stateMachine.DoSetup(states);
            foreach (var state in states)
            {
                state.DoSetup(this);
            }
            stateMachine.ChangeState(defaultStateKey);

            groundCheck.SetTransform(transform);

            if (animator == null)
            {
                Debug.LogError("Animatorがアタッチされていません");
            }
        }

        protected override void Update()
        {
            float t = Time.deltaTime;
            property.DoUpdate(t);
            groundCheck.DoUpdate();
            stateMachine.DoUpdate(t);
            fieldOfView.DoUpdate();
        }

        protected override void FixedUpdate()
        {
            stateMachine.DoFixedUpdate(Time.deltaTime);
        }

        private void OnAnimatorIK()
        {
            stateMachine.DoAnimatorIKUpdate();
        }

        private void OnDestroy()
        {
            stateMachine.Dispose();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            stateMachine.DoTriggerEnter(gameObject, other);
        }

        protected override void OnTriggerStay(Collider other)
        {
            stateMachine.DoTriggerStay(gameObject, other);
        }

        protected override void OnTriggerExit(Collider other)
        {
            stateMachine.DoTriggerExit(gameObject, other);
        }

        public void RunDestroy()
        {
            Destroy(gameObject);
        }
    }
}
