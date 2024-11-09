using System.Collections;
using UnityEngine;

namespace MyAssets
{
    public interface IPlayerAnimator
    {
        Animator Animator { get;}

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

    public interface ISlimeAnimator
    {
        Animator Animator { get; }
        /*
        string VelocityX { get; }
        string VelocityZ { get; }
        string MoveName { get; }
        string DashName { get; }
        string AlertLevelName { get; }
        string BattleModeName { get; }
        string ToolLevel { get; }
        string JumpTypeName { get; }
        string FallName { get; }
        string LandName { get; }
        string AttacksName { get; }
        string Weapon_In_OutName { get; }
        string ClimbName { get; }

        void SetWeight(bool enabled, int layer);
        void UpdateWeight();
        */
    }
}
