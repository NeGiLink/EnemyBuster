using UnityEngine;

namespace MyAssets
{
    public interface IPlayerSetup
    {
        GameObject          gameObject { get; }
        ICharacterRotation  Rotation { get; }
        IMoveInputProvider  MoveInput { get; }
        IVelocityComponent  Velocity {  get; }
        IMovement           Movement { get; }
        IClimb              Climb { get; }
        IStateMachine       StateMachine { get; }
        IPlayerAnimator     PlayerAnimator { get; }
        IGroundCheck        GroundCheck { get; }
        IObstacleJudgment   ObstacleJudgment { get; }
        IStepClimberJudgment StepClimberJudgment {  get; }
    }
}

