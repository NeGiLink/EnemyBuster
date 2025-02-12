using UnityEngine;

namespace MyAssets
{
    /*
     * 各キャラクターのアニメーション関係の処理をまとめたクラスの
     * インターフェース
     */

    public interface IPlayerAnimator
    {
        Animator Animator { get;}
        AnimatorStateInfo AnimatorStateInfo {  get;}
        bool IsEndMotion();


        int VelocityXAnimationID { get; }
        int VelocityZAnimationID { get; }
        int MoveAnimationID { get; }
        int DashAnimationID { get; }
        int AlertLevelAnimationID { get; }
        int BattleModeAnimationID { get; }
        int ToolLevelAnimationID { get; }
        int JumpAnimationID { get; }
        int FallAnimationID { get; }
        int LandAnimationID { get; }
        int AttackAnimationID { get; }
        int ChargeAttackAnimationID {  get; }
        int Weapon_In_OutAnimationID {  get; }
        int ClimbAnimationID { get; }
        int ImpactAnimationID {  get; }

        void SetWeight(bool enabled, int layer);
        void UpdateWeight();
    }
    public interface IEnemyAnimator
    {
        Animator Animator { get; }
    }

    public interface ISlimeAnimator : IEnemyAnimator
    {
        int MoveAnimationID { get; }
        int AttackAnimationID { get; }
        int AttackTriggerAnimationID { get; }
        int DeathAnimationID { get; }
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
        int MoveAnimationID { get; }
    }
}
