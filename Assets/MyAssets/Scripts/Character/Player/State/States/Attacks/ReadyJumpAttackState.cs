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

        public static readonly string StateKey = "ReadyJumpAttack";
        public override string Key => StateKey;

        public override List<IPlayerStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<IPlayerStateTransition<string>> re = new List<IPlayerStateTransition<string>>();
            if (StateChanger.IsContain(JumpAttackState.StateKey)) { re.Add(new IsJumpAttackTransition(actor, StateChanger, JumpAttackState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            velocity = player.Velocity;
            equipment = player.Equipment;
            animator = player.PlayerAnimator;
            changingState = player.ChangingState;
        }

        public override void DoStart()
        {
            base.DoStart();
            equipment.SetOutWeapon();
            changingState.SetBattleMode(true);

            animator.Animator.SetInteger(animator.AttacksName, 3);
            animator.Animator.SetFloat(animator.ToolLevel, 1.0f);

            velocity.Rigidbody.velocity = Vector3.zero;
            velocity.Rigidbody.useGravity = false;
        }

        public override void DoExit()
        {
            base.DoExit();
            velocity.Rigidbody.useGravity = true;
        }
    }
}
