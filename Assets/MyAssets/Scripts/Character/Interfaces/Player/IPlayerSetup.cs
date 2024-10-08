using UnityEngine;

namespace MyAssets
{
    public interface IPlayerSetup
    {
        GameObject          gameObject { get; }
        IMoveInputProvider  MoveInput { get; }
        IVelocityComponent  Velocity {  get; }
        IMovement           Movement { get; }
        IStateMachine       StateMachine { get; }
        IPlayerAnimator     PlayerAnimator { get; }
    }
}

