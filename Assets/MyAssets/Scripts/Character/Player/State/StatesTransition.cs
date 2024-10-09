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
    /// ÉWÉÉÉìÉvì¸óÕÇ…ÇÊÇÈëJà⁄
    /// </summary>
    public class IsJumpPushTransition : PlayerStateTransitionBase
    {
        private readonly IGroundCheck groundCheck;
        readonly IJumpInputProvider input;
        public IsJumpPushTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            groundCheck = actor.GroundCheck;
            input = actor.gameObject.GetComponent<IJumpInputProvider>();
        }
        public override bool IsTransition() => input.Jump > 0 &&groundCheck.Landing;
    }
    public class IsNotJumpTransition : PlayerStateTransitionBase
    {
        private readonly IGroundCheck groundCheck;

        private readonly IJumpInputProvider input;
        public IsNotJumpTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            groundCheck = actor.GroundCheck;
            input = actor.gameObject.GetComponent<IJumpInputProvider>();
        }
        public override bool IsTransition() => input.Jump < 1&& groundCheck.Landing;
    }

    /// <summary>
    /// ínñ Ç∆ÇÃê⁄êGÇ…ÇÊÇÈëJà⁄
    /// </summary>
    public class IsGroundTransition : PlayerStateTransitionBase
    {
        private readonly IGroundCheck groundCheck;
        public IsGroundTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            groundCheck = actor.GroundCheck;
        }
        public override bool IsTransition() => groundCheck.Landing;
    }
    public class IsNotGroundTransition : PlayerStateTransitionBase
    {
        private readonly IGroundCheck groundCheck;
        public IsNotGroundTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            groundCheck = actor.GroundCheck;
        }
        public override bool IsTransition() => !groundCheck.Landing;
    }

    /// <summary>
    /// ë¨ìxÇ…ÇÊÇÈëJà⁄
    /// </summary>
    public class IsFallVelocityTransition : PlayerStateTransitionBase
    {
        readonly IVelocityComponent velocity;
        public IsFallVelocityTransition(IPlayerSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            velocity = actor.Velocity;
        }
        public override bool IsTransition() => velocity.Rigidbody.velocity.y < 0;
    }
}
