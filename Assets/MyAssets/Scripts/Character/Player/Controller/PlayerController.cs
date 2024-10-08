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

        [SerializeField]
        private VelocityComponent velocity;

        public IVelocityComponent Velocity => velocity;

        [SerializeField]
        private PlayerRotation rotation;

        [SerializeField]
        private PlayerAnimator animator;
        public IPlayerAnimator PlayerAnimator => animator;

        [SerializeField]
        private float maxSpeed;

        [SerializeField]
        private StateMachine<string> stateMachine;
        public IStateMachine StateMachine => stateMachine;

        [SerializeField]
        private Movement movement;

        public IMovement Movement => movement;

        [SerializeField]
        private string defaultStateKey;

        [SerializeField]
        private IdleState idleState;

        [SerializeField]
        private MoveState moveState;


        IPlayerState<string>[] states;

        private void Awake()
        {
            animator.DoSetup(this);
            input = GetComponent<IControllerInput>();
            keyInput = input as PlayerInput;
            velocity.DoSetup(this);
            movement.DoSetup(this);
            states = new IPlayerState<string>[]
            {
                idleState,
                moveState
            };
            stateMachine.DoSetup(states);
            foreach (var state in states)
            {
                state.DoSetup(this);
            }
            stateMachine.ChangeState(defaultStateKey);
            rotation.DoSetUp(transform,this);

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
            rotation.DoUpdate();
            stateMachine.DoUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            stateMachine.DoFixedUpdate(Time.deltaTime);
            rotation.DoFixedUpdate(velocity.CurrentVelocity);
        }

        private void OnDestroy()
        {
            stateMachine.Dispose();
        }
    }
}

