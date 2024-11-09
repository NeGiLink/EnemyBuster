using UnityEngine;

namespace MyAssets
{
    public interface ICharacterSetup
    {
        GameObject gameObject { get; }
        IVelocityComponent Velocity { get; }
        ICharacterMovement Movement { get; }
        IStepClimberJudgment StepClimberJudgment { get; }
        IRotation Rotation { get; }
    }
    public interface IPlayerSetup : ICharacterSetup
    {
        IMoveInputProvider      MoveInput { get; }
        IAttackInputProvider    AttackInput { get; }
        IToolInputProvider      ToolInput { get; }
        IClimb                  Climb { get; }
        IStateMachine           StateMachine { get; }
        IPlayerAnimator         PlayerAnimator { get; }
        IGroundCheck            GroundCheck { get; }
        IObstacleJudgment       ObstacleJudgment { get; }
        IFootIK                 FootIK { get; }
        IChangingState          ChangingState { get; }

        IEquipment              Equipment { get; }
    }

    public interface ISlimeSetup : ICharacterSetup
    {
        IStateMachine StateMachine { get; }
        ISlimeAnimator SlimeAnimator { get; }
        IGroundCheck GroundCheck { get; }
    }
}

