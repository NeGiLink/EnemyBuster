using UnityEngine;

namespace MyAssets
{
    public interface ICharacterSetup
    {
        IBaseStauts            BaseStauts { get; }
        GameObject gameObject { get; }
        IVelocityComponent Velocity { get; }
        IMovement Movement { get; }
        IStepClimberJudgment StepClimberJudgment { get; }
        IRotation Rotation { get; }

        IDamagement Damagement { get; }
        IDamageContainer DamageContainer { get; }
    }
    public interface IPlayerSetup : ICharacterSetup
    {
        IPlayerStauts           Stauts { get; }

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
        SlimeBodyAttackController AttackObject { get; }

        void RunDestroy();
    }
}

