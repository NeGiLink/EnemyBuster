using UnityEngine;

namespace MyAssets
{
    /*
     * NPCの制御クラス
     */
    public class NPCController : CharacterBaseController,INPCSetup
    {
        [SerializeField]
        private NPCStatusProperty       property;
        public IBaseStauts              BaseStauts => property;

        [SerializeField]
        private NPCCommandPanel         commandPanel;
        public INPCCommandPanel         CommandPanel => commandPanel;

        public IStepClimberJudgment     StepClimberJudgment => null;
        public IRotation                Rotation => null;

        [SerializeField]
        private Movement                movement;
        public IMovement                Movement => movement;

        [SerializeField]
        private NPCAnimator             animator;
        public INPCAnimator             Animator => animator;
        
        public IFieldOfView             FieldOfView => null;

        private SEHandler               seHandler;
        public SEHandler                SEHandler => seHandler;

        [SerializeField]
        private GuardTrigger            guardTrigger;
        public IGuardTrigger            GuardTrigger => guardTrigger;

        [SerializeField]
        private StateMachine<string>    stateMachine;
        public IStateMachine            StateMachine => stateMachine;

        [SerializeField]
        private string                  defaultStateKey;

        [SerializeField]
        private NPCIdleState            idleState;

        [SerializeField]
        private NPCMoveState            moveState;

        private INPCState<string>[]     states;
        protected override void Awake()
        {

            animator.DoSetup(this);
            velocity.DoSetup(this);
            movement.DoSetup(this);

            states = new INPCState<string>[]
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

            groundCheck.SetTransform(transform);

            if (animator == null)
            {
                Debug.LogError("Animatorがアタッチされていません");
            }
        }
        protected override void Update()
        {
            if (SystemManager.IsPause) { return; }
            groundCheck.DoUpdate();
            stateMachine.DoUpdate(Time.deltaTime);
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
    }

}
