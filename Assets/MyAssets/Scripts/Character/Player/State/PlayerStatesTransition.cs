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

    public class IsBattleModeTransition : CharacterStateTransitionBase
    {
        private IFocusInputProvider focusInput;
        public IsBattleModeTransition(IPlayerSetup character, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            focusInput = character.gameObject.GetComponent<IFocusInputProvider>();
        }
        public override bool IsTransition() => focusInput.Foucus > 0;
    }
    public class IsNotBattleModeTransition : CharacterStateTransitionBase
    {
        private IFocusInputProvider focusInput;
        public IsNotBattleModeTransition(IPlayerSetup character, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            focusInput = character.gameObject.GetComponent<IFocusInputProvider>();
        }
        public override bool IsTransition() => focusInput.Foucus < 1;
    }

    /// <summary>
    /// àÍíËéûä‘à»ç~ÇÃà⁄ìÆì¸óÕÇ…ÇÊÇÈëJà⁄
    /// </summary>
    public class IsTimerAndMoveTransition : CharacterStateTransitionBase
    {
        private readonly Timer timer;
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
        private readonly Timer timer;
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
    /// ÉWÉÉÉìÉvì¸óÕÇ…ÇÊÇÈëJà⁄
    /// </summary>
    public class IsJumpPushTransition : CharacterStateTransitionBase
    {
        private readonly IGroundCheck groundCheck;
        private readonly IJumpInputProvider input;
        private readonly IPlayerAnimator animator;
        public IsJumpPushTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            groundCheck = actor.GroundCheck;
            input = actor.gameObject.GetComponent<IJumpInputProvider>();
            animator = actor.PlayerAnimator;
        }
        public override bool IsTransition() => input.Jump&&animator.Animator.GetInteger(animator.LandName) == -1;

        
    }
    public class IsNotJumpTransition : CharacterStateTransitionBase
    {
        private readonly IGroundCheck groundCheck;

        private readonly IVelocityComponent velocity;

        private readonly IJumpInputProvider input;
        public IsNotJumpTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            groundCheck = actor.GroundCheck;
            velocity = actor.Velocity;
            input = actor.gameObject.GetComponent<IJumpInputProvider>();
        }
        public override bool IsTransition() => velocity.Rigidbody.velocity.y < -0.5f && groundCheck.Landing;
    }
    /// <summary>
    /// ÉçÅ[ÉäÉìÉOÇ…ÇÊÇÈëJà⁄
    /// </summary>
    public class IsRollingTransition : CharacterStateTransitionBase
    {
        private readonly IPlayerAnimator animator;

        private readonly IJumpInputProvider input;

        private readonly IFocusInputProvider focusInput;
        public IsRollingTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            animator = actor.PlayerAnimator;
            input = actor.gameObject.GetComponent<IJumpInputProvider>();
            focusInput = actor.gameObject.GetComponent<IFocusInputProvider>();
        }
        public override bool IsTransition() => input.Jump;
    }
    public class IsNotRollingTransition : CharacterStateTransitionBase
    {
        private readonly IPlayerAnimator animator;

        private readonly IJumpInputProvider input;

        private readonly IFocusInputProvider focusInput;
        public IsNotRollingTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            animator = actor.PlayerAnimator;
            input = actor.gameObject.GetComponent<IJumpInputProvider>();
            focusInput = actor.gameObject.GetComponent<IFocusInputProvider>();
        }
        public override bool IsTransition() => animator.Animator.GetInteger("Rolling") > -1&&animator.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
    }

    /// <summary>
    /// ínñ Ç∆ÇÃê⁄êGÇ…ÇÊÇÈëJà⁄
    /// </summary>
    public class IsGroundTransition : CharacterStateTransitionBase
    {
        private readonly IGroundCheck groundCheck;

        private readonly IVelocityComponent velocity;
        public IsGroundTransition(IPlayerSetup player, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            groundCheck = player.GroundCheck;
            velocity = player.Velocity;
        }
        public override bool IsTransition() => groundCheck.IsFalling&&groundCheck.Landing;
    }
    public class IsNotGroundTransition : CharacterStateTransitionBase
    {
        private readonly IGroundCheck groundCheck;
        private readonly IVelocityComponent velocity;
        public IsNotGroundTransition(IPlayerSetup player, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            groundCheck = player.GroundCheck;
            velocity = player.Velocity;
        }
        public override bool IsTransition() => !groundCheck.Landing&&velocity.Rigidbody.velocity.y < -5.0f;
    }

    /// <summary>
    /// ë¨ìxÇ…ÇÊÇÈëJà⁄
    /// </summary>
    public class IsFallVelocityTransition : CharacterStateTransitionBase
    {
        readonly IVelocityComponent velocity;
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
        public IsClimbTransition(IPlayerSetup player, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            cliffJudgment = player.ObstacleJudgment;
        }
        public override bool IsTransition() => cliffJudgment.IsClimbStart;
    }

    public class IsEndClimbTransition : CharacterStateTransitionBase
    {
        private readonly IClimb climb;
        public IsEndClimbTransition(IPlayerSetup player, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            climb = player.Climb;
        }
        public override bool IsTransition() => climb.IsClimbEnd;
    }

    public class IsFirstAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider input;
        private readonly IChangingState changingState;
        private readonly IGroundCheck groundCheck;
        public IsFirstAttackTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            changingState = actor.ChangingState;
            groundCheck = actor.GroundCheck;
        }
        public override bool IsTransition() => input.Attack && changingState.IsBattleMode && groundCheck.Landing;
    }
    public class IsLoopFirstAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider input;
        private readonly IGroundCheck groundCheck;
        private readonly IPlayerAnimator animator;

        private readonly float maxNormalizedTime;
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
        private readonly IAttackInputProvider input;
        private readonly IGroundCheck groundCheck;
        private readonly IPlayerAnimator animator;

        private readonly float maxNormalizedTime;

        private readonly string motionName;
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

    public class IsNotAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider input;
        private readonly IPlayerAnimator animator;
        private readonly string firstAttackName = "FirstAttack";

        private readonly string[] attackMotionNames = new string[]
        {
            "FirstAttack",
            "SecondAttack",
            "ThirdAttack"
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
        private readonly IMoveInputProvider moveinput;
        private readonly IAttackInputProvider input;
        private readonly IPlayerAnimator animator;
        private readonly string firstAttackName = "FirstAttack";

        private readonly string[] attackMotionNames = new string[]
        {
            "FirstAttack",
            "SecondAttack",
            "ThirdAttack"
        };

        private readonly float maxNormalizedTime;
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
        private readonly IAttackInputProvider input;
        private readonly IChangingState changingState;
        private readonly IGroundCheck groundCheck;
        private readonly IPlayerAnimator animator;

        public IsWeaponOutTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            groundCheck = actor.GroundCheck;
            changingState = actor.ChangingState;
            animator = actor.PlayerAnimator;
        }
        public override bool IsTransition() => input.Attack && !changingState.IsBattleMode;
    }
    public class IsWeaponInTransition : CharacterStateTransitionBase
    {
        private readonly IToolInputProvider input;
        private readonly IChangingState changingState;
        private readonly IGroundCheck groundCheck;
        private readonly IPlayerAnimator animator;

        public IsWeaponInTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IToolInputProvider>();
            groundCheck = actor.GroundCheck;
            changingState = actor.ChangingState;
            animator = actor.PlayerAnimator;
        }
        public override bool IsTransition() => (input.Receipt||input.Receipting > 0) && changingState.IsBattleMode;
    }

    public class IsReadyJumpAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider input;
        private readonly IMoveInputProvider moveInput;
        private readonly IGroundCheck groundCheck;
        private readonly IPlayerAnimator animator;

        public IsReadyJumpAttackTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            groundCheck = actor.GroundCheck;
            moveInput = actor.MoveInput;
            animator = actor.PlayerAnimator;
        }
        public override bool IsTransition() => input.Attack &&moveInput.IsMove&&
            !groundCheck.Landing;
    }

    public class IsJumpAttackTransition : CharacterStateTransitionBase
    {
        private readonly IAttackInputProvider input;
        private readonly IChangingState changingState;
        private readonly IGroundCheck groundCheck;
        private readonly IPlayerAnimator animator;

        private readonly string readyJumpAttackName = "JumpAttackPosture";

        public IsJumpAttackTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IAttackInputProvider>();
            groundCheck = actor.GroundCheck;
            changingState = actor.ChangingState;
            animator = actor.PlayerAnimator;
        }
        public override bool IsTransition() => animator.Animator.GetCurrentAnimatorStateInfo(0).IsName(readyJumpAttackName)&&
            animator.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime  >= 1.0f;
    }
}
