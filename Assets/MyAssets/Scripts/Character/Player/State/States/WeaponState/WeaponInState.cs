using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class WeaponInState : PlayerStateBase
    {
        private IBattleFlagger battleFlagger;
        private IEquipment equipment;
        private IPlayerAnimator animator;

        public static readonly string StateKey = "WeaponIn";

        public override string Key => StateKey;
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsMoveTransition(actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotMoveTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            battleFlagger = actor.BattleFlagger;
            equipment = actor.Equipment;
            animator = actor.PlayerAnimator;
        }

        public override void DoStart()
        {
            base.DoStart();
            battleFlagger.SetBattleMode(false);
            equipment.SetInWeapon();
            animator.Animator.SetInteger(animator.Weapon_In_OutName, 1);
            animator.Animator.SetFloat(animator.ToolLevel, 0.0f);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.Weapon_In_OutName, -1);
        }


    }
}
