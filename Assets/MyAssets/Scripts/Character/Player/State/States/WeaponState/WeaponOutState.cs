using System.Collections.Generic;

namespace MyAssets
{
    /*
     * プレイヤーの武器取り出し状態
     */
    [System.Serializable]
    public class WeaponOutState : PlayerStateBase
    {
        private IBattleFlagger              battleFlagger;

        private IEquipment                  equipment;
        
        private IPlayerAnimator             animator;

        public static readonly string       StateKey = "WeaponOut";

        public override string              Key => StateKey;
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(FirstAttackState.StateKey)) { re.Add(new IsWeaponOutFirstAttackTransition(actor, StateChanger, FirstAttackState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
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
            battleFlagger.SetBattleMode(true);
            equipment.SetOutWeapon();
            animator.Animator.SetInteger(animator.Weapon_In_OutAnimationID, 0);
            animator.Animator.SetFloat(animator.ToolLevelAnimationID, 1.0f);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.Weapon_In_OutAnimationID, -1);
        }

    }
}
