using UnityEngine;

namespace MyAssets
{
    public class BullTankController : CharacterBaseController, IBullTankSetup
    {
        [SerializeField]
        private BullTankStatusProperty property;
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
        [SerializeField]
        private GuardTrigger guardTrigger;
        public IGuardTrigger GuardTrigger => guardTrigger;

        private BullTankEffectHandler effectHandler;

        public BullTankEffectHandler EffectHandler => effectHandler;

        private SEHandler seHandler;
        public SEHandler SEHandler => seHandler;

        [SerializeField]
        private AxeController attackObject;
        public AxeController AttackObject => attackObject;

        [SerializeField]
        private BullTankHeadAttackController headAttackObject;
        public BullTankHeadAttackController HeadAttackObject => headAttackObject;

        [SerializeField]
        private StateMachine<string> stateMachine;
        public IStateMachine StateMachine => stateMachine;

        [SerializeField]
        private string defaultStateKey;

        [SerializeField]
        private BullTankIdleState idleState;

        [SerializeField]
        private BullTankActionDecisionState actionDecisionState;

        [SerializeField]
        private BullTankChaseState moveState;

        [SerializeField]
        private BullTankSideMoveState sideMoveState;

        [SerializeField]
        private BullTankNormalAttackState normalAttackState;

        [SerializeField]
        private ReadyRushAttackStartState readyRushAttackStartState;

        [SerializeField]
        private ReadyRushAttackLoopState readyRushAttackLoopState;

        [SerializeField]
        private RushAttackStartState rushAttackStartState;

        [SerializeField]
        private RushAttackLoopState rushAttackLoopState;

        [SerializeField]
        private RushAttackEndState rushAttackEndState;

        [SerializeField]
        private BullTankDamageState damageState;

        [SerializeField]
        private BullTankDeathState deathState;

        IBullTankState<string>[] states;

        public override CharacterType CharaType => CharacterType.Enemy;

        protected override void Awake()
        {
            fieldOfView = GetComponent<FieldOfView>();
            attackObject = GetComponentInChildren<AxeController>();
            headAttackObject = GetComponentInChildren<BullTankHeadAttackController>();

            effectHandler = GetComponent<BullTankEffectHandler>();

            animator.DoSetup(this);
            velocity.DoSetup(this);
            movement.DoSetup(this);
            //rotation.DoSetup(this);
            damageContainer.DoSetup(this);
            damagement.DoSetup(this);

            property.Setup();

            states = new IBullTankState<string>[]
            {
                idleState,
                actionDecisionState,
                moveState,
                sideMoveState,
                normalAttackState,
                damageState,
                deathState,
                readyRushAttackStartState,
                readyRushAttackLoopState,
                rushAttackStartState,
                rushAttackLoopState,
                rushAttackEndState
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
            if (SystemManager.IsPause) { return; }
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
            effectHandler.EffectLedger.SetPosAndRotCreate((int)BullTankEffectType.Death,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
