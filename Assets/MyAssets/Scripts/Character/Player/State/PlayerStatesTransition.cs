using UnityEngine;

namespace MyAssets
{
    public class IsMoveTransition : CharacterStateTransitionBase
    {
        private readonly IMoveInputProvider input;
        public IsMoveTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IMoveInputProvider>();
        }
        public override bool IsTransition() => input.IsMove;
    }
    public class IsNotMoveTransition : CharacterStateTransitionBase
    {
        private readonly IMoveInputProvider input;
        public IsNotMoveTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IMoveInputProvider>();
        }
        public override bool IsTransition() => !input.IsMove;
    }

    public class IsBattleModeIdleTransition : CharacterStateTransitionBase
    {
        private readonly IFocusInputProvider         focusInput;
        private readonly IMoveInputProvider         input;
        public IsBattleModeIdleTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            focusInput = actor.gameObject.GetComponent<IFocusInputProvider>();
            input = actor.MoveInput;
        }
        public override bool IsTransition() => focusInput.Foucus == 1 && !input.IsMove;
    }
    public class IsBattleModeMoveTransition : CharacterStateTransitionBase
    {
        private readonly IFocusInputProvider         focusInput;
        private readonly IMoveInputProvider         input;
        public IsBattleModeMoveTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            focusInput = actor.gameObject.GetComponent<IFocusInputProvider>();
            input = actor.MoveInput;
        }
        public override bool IsTransition() => focusInput.Foucus == 1 && input.IsMove;
    }
    public class IsNotBattleModeMoveTransition : CharacterStateTransitionBase
    {
        private readonly IFocusInputProvider            focusInput;
        private readonly IMoveInputProvider             input;
        public IsNotBattleModeMoveTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            focusInput = actor.gameObject.GetComponent<IFocusInputProvider>();
            input = actor.MoveInput;
        }
        public override bool IsTransition() => focusInput.Foucus == 0 && input.IsMove;
    }
    public class IsNotBattleModeIdleTransition : CharacterStateTransitionBase
    {
        private readonly IFocusInputProvider    focusInput;
        private readonly IMoveInputProvider     input;
        public IsNotBattleModeIdleTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            focusInput = actor.gameObject.GetComponent<IFocusInputProvider>();
            input = actor.MoveInput;
        }
        public override bool IsTransition() => focusInput.Foucus < 1&&!input.IsMove;
    }

    /// <summary>
    /// ��莞�Ԉȍ~�̈ړ����͂ɂ��J��
    /// </summary>
    public class IsTimerAndMoveTransition : CharacterStateTransitionBase
    {
        private readonly Timer              timer;
        private readonly IMoveInputProvider input;
        public IsTimerAndMoveTransition(IPlayerSetup actor, Timer _timer, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            timer = _timer;
            input = actor.gameObject.GetComponent<IMoveInputProvider>();
        }
        public override bool IsTransition() => timer.IsEnd() && input.IsMove;
    }
    public class IsTimerAndNotMoveTransition : CharacterStateTransitionBase
    {
        private readonly Timer              timer;
        private readonly IMoveInputProvider input;
        public IsTimerAndNotMoveTransition(IPlayerSetup actor, Timer _timer, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            timer = _timer;
            input = actor.gameObject.GetComponent<IMoveInputProvider>();
        }
        public override bool IsTransition() => timer.IsEnd() && !input.IsMove;
    }

    public class IsTimerTransition : CharacterStateTransitionBase
    {
        private readonly Timer timer;
        public IsTimerTransition(ICharacterSetup actor, Timer _timer, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            timer = _timer;
        }
        public override bool IsTransition() => timer.IsEnd();
    }

    /// <summary>
    /// �W�����v���͂ɂ��J��
    /// </summary>
    public class IsJumpPushTransition : CharacterStateTransitionBase
    {
        private readonly IJumpInputProvider input;
        private readonly IPlayerAnimator    animator;
        public IsJumpPushTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IJumpInputProvider>();
            animator = actor.PlayerAnimator;
        }
        public override bool IsTransition() => input.Jump&&animator.Animator.GetInteger(animator.LandName) == -1;

        
    }
    public class IsNotJumpTransition : CharacterStateTransitionBase
    {
        private readonly IGroundCheck       groundCheck;

        private readonly IVelocityComponent velocity;

        private Timer jumpStartTimer;
        public IsNotJumpTransition(IPlayerSetup actor,Timer _t, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            groundCheck = actor.GroundCheck;
            velocity = actor.Velocity;
            jumpStartTimer = _t;
        }
        public override bool IsTransition() => velocity.Rigidbody.velocity.y < -0.5f && groundCheck.Landing&&jumpStartTimer.IsEnd();
    }

    public class IsJumpToFallTransition : CharacterStateTransitionBase
    {
        private readonly IPlayerAnimator    animator;
        private readonly IGroundCheck       groundCheck;
        private readonly IVelocityComponent velocity;
        public IsJumpToFallTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            animator = actor.PlayerAnimator;
            groundCheck = actor.GroundCheck;
            velocity = actor.Velocity;
        }
        public override bool IsTransition() => !groundCheck.Landing && velocity.Rigidbody.velocity.y < -0.5f &&
            animator.Animator.GetInteger(animator.JumpTypeName) == 1 && animator.IsEndMotion();


    }

    /// <summary>
    /// ���[�����O�ɂ��J��
    /// </summary>
    public class IsRollingTransition : CharacterStateTransitionBase
    {

        private readonly IJumpInputProvider input;

        private readonly IPlayerStauts      stauts;
        public IsRollingTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IJumpInputProvider>();
            stauts = actor.Stauts;
        }
        public override bool IsTransition() => input.Jump&&stauts.SP > 0&&stauts.SP > stauts.RollingUseSP;
    }
    public class IsNotRollingTransition : CharacterStateTransitionBase
    {
        private readonly IPlayerAnimator animator;

        private readonly Timer          timer;

        public IsNotRollingTransition(IPlayerSetup actor,Timer t, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            animator = actor.PlayerAnimator;
            timer = t;
        }
        public override bool IsTransition() => animator.Animator.GetInteger("Rolling") > -1&&animator.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f&&
                                                timer.IsEnd();
    }

    /// <summary>
    /// �n�ʂƂ̐ڐG�ɂ��J��
    /// </summary>
    public class IsGroundTransition : CharacterStateTransitionBase
    {
        private readonly IGroundCheck       groundCheck;

        private readonly IVelocityComponent velocity;
        public IsGroundTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            groundCheck = actor.GroundCheck;
            velocity = actor.Velocity;
        }
        public override bool IsTransition() => velocity.Rigidbody.velocity.y < -0.1f &&groundCheck.Landing;
    }
    public class IsNotGroundTransition : CharacterStateTransitionBase
    {
        private readonly IGroundCheck       groundCheck;
        private readonly IVelocityComponent velocity;
        public IsNotGroundTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            groundCheck = actor.GroundCheck;
            velocity = actor.Velocity;
        }
        public override bool IsTransition() => !groundCheck.Landing&&velocity.Rigidbody.velocity.y < -5.0f;
    }

    /// <summary>
    /// ���x�ɂ��J��
    /// </summary>
    public class IsFallVelocityTransition : CharacterStateTransitionBase
    {
        private readonly IVelocityComponent velocity;
        public IsFallVelocityTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            velocity = actor.Velocity;
        }
        public override bool IsTransition() => velocity.Rigidbody.velocity.y < -1.0f;
    }

    public class IsClimbTransition : CharacterStateTransitionBase
    {
        private readonly IObstacleJudgment cliffJudgment;
        public IsClimbTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            cliffJudgment = actor.ObstacleJudgment;
        }
        public override bool IsTransition() => cliffJudgment.IsClimbStart;
    }

    public class IsEndClimbTransition : CharacterStateTransitionBase
    {
        private readonly IClimb climb;
        public IsEndClimbTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            climb = actor.Climb;
        }
        public override bool IsTransition() => climb.IsClimbEnd;
    }

    public class IsFirstAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider   input;
        private readonly IBattleFlagger         battleFlagger;
        private readonly IGroundCheck           groundCheck;
        public IsFirstAttackTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            battleFlagger = actor.BattleFlagger;
            groundCheck = actor.GroundCheck;
        }
        public override bool IsTransition() => input.Attack && battleFlagger.IsBattleMode && groundCheck.Landing;
    }

    public class IsWeaponOutFirstAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider input;
        private readonly IBattleFlagger battleFlagger;
        private readonly IGroundCheck groundCheck;
        public IsWeaponOutFirstAttackTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            battleFlagger = actor.BattleFlagger;
            groundCheck = actor.GroundCheck;
        }
        public override bool IsTransition() => battleFlagger.IsBattleMode && groundCheck.Landing;
    }
    public class IsLoopFirstAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider   input;
        private readonly IGroundCheck           groundCheck;
        private readonly IPlayerAnimator        animator;

        private readonly float                  maxNormalizedTime;
        public IsLoopFirstAttackTransition(float _t,IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            groundCheck = actor.GroundCheck;
            maxNormalizedTime = _t;
            animator = actor.PlayerAnimator;
        }
        public override bool IsTransition() => input.Attack && groundCheck.Landing&&
            animator.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= maxNormalizedTime;
    }

    public class IsBurstAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider   input;
        private readonly IGroundCheck           groundCheck;
        private readonly IPlayerAnimator        animator;

        private readonly float                  maxNormalizedTime;

        private readonly string                 motionName;
        public IsBurstAttackTransition(string _motionName,float _t, IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            groundCheck = actor.GroundCheck;
            animator = actor.PlayerAnimator;
            maxNormalizedTime = _t;
            motionName = _motionName;
        }
        public override bool IsTransition() =>
            input.Attack && groundCheck.Landing&&
            animator.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= maxNormalizedTime&&
            animator.Animator.GetCurrentAnimatorStateInfo(0).IsName(motionName);
    }
    public class IsSecondAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider input;
        private readonly IGroundCheck groundCheck;
        private readonly IPlayerAnimator animator;

        private readonly float maxNormalizedTime;

        private readonly float noNormalizedTime;

        private readonly string motionName;
        public IsSecondAttackTransition(string _motionName, float _t,float _noTime, IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            groundCheck = actor.GroundCheck;
            animator = actor.PlayerAnimator;
            maxNormalizedTime = _t;
            noNormalizedTime = _noTime;
            motionName = _motionName;
        }
        public override bool IsTransition() =>
            input.Attack && groundCheck.Landing &&
            animator.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= maxNormalizedTime &&
            animator.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < noNormalizedTime &&
            animator.Animator.GetCurrentAnimatorStateInfo(0).IsName(motionName);
    }

    public class IsNotAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider   input;
        private readonly IPlayerAnimator        animator;

        private readonly string[] attackMotionNames = new string[]
        {
            "FirstAttack",
            "SecondAttack",
            "ThirdAttack",
            "CounterAttack"
        };
        public IsNotAttackTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            animator = actor.PlayerAnimator;
        }

        private bool AttackMotionChecker()
        {
            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            for(int i = 0; i < attackMotionNames.Length; i++)
            {
                if (animInfo.IsName(attackMotionNames[i])&&animInfo.normalizedTime >= 1.0f)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool IsTransition() =>
            !input.Attack &&
            AttackMotionChecker();
    }

    public class IsNotAttackToMoveTransition : CharacterStateTransitionBase
    {
        private readonly IMoveInputProvider     moveinput;
        private readonly IAttackInputProvider   input;
        private readonly IPlayerAnimator        animator;

        private readonly string[] attackMotionNames = new string[]
        {
            "FirstAttack",
            "SecondAttack",
            "ThirdAttack"
        };

        private readonly float                  maxNormalizedTime;
        public IsNotAttackToMoveTransition(float _t,IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            moveinput = actor.MoveInput;
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            animator = actor.PlayerAnimator;
            maxNormalizedTime = _t;
        }

        private bool AttackMotionChecker()
        {
            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            for (int i = 0; i < attackMotionNames.Length; i++)
            {
                if (animInfo.IsName(attackMotionNames[i]) && animInfo.normalizedTime >= maxNormalizedTime)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool IsTransition() =>
            !input.Attack &&
            AttackMotionChecker()&&
            moveinput.IsMove;
    }

    public class IsWeaponOutTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider   input;
        private readonly IBattleFlagger         battleFlagger;

        public IsWeaponOutTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            battleFlagger = actor.BattleFlagger;
        }
        public override bool IsTransition() => input.Attack && !battleFlagger.IsBattleMode;
    }
    public class IsWeaponInTransition : CharacterStateTransitionBase
    {
        private readonly IToolInputProvider     input;
        private readonly IFocusInputProvider    focusInput;
        private readonly IBattleFlagger         battleFlagger;

        public IsWeaponInTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IToolInputProvider>();
            focusInput = actor.gameObject.GetComponent <IFocusInputProvider>();
            battleFlagger = actor.BattleFlagger;
        }
        public override bool IsTransition() => (input.WeaponEquipment||input.Equipmenting > 0) && battleFlagger.IsBattleMode &&
                                                focusInput.Foucus < 1.0f;
    }

    public class IsReadyJumpAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider   input;
        private readonly IMoveInputProvider     moveInput;
        private readonly IGroundCheck           groundCheck;
        private readonly IPlayerStauts          stauts;

        private readonly int useSP;

        public IsReadyJumpAttackTransition(IPlayerSetup actor,int usesp, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            groundCheck = actor.GroundCheck;
            moveInput = actor.MoveInput;
            stauts = actor.Stauts;
            useSP = usesp;
        }
        public override bool IsTransition() => input.Attack &&moveInput.IsMove&&
            stauts.SP > 0&&stauts.SP > useSP &&!groundCheck.Landing;
    }
    public class IsSecondAttackVer2ToReadyJumpAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider   input;
        private readonly IGroundCheck           groundCheck;
        private readonly IPlayerAnimator        animator;

        private readonly string                 motionName;

        public IsSecondAttackVer2ToReadyJumpAttackTransition(IPlayerSetup actor,string name, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            groundCheck = actor.GroundCheck;
            animator = actor.PlayerAnimator;
            motionName = name;
        }
        public override bool IsTransition() => input.Attack && animator.Animator.GetCurrentAnimatorStateInfo(0).IsName(motionName) &&
            !groundCheck.Landing;
    }

    public class IsCounterAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider   input;
        private readonly IPlayerAnimator        animator;

        private readonly string                 motionName;

        private readonly IPlayerStauts          stauts;

        private readonly int                    useSP;

        public IsCounterAttackTransition(IPlayerSetup actor,int usesp, string name, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            animator = actor.PlayerAnimator;
            motionName = name;
            stauts = actor.Stauts;
            useSP = usesp;
        }
        public override bool IsTransition() => input.Attack &&
            animator.Animator.GetCurrentAnimatorStateInfo(0).IsName(motionName)&&
            stauts.SP > 0 && stauts.SP > useSP;
    }

    public class IsJumpAttackTransition : CharacterStateTransitionBase
    {
        private readonly IPlayerAnimator    animator;

        private readonly string             readyJumpAttackName = "JumpAttackPosture";

        public IsJumpAttackTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            animator = actor.PlayerAnimator;
        }
        public override bool IsTransition() => animator.Animator.GetCurrentAnimatorStateInfo(0).IsName(readyJumpAttackName)&&
            animator.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime  >= 1.0f;
    }
    public class IsPlayerChargeStartTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider   attackInputProvider;

        private readonly IPlayerStauts          stauts;


        public IsPlayerChargeStartTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            attackInputProvider = actor.AttackInput;
            stauts = actor.Stauts;
        }

        public override bool IsTransition() => attackInputProvider.ChargeAttack&&stauts.SP > 0&&stauts.SP > stauts.ChargeAttackUseSP;
    }
    public class IsPlayerChargeAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider attackInputProvider;


        public IsPlayerChargeAttackTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            attackInputProvider = actor.AttackInput;
        }

        public override bool IsTransition() => !attackInputProvider.ChargeAttack;
    }


    public class IsPlayerEndMotionTransition : CharacterStateTransitionBase
    {

        private readonly IPlayerAnimator    animator;

        private readonly string             motionName;

        public IsPlayerEndMotionTransition(IPlayerSetup actor, string name, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            animator = actor.PlayerAnimator;
            motionName = name;
        }

        private bool AttackMotionEndChecker()
        {
            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            if (animInfo.IsName(motionName) && animInfo.normalizedTime >= 1.0f)
            {
                return true;
            }
            return false;
        }
        public override bool IsTransition() => AttackMotionEndChecker();
    }

    public class IsDamageTransition : CharacterStateTransitionBase
    {

        private readonly IBaseStauts baseStauts;

        public IsDamageTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            baseStauts = actor.BaseStauts;
        }
        public override bool IsTransition() => baseStauts.MaxStoredDamage <= baseStauts.StoredDamage;
    }
    public class IsPlayerDamageToGetUpTransition : CharacterStateTransitionBase
    {

        private readonly Timer              damageTimer;

        private readonly IPlayerAnimator    animator;

        public IsPlayerDamageToGetUpTransition(IPlayerSetup actor, Timer t, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            damageTimer = t;
            animator = actor.PlayerAnimator;
        }
        public override bool IsTransition() => damageTimer.IsEnd() && animator.Animator.GetCurrentAnimatorStateInfo(0).IsName("BigImpact");
    }

    public class IsNotPlayerDamageToBattleTransition : CharacterStateTransitionBase
    {

        private readonly IBattleFlagger battleFlagger;

        private readonly Timer          damageTimer;

        public IsNotPlayerDamageToBattleTransition(IPlayerSetup actor,Timer t, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            battleFlagger = actor.BattleFlagger;
            damageTimer = t;
        }
        public override bool IsTransition() => damageTimer.IsEnd()&&battleFlagger.IsBattleMode;
    }
    public class IsNotPlayerDamageToTransition : CharacterStateTransitionBase
    {

        private readonly IBattleFlagger battleFlagger;

        private readonly Timer          damageTimer;

        public IsNotPlayerDamageToTransition(IPlayerSetup actor, Timer t, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            battleFlagger = actor.BattleFlagger;
            damageTimer = t;
        }
        public override bool IsTransition() => damageTimer.IsEnd() && !battleFlagger.IsBattleMode;
    }

    public class IsDeathTransition : CharacterStateTransitionBase
    {

        private readonly IBaseStauts stauts;

        public IsDeathTransition(ICharacterSetup chara, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            stauts = chara.BaseStauts;
        }
        public override bool IsTransition() => stauts.HP <= 0;
    }

    public class IsGuardTransition : CharacterStateTransitionBase
    {

        private readonly IGuardTrigger guardTrigger;

        public IsGuardTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            guardTrigger = actor.GuardTrigger;
        }
        public override bool IsTransition() => guardTrigger.IsGuard;
    }
    public class IsFailGuardTransition : CharacterStateTransitionBase
    {

        private readonly IGuardTrigger guardTrigger;

        private readonly IPlayerStauts stauts;

        public IsFailGuardTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            guardTrigger = actor.GuardTrigger;
            stauts = actor.Stauts;
        }
        public override bool IsTransition() => guardTrigger.IsGuard&&(stauts.SP <= 0||stauts.SP < stauts.GuardUseSP);
    }
    public class IsEndGuardTransition : CharacterStateTransitionBase
    {

        private readonly IPlayerAnimator    animator;

        private readonly IGuardTrigger      guardTrigger;

        public IsEndGuardTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            animator = actor.PlayerAnimator;
            guardTrigger = actor.GuardTrigger;
        }
        public override bool IsTransition() => guardTrigger.IsGuard && 
                                               animator.Animator.GetCurrentAnimatorStateInfo(0).IsName("Shield Impact") &&
                                               animator.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
    }
}
