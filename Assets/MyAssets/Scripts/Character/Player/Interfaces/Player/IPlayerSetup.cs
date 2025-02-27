using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// �S�L�����N�^�[���ʂ̃C���^�t�F�[�X
    /// </summary>
    public interface ICharacterSetup
    {
        IStateMachine           StateMachine { get; }
        IBaseStatus             BaseStatus { get; }
        GameObject              gameObject { get; }
        IVelocityComponent      Velocity { get; }
        IMovement               Movement { get; }
        IStepClimberJudgment    StepClimberJudgment { get; }
        IRotation               Rotation { get; }

        IDamagement             Damagement { get; }
        IDamageContainer        DamageContainer { get; }

        IFieldOfView            FieldOfView { get; }

        IGuardTrigger           GuardTrigger { get; }

        CharacterType           CharaType { get; }

        SEHandler SEHandler { get; }
    }
    /// <summary>
    /// �v���C���[�Ŏg���C���^�t�F�[�X
    /// </summary>
    public interface IPlayerSetup : ICharacterSetup
    {
        IPlayerStatus           Stauts { get; }

        IMoveInputProvider      MoveInput { get; }
        IAttackInputProvider    AttackInput { get; }
        IToolInputProvider      ToolInput { get; }
        IClimb                  Climb { get; }
        IPlayerAnimator         PlayerAnimator { get; }
        IGroundCheck            GroundCheck { get; }
        IObstacleJudgment       ObstacleJudgment { get; }
        IAllIK                  IkController { get; }
        IBattleFlagger          BattleFlagger { get; }

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
        ISlimeAnimator              SlimeAnimator { get; }
        IGroundCheck                GroundCheck { get; }
        SlimeBodyAttackController   AttackObject { get; }

        SlimeEffectHandler          EffectHandler { get; }

        ISlimeRotation              SlimeRotation { get; }

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
        IMushroomAnimator           MushroomAnimator { get; }
        IGroundCheck                GroundCheck {  get; }
        MushroomAttackController    AttackObject { get; }

        MushroomEffectHandler       EffectHandler { get; }

        void RunDestroy();
    }
    /// <summary>
    /// �u���^���N�Ɏg���C���^�t�F�[�X
    /// </summary>
    public interface IBullTankSetup : IEnemySetup
    {
        IBullTankAnimator               BullTankAnimator { get; }
        IGroundCheck                    GroundCheck { get; }
        AxeController                   AttackObject { get; }
        BullTankHeadAttackController    HeadAttackObject {  get; }

        BullTankEffectHandler           EffectHandler { get; }

        void RunDestroy();
    }
    public interface IGolemSetup : IEnemySetup
    {
        IGolemAnimator      GolemAnimator { get; }
        IGroundCheck        GroundCheck { get; }
        GolemEffectHandler  EffectHandler { get; }

        GolemFistController FistController {  get; }
        void RunDestroy();
    }
    /// <summary>
    /// NPC�Ɏg���C���^�t�F�[�X
    /// </summary>
    public interface INPCSetup : ICharacterSetup
    {
        public INPCAnimator     Animator { get; }

        public INPCCommandPanel CommandPanel { get; }
    }
}

