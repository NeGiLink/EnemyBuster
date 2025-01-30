using UnityEngine;

namespace MyAssets
{
    public class GolemController : CharacterBaseController, IGolemSetup
    {
        [SerializeField]
        private GolemStatusProperty property;
        public IBaseStauts BaseStauts => property;

        [SerializeField]
        private Movement movement;
        public IMovement Movement => movement;

        private StepClimberJudgment stepClimberJudgment;
        public IStepClimberJudgment StepClimberJudgment => stepClimberJudgment;


        public IRotation Rotation => null;

        [SerializeField]
        private GolemAnimator animator;
        public IEnemyAnimator EnemyAnimator => animator;
        public IGolemAnimator GolemAnimator => animator;

        private FieldOfView fieldOfView;
        public IFieldOfView FieldOfView => fieldOfView;
        [SerializeField]
        private GuardTrigger guardTrigger;
        public IGuardTrigger GuardTrigger => guardTrigger;

        private GolemEffectHandler effectHandler;

        public GolemEffectHandler EffectHandler => effectHandler;

        private SEHandler seHandler;
        public SEHandler SEHandler => seHandler;

        private GolemFistController fistController;
        public GolemFistController FistController => fistController;

        [SerializeField]
        private StateMachine<string> stateMachine;
        public IStateMachine StateMachine => stateMachine;

        [SerializeField]
        private string defaultStateKey;

        [SerializeField]
        private GolemIdleState idleState;

        [SerializeField]
        private GolemActionDecisionState actionDecisionState;

        [SerializeField]
        private GolemChaseState chaseState;

        [SerializeField]
        private GolemAttackState attackState;

        [SerializeField]
        private GolemStampAttackState stampAttackState;

        [SerializeField]
        private GolemDamageState damageState;

        [SerializeField]
        private GolemDeathState deathState;

        IGolemState<string>[] states;

        public override CharacterType CharaType => CharacterType.Enemy;

        protected override void Awake()
        {
            fieldOfView = GetComponent<FieldOfView>();

            fistController = GetComponentInChildren<GolemFistController>();

            effectHandler = GetComponent<GolemEffectHandler>();

            animator.DoSetup(this);
            velocity.DoSetup(this);
            movement.DoSetup(this);
            //rotation.DoSetup(this);
            damageContainer.DoSetup(this);
            damagement.DoSetup(this);

            property.Setup();

            states = new IGolemState<string>[]
            {
                idleState, 
                actionDecisionState, 
                chaseState, 
                attackState, 
                stampAttackState,
                damageState, 
                deathState,
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

        }
    }
}
