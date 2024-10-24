using System.Collections;
using UnityEngine;

namespace MyAssets
{
    public interface IPlayerAnimator
    {
        Animator Animator { get;}

        string MoveName { get; }
        string DashName {  get; }
        string AlertLevelName {  get; }
        string BattleModeName {  get; }
        string JumpTypeName {  get; }
        string FallName {  get; }
        string LandName {  get; }
        string AttacksName {  get; }
        string Weapon_In_OutName {  get; }
        string ClimbName {  get; }
    }
}
