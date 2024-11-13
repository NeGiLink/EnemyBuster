using UnityEngine;


namespace MyAssets
{
    public class PlayerController : CharacterBaseController,IPlayerSetup
    {
        private FieldOfView fieldOfView;

        private IControllerInput input;

        private PlayerInput keyInput;
        public IMoveInputProvider MoveInput => keyInput;
        public IAttackInputProvider AttackInput => keyInput;
        public IToolInputProvider ToolInput => keyInput;
        [SerializeField]
        private WeaponController weaponController;
        public IEquipment Equipment => weaponController;

        [SerializeField]
        private ChangingState changingState;
        public IChangingState ChangingState => changingState;

        [SerializeField]
        private FootIK footIK;
        public IFootIK FootIK => footIK;


        [SerializeField]
        private PlayerRotation rotation;
        public IRotation Rotation => rotation;

        [SerializeField]
        private ObstacleJudgment obstacleJudgment;
        public IObstacleJudgment ObstacleJudgment => obstacleJudgment;
        [SerializeField]
        private StepClimberJudgment stepClimberJudgment;
        public IStepClimberJudgment StepClimberJudgment => stepClimberJudgment;
        [SerializeField]
        private PlayerAnimator animator;
        public IPlayerAnimator PlayerAnimator => animator;

        [SerializeField]
        private StateMachine<string> stateMachine;
        public IStateMachine StateMachine => stateMachine;

        [SerializeField]
        private Movement movement;
        public IMovement Movement => movement;

        [SerializeField]
        private Climb climb;
        public IClimb Climb => climb;

        [SerializeField]
        private string defaultStateKey;

        [SerializeField]
        private PlayerIdleState idleState;

        [SerializeField]
        private MoveState moveState;

        [SerializeField]
        private BattleIdleState battleIdleState;

        [SerializeField]
        private BattleMoveState battleMoveState;

        [SerializeField]
        private JumpState jumpState;

        [SerializeField]
        private RollingState rollingState;

        [SerializeField]
        private FallState fallState;

        [SerializeField]
        private LandingState landingState;

        [SerializeField]
        private ClimbState climbState;

        [SerializeField]
        private FirstAttackState firstAttackState;
        [SerializeField]
        private SecondAttackState secondAttackState;
        [SerializeField]
        private ThirdAttackState thirdAttackState;
        [SerializeField]
        private ReadyJumpAttack readyJumpAttack;
        [SerializeField]
        private JumpAttackState jumpAttackState;
        [SerializeField]
        private JumpAttackLandingState jumpAttackLandingState;

        [SerializeField]
        private WeaponOutState weaponOutState;
        [SerializeField]
        private WeaponInState weaponInState;

        [SerializeField]
        private PlayerDamageState damageState;

        [SerializeField]
        private GetUpState getUpState;

        IPlayerState<string>[] states;

        protected override void Awake()
        {
            fieldOfView = GetComponent<FieldOfView>();


            input = GetComponent<IControllerInput>();
            keyInput = input as PlayerInput;
            weaponController = GetComponent<WeaponController>();
            animator.DoSetup(this);
            footIK.DoSetup(this);
            changingState.DoSetup(this);
            velocity.DoSetup(this);
            movement.DoSetup(this);
            damageContainer.DoSetup(this);
            damagement.DoSetup(this);
            obstacleJudgment.DoSetup(this);
            stepClimberJudgment.DoSetup(this);
            climb.DoSetup(this);
            rotation.DoSetup(this);

            states = new IPlayerState<string>[]
            {
                idleState,
                moveState,
                battleIdleState,
                battleMoveState,
                jumpState,
                rollingState,
                fallState,
                landingState,
                climbState,
                firstAttackState,
                secondAttackState,
                thirdAttackState,
                readyJumpAttack,
                jumpAttackState,
                jumpAttackLandingState,
                weaponOutState,
                weaponInState,
                damageState,
                getUpState
            };
            stateMachine.DoSetup(states);
            foreach (var state in states)
            {
                state.DoSetup(this);
            }
            stateMachine.ChangeState(defaultStateKey);

            groundCheck.SetTransform(transform);

            if (input == null)
            {
                Debug.LogError("IControllerInputがアタッチされていません");
            }
            else
            {
                input.Setup();
            }
            if (animator == null)
            {
                Debug.LogError("Animatorがアタッチされていません");
            }
        }

        protected override void Update()
        {
            input.DoUpdate();
            groundCheck.DoUpdate();
            stateMachine.DoUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            stateMachine.DoFixedUpdate(Time.deltaTime);

            TargetUpdate();
        }

        private void TargetUpdate()
        {
            fieldOfView.DoUpdate();
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
            stateMachine.DoTriggerEnter(other);
        }

        protected override void OnTriggerStay(Collider other)
        {
            stateMachine.DoTriggerStay(other);
        }

        protected override void OnTriggerExit(Collider other)
        {
            stateMachine?.DoTriggerExit(other);
        }
    }
}

