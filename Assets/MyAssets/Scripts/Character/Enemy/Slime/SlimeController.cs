using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace MyAssets
{
    public class SlimeController : CharacterBaseController,ISlimeSetup
    {
        [SerializeField]
        private SlimeStatusProperty         property;
        public IBaseStauts                  BaseStauts => property;

        [SerializeField]
        private Movement                    movement;
        public IMovement                    Movement => movement;
        
        private StepClimberJudgment         stepClimberJudgment;
        public IStepClimberJudgment         StepClimberJudgment => stepClimberJudgment;
        
        [SerializeField]
        private SlimeRotation               rotation;
        public IRotation                    Rotation => rotation;

        [SerializeField]
        private SlimeAnimator               animator;
        public ISlimeAnimator               SlimeAnimator => animator;

        private FieldOfView                 fieldOfView;
        public IFieldOfView FieldOfView => fieldOfView;

        [SerializeField]
        private SlimeBodyAttackController   attackObject;
        public SlimeBodyAttackController    AttackObject => attackObject;

        [SerializeField]
        private StateMachine<string>        stateMachine;
        public IStateMachine                StateMachine => stateMachine;

        [SerializeField]
        private string                      defaultStateKey;

        [SerializeField]
        private SlimeIdleState              idleState;


        [SerializeField]
        private PatrolState                 patrolState;

        [SerializeField]
        private ChaseState                  chaseState;

        [SerializeField]
        private ReadySlimeAttackState       readyAttackState;

        [SerializeField]
        private SlimeAttackState            attackState;

        [SerializeField]
        private SlimeDamageState            damageState;

        [SerializeField]
        private SlimeDeathState             deathState;

        ISlimeState<string>[]               states;
        protected override void Awake()
        {
            fieldOfView = GetComponent<FieldOfView>();
            attackObject = GetComponentInChildren<SlimeBodyAttackController>();

            animator.DoSetup(this);
            velocity.DoSetup(this);
            movement.DoSetup(this);
            rotation.DoSetup(this);
            damageContainer.DoSetup(this);
            damagement.DoSetup(this);

            states = new ISlimeState<string>[]
            {
                idleState,
                patrolState,
                chaseState,
                readyAttackState,
                attackState,
                damageState,
                deathState
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
            groundCheck.DoUpdate();
            stateMachine.DoUpdate(Time.deltaTime);
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
            stateMachine.DoTriggerEnter(gameObject,other);
        }

        protected override void OnTriggerStay(Collider other)
        {
            stateMachine.DoTriggerStay(gameObject,other);
        }

        protected override void OnTriggerExit(Collider other)
        {
            stateMachine.DoTriggerExit(gameObject,other);
        }

        public void RunDestroy()
        {
            Destroy(gameObject);
        }
    }
}
