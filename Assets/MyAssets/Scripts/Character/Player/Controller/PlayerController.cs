using UnityEngine;


namespace MyAssets
{
    /*
     * プレイヤーの制御を行うクラス
     */
    public class PlayerController : CharacterBaseController,IPlayerSetup
    {
        [SerializeField]
        private PlayerStatusProperty    property;

        public IPlayerStauts            Stauts => property;
        public IBaseStauts              BaseStauts => property;

        private FieldOfView             fieldOfView;
        public IFieldOfView             FieldOfView => fieldOfView;

        private PlayerUIHandler         uIHandler;
        public PlayerUIHandler          PlayerUIHandler => uIHandler;

        private IControllerInput        input;

        private PlayerActionInput       keyInput;
        public IMoveInputProvider       MoveInput => keyInput;
        public IAttackInputProvider     AttackInput => keyInput;
        public IToolInputProvider       ToolInput => keyInput;
        [SerializeField]
        private WeaponController        weaponController;
        public IEquipment               Equipment => weaponController;

        private PlayerEffectController  effectController;

        public PlayerEffectController   EffectController => effectController;

        [SerializeField]
        private ChangingState           battleFlagger;
        public  IBattleFlagger          BattleFlagger => battleFlagger;

        [SerializeField]
        private GuardTrigger            guardTrigger;
        public IGuardTrigger            GuardTrigger => guardTrigger;

        [SerializeField]
        private IKController            ikController;
        public  IAllIK                  IkController => ikController;


        [SerializeField]
        private PlayerRotation          rotation;
        public IRotation                Rotation => rotation;

        [SerializeField]
        private ObstacleJudgment        obstacleJudgment;
        public IObstacleJudgment        ObstacleJudgment => obstacleJudgment;
        [SerializeField]
        private StepClimberJudgment     stepClimberJudgment;
        public IStepClimberJudgment     StepClimberJudgment => stepClimberJudgment;
        [SerializeField]
        private PlayerAnimator          animator;
        public IPlayerAnimator          PlayerAnimator => animator;

        private SEHandler               seHandler;
        public SEHandler                SEHandler => seHandler;

        [SerializeField]
        private StateMachine<string>    stateMachine;
        public IStateMachine            StateMachine => stateMachine;

        [SerializeField]
        private Movement                movement;
        public IMovement                Movement => movement;

        [SerializeField]
        private Climb                   climb;
        public IClimb                   Climb => climb;

        [SerializeField]
        private string                  defaultStateKey;

        [Header("下記はキャラクターの状態クラス")]
        [SerializeField]
        private PlayerIdleState         idleState;

        [SerializeField]
        private MoveState               moveState;

        [SerializeField]
        private BattleIdleState         battleIdleState;

        [SerializeField]
        private BattleMoveState         battleMoveState;

        [SerializeField]
        private JumpState               jumpState;

        [SerializeField]
        private RollingState            rollingState;

        [SerializeField]
        private FallState               fallState;

        [SerializeField]
        private LandingState            landingState;

        [SerializeField]
        private ClimbState              climbState;

        [SerializeField]
        private FirstAttackState        firstAttackState;
        [SerializeField]
        private SecondAttackState       secondAttackVer1State;
        [SerializeField]
        private SecondDerivationAttack2State secondAttackVer2State;
        [SerializeField]
        private ThirdAttackState        thirdAttackState;
        [SerializeField]
        private ReadyJumpAttack         readyJumpAttack;
        [SerializeField]
        private JumpAttackState         jumpAttackState;
        [SerializeField]
        private JumpAttackLandingState  jumpAttackLandingState;
        [SerializeField]
        private CounterAttackState      counterAttackState;
        [SerializeField]
        private PlayerChargeAttackStartState chargeAttackStartState;
        [SerializeField]
        private PlayerChargingAttackState chargingAttackState;
        [SerializeField]
        private PlayerChargeAttackState chargeAttackState;
        [SerializeField]
        private PlayerChargeAttackEndState chargeAttackEndState;
        [SerializeField]
        private GuardState              guardState;

        [SerializeField]
        private WeaponOutState          weaponOutState;
        [SerializeField]
        private WeaponInState           weaponInState;

        [SerializeField]
        private PlayerDamageState       damageState;

        [SerializeField]
        private PlayerDeathState        deathState;

        [SerializeField]
        private GetUpState              getUpState;

        IPlayerState<string>[]          states;

        public override CharacterType CharaType => CharacterType.Player;
        protected override void Awake()
        {
            base.Awake();
            fieldOfView = GetComponent<FieldOfView>();
            uIHandler = GetComponent<PlayerUIHandler>();
            effectController = GetComponent<PlayerEffectController>();
            seHandler = GetComponent<SEHandler>();
            input = GetComponent<IControllerInput>();
            keyInput = input as PlayerActionInput;
            weaponController = GetComponent<WeaponController>();


            property.DoSetup(this);
            animator.DoSetup(this);
            ikController.DoSetup(this);
            battleFlagger.DoSetup(this);
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
                secondAttackVer1State,
                secondAttackVer2State,
                thirdAttackState,
                readyJumpAttack,
                jumpAttackState,
                jumpAttackLandingState,
                counterAttackState,
                chargeAttackStartState,
                chargingAttackState,
                chargeAttackState,
                chargeAttackEndState,
                guardState,
                weaponOutState,
                weaponInState,
                damageState,
                deathState,
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

        protected override void Start()
        {
            property.Initilaize();
        }

        protected override void Update()
        {
            if (SystemManager.IsPause) { return; }
            float t = Time.deltaTime;
            if(fieldOfView.TargetObject != null&&keyInput.Foucus > 0)
            {
                uIHandler.LockOnUI.gameObject.SetActive(true);
                uIHandler.LockOnUI.LockUpdate();
            }
            else
            {
                uIHandler.LockOnUI.gameObject.SetActive(false);
            }
            property.DoUpdate(t);
            input.DoUpdate();
            groundCheck.DoUpdate();
            stateMachine.DoUpdate(t);
        }

        protected override void FixedUpdate()
        {
            stateMachine.DoFixedUpdate(Time.deltaTime);

            TargetUpdate();
        }

        protected virtual void LateUpdate()
        {
            stateMachine.DoLateUpdate(Time.deltaTime);
        }


        private void TargetUpdate()
        {
            fieldOfView.DoUpdate();
        }

        public void ThisDestroy()
        {
            Destroy(this);
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
            stateMachine?.DoTriggerExit(gameObject,other);
        }
    }
}

