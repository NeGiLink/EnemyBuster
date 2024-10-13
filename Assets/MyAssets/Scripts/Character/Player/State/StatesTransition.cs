using UnityEngine;

namespace MyAssets
{
    public class IsMoveTransition : PlayerStateTransitionBase
    {
        private readonly IMoveInputProvider input;
        public IsMoveTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IMoveInputProvider>();
        }
        public override bool IsTransition() => input.IsMove;
    }
    public class IsNotMoveTransition : PlayerStateTransitionBase
    {
        private readonly IMoveInputProvider input;
        public IsNotMoveTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            input = actor.gameObject.GetComponent<IMoveInputProvider>();
        }
        public override bool IsTransition() => !input.IsMove;
    }

    /// <summary>
    /// ˆê’èŠÔˆÈ~‚ÌˆÚ“®“ü—Í‚É‚æ‚é‘JˆÚ
    /// </summary>
    public class IsTimerAndMoveTransition : PlayerStateTransitionBase
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
    public class IsTimerAndNotMoveTransition : PlayerStateTransitionBase
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

    /// <summary>
    /// ƒWƒƒƒ“ƒv“ü—Í‚É‚æ‚é‘JˆÚ
    /// </summary>
    public class IsJumpPushTransition : PlayerStateTransitionBase
    {
        private readonly IGroundCheck groundCheck;
        private readonly IJumpInputProvider input;
        public IsJumpPushTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            groundCheck = actor.GroundCheck;
            input = actor.gameObject.GetComponent<IJumpInputProvider>();
        }
        public override bool IsTransition() => input.Jump;

        
    }
    public class IsNotJumpTransition : PlayerStateTransitionBase
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
        public override bool IsTransition() => velocity.Rigidbody.velocity.y < -0.5f&& !input.Jump && groundCheck.Landing;
    }

    /// <summary>
    /// ’n–Ê‚Æ‚ÌÚG‚É‚æ‚é‘JˆÚ
    /// </summary>
    public class IsGroundTransition : PlayerStateTransitionBase
    {
        private readonly IGroundCheck groundCheck;

        private readonly IVelocityComponent velocity;
        public IsGroundTransition(IPlayerSetup player, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            groundCheck = player.GroundCheck;
            velocity = player.Velocity;
        }
        public override bool IsTransition() => velocity.Rigidbody.velocity.y > -0.1f&&groundCheck.Landing;
    }
    public class IsNotGroundTransition : PlayerStateTransitionBase
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
    /// ‘¬“x‚É‚æ‚é‘JˆÚ
    /// </summary>
    public class IsFallVelocityTransition : PlayerStateTransitionBase
    {
        readonly IVelocityComponent velocity;
        public IsFallVelocityTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            velocity = actor.Velocity;
        }
        public override bool IsTransition() => velocity.Rigidbody.velocity.y < -1.0f;
    }

    public class IsClimbTransition : PlayerStateTransitionBase
    {
        private readonly IObstacleJudgment cliffJudgment;
        public IsClimbTransition(IPlayerSetup player, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            cliffJudgment = player.ObstacleJudgment;
        }
        public override bool IsTransition() => cliffJudgment.IsClimbStart;
    }

    public class IsEndClimbTransition : PlayerStateTransitionBase
    {
        private readonly IClimb climb;
        public IsEndClimbTransition(IPlayerSetup player, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            climb = player.Climb;
        }
        public override bool IsTransition() => climb.IsClimbEnd;
    }
}
