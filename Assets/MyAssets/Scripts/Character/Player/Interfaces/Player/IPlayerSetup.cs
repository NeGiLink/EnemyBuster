using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// �S�L�����N�^�[���ʂ̃C���^�t�F�[�X
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
    /// �v���C���[�Ŏg���C���^�t�F�[�X
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

    public interface IEnemySetup : ICharacterSetup
    {
        IEnemyAnimator EnemyAnimator { get; }
    }

    /// <summary>
    /// �G(�X���C��)�Ŏg���C���^�t�F�[�X
    /// </summary>
    public interface ISlimeSetup : IEnemySetup
    {
        ISlimeAnimator SlimeAnimator { get; }
        IGroundCheck GroundCheck { get; }
        SlimeBodyAttackController AttackObject { get; }

        void RunDestroy();
    }
    /// <summary>
    /// �������Ƃ��v���C���[�̐퓬���K��Ɏg���C���^�t�F�[�X
    /// </summary>
    public interface IDummySetup : IEnemySetup
    {

    }
    /// <summary>
    /// �L�m�R�����X�^�[�Ɏg���C���^�t�F�[�X
    /// </summary>
    public interface IMushroomSetup : IEnemySetup
    {
        IMushroomAnimator MushroomAnimator { get; }
        IGroundCheck GroundCheck {  get; }
        MushroomAttackController AttackObject { get; }

        void RunDestroy();
    }
    /// <summary>
    /// NPC�Ɏg���C���^�t�F�[�X
    /// </summary>
    public interface INPCSetup : ICharacterSetup
    {
        public INPCAnimator Animator { get; }

        public INPCCommandPanel CommandPanel { get; }
    }
}

