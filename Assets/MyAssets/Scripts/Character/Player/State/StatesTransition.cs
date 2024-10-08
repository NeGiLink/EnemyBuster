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
}
