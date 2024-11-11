using UnityEngine;

namespace MyAssets
{
    public class SlimeController : MonoBehaviour,ISlimeSetup
    {
        [SerializeField]
        private VelocityComponent velocity;
        public IVelocityComponent Velocity => velocity;
        [SerializeField]
        private Movement movement;
        public ICharacterMovement Movement => movement;
        private StepClimberJudgment stepClimberJudgment;
        public IStepClimberJudgment StepClimberJudgment => stepClimberJudgment;
        [SerializeField]
        private SlimeRotation rotation;
        public IRotation Rotation => rotation;
        [SerializeField]
        private GroundCheck groundCheck;
        public IGroundCheck GroundCheck => groundCheck;
        [SerializeField]
        private SlimeAnimator animator;
        public ISlimeAnimator SlimeAnimator => animator;

        private FieldOfView fieldOfView;

        [SerializeField]
        private StateMachine<string> stateMachine;
        public IStateMachine StateMachine => stateMachine;

        [SerializeField]
        private string defaultStateKey;

        [SerializeField]
        private SlimeIdleState idleState;


        [SerializeField]
        private PatrolState patrolState;

        [SerializeField]
        private ChaseState chaseState;

        ISlimeState<string>[] states;
        private void Awake()
        {
            fieldOfView = GetComponent<FieldOfView>();

            animator.DoSetup(this);
            velocity.DoSetup(this);
            movement.DoSetup(this);
            rotation.DoSetup(this);

            states = new ISlimeState<string>[]
            {
                idleState,
                patrolState,
                chaseState
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

        void Update()
        {
            groundCheck.DoUpdate();
            stateMachine.DoUpdate(Time.deltaTime);
            fieldOfView.DoUpdate();
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
