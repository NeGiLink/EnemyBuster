using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyAssets
{
    public class PlayerController : MonoBehaviour,IPlayerSetup
    {
        private IControllerInput input;

        private PlayerInput keyInput;
        public IMoveInputProvider MoveInput => keyInput;
        public IAttackInputProvider TimerInput => keyInput;

        [SerializeField]
        private VelocityComponent velocity;

        public IVelocityComponent Velocity => velocity;

        [SerializeField]
        private FootIK footIK;
        public IFootIK FootIK => footIK;

        [SerializeField]
        private PlayerRotation rotation;
        public ICharacterRotation Rotation => rotation;

        [SerializeField]
        private GroundCheck groundCheck;
        public IGroundCheck GroundCheck => groundCheck;

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
        private IdleState idleState;

        [SerializeField]
        private MoveState moveState;

        [SerializeField]
        private JumpState jumpState;

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

        IPlayerState<string>[] states;

        private void Awake()
        {
            animator.DoSetup(this);
            footIK.DoSetup(this);
            input = GetComponent<IControllerInput>();
            keyInput = input as PlayerInput;
            velocity.DoSetup(this);
            movement.DoSetup(this);
            obstacleJudgment.DoSetup(this);
            stepClimberJudgment.DoSetup(this);
            climb.DoSetup(this);
            rotation.DoSetup(this);
            states = new IPlayerState<string>[]
            {
                idleState,
                moveState,
                jumpState,
                fallState,
                landingState,
                climbState,
                firstAttackState,
                secondAttackState,
                thirdAttackState
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

        void Start()
        {
        }

        void Update()
        {
            input.DoUpdate();
            groundCheck.CheckGroundStatus();
            stateMachine.DoUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
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
    }
}

