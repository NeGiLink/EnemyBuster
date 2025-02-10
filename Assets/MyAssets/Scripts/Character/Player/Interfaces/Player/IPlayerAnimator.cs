using UnityEngine;

namespace MyAssets
{
    public interface IPlayerAnimator
    {
        Animator Animator { get;}
        AnimatorStateInfo AnimatorStateInfo {  get;}
        bool IsEndMotion();
        string VelocityX {  get;}
        string VelocityZ {  get;}
        string MoveName { get; }
        string DashName {  get; }
        string AlertLevelName {  get; }
        string BattleModeName {  get; }
        string ToolLevel {  get; }
        string JumpTypeName {  get; }
        string FallName {  get; }
        string LandName {  get; }
        string AttacksName {  get; }
        string Weapon_In_OutName {  get; }
        string ClimbName {  get; }

        void SetWeight(bool enabled, int layer);
        void UpdateWeight();
    }
    public interface IEnemyAnimator
    {
        Animator Animator { get; }
    }

    public interface ISlimeAnimator : IEnemyAnimator
    {
        string MoveName { get; }
        string AttacksName { get; }
    }

    public interface IMushroomAnimator : IEnemyAnimator
    {
        int MoveAnimationID { get; }
        int AttackAnimationID { get; }
        int ImpactAnimationID { get; }
        int DeathAnimationID { get; }
    }

    public interface IBullTankAnimator : IEnemyAnimator
    {
        int MoveAnimationID {  get; }
        int SideMoveAnimationID { get; }
        int AttackAnimationID { get; }
        int ReadyAttackAnimationID { get; }
        int ImpactAnimationID { get; }
        int DeathAnimationID { get; }
    }

    public interface IGolemAnimator : IEnemyAnimator
    {
        int MoveAnimationID { get; }
        int AttackAnimationID { get; }
        int ImpactAnimationID { get; }
        int DeathAnimationID { get; }
    }

    public interface INPCAnimator
    {
        Animator Animator { get; }
    }
}
