using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// 全キャラクター共通のインタフェース
    /// </summary>
    public interface ICharacterSetup
    {
        IStateMachine           StateMachine { get; }
        IBaseStauts             BaseStauts { get; }
        GameObject              gameObject { get; }
        IVelocityComponent      Velocity { get; }
        IMovement               Movement { get; }
        IStepClimberJudgment    StepClimberJudgment { get; }
        IRotation               Rotation { get; }

        IDamagement             Damagement { get; }
        IDamageContainer        DamageContainer { get; }

        IFieldOfView            FieldOfView { get; }
    }
    /// <summary>
    /// プレイヤーで使うインタフェース
    /// </summary>
    public interface IPlayerSetup : ICharacterSetup
    {
        IPlayerStauts           Stauts { get; }

        IMoveInputProvider      MoveInput { get; }
        IAttackInputProvider    AttackInput { get; }
        IToolInputProvider      ToolInput { get; }
        IClimb                  Climb { get; }
        IPlayerAnimator         PlayerAnimator { get; }
        IGroundCheck            GroundCheck { get; }
        IObstacleJudgment       ObstacleJudgment { get; }
        IAllIK                 FootIK { get; }
        IChangingState          ChangingState { get; }

        IEquipment              Equipment { get; }
    }
    /// <summary>
    /// 敵(スライム)で使うインタフェース
    /// </summary>
    public interface ISlimeSetup : ICharacterSetup
    {
        ISlimeAnimator SlimeAnimator { get; }
        IGroundCheck GroundCheck { get; }
        SlimeBodyAttackController AttackObject { get; }

        void RunDestroy();
    }
    /// <summary>
    /// かかしとかプレイヤーの戦闘練習台に使うインタフェース
    /// </summary>
    public interface IDummySetup : ICharacterSetup
    {

    }
    /// <summary>
    /// NPCに使うインタフェース
    /// </summary>
    public interface INPCSetup : ICharacterSetup
    {
        public INPCAnimator Animator { get; }

        public INPCCommandPanel CommandPanel { get; }
    }
}

