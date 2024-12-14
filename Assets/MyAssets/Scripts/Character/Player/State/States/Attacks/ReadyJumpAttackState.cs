using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class ReadyJumpAttack : PlayerStateBase
    {
        private IVelocityComponent velocity;

        private IEquipment equipment;

        private IChangingState changingState;

        private IPlayerAnimator animator;

        private IDamageContainer damageContainer;

        public static readonly string StateKey = "ReadyJumpAttack";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(JumpAttackState.StateKey)) { re.Add(new IsJumpAttackTransition(actor, StateChanger, JumpAttackState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            velocity = player.Velocity;
            equipment = player.Equipment;
            animator = player.PlayerAnimator;
            changingState = player.ChangingState;
            damageContainer = player.DamageContainer;
        }

        public override void DoStart()
        {
            base.DoStart();
            equipment.SetOutWeapon();
            changingState.SetBattleMode(true);

            animator.Animator.SetInteger(animator.AttacksName, (int)NormalAttackState.JumpAttack);
            animator.Animator.SetFloat(animator.ToolLevel, 1.0f);

            velocity.Rigidbody.velocity = Vector3.zero;
            velocity.Rigidbody.useGravity = false;
        }

        public override void DoExit()
        {
            base.DoExit();
            velocity.Rigidbody.useGravity = true;
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
