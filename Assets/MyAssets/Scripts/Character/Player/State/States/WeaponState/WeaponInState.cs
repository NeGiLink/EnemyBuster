using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class WeaponInState : PlayerStateBase
    {
        private IChangingState changingState;
        private IEquipment equipment;
        private IPlayerAnimator animator;

        private IDamageContainer damageContainer;

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

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            changingState = player.ChangingState;
            equipment = player.Equipment;
            animator = player.PlayerAnimator;
            damageContainer = player.DamageContainer;
        }

        public override void DoStart()
        {
            base.DoStart();
            changingState.SetBattleMode(false);
            equipment.SetInWeapon();
            animator.Animator.SetInteger(animator.Weapon_In_OutName, 1);
            animator.Animator.SetFloat(animator.ToolLevel, 0.0f);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            if (animInfo.normalizedTime > 0.5f && animInfo.normalizedTime < 0.55f)
            {
                //equipment.SetInWeapon();
            }
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.Weapon_In_OutName, -1);
        }

        public override void DoTriggerEnter(GameObject thisObject,Collider collider)
        {
            base.DoTriggerEnter(thisObject,collider);
            AttackObject data = collider.GetComponent<AttackObject>();
            if (data == null) { return; }
            damageContainer.SetAttackerData(data.Power, data.Type, collider.transform);
        }
    }
}
