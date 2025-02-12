using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * プレイヤーのジャンプ攻撃準備状態
     */
    [System.Serializable]
    public class ReadyJumpAttack : PlayerStateBase
    {
        private IPlayerStauts           stauts;

        private IVelocityComponent      velocity;

        private IEquipment              equipment;

        private IBattleFlagger          battleFlagger;

        private IPlayerAnimator         animator;

        public static readonly string   StateKey = "ReadyJumpAttack";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(JumpAttackState.StateKey)) { re.Add(new IsJumpAttackTransition(actor, StateChanger, JumpAttackState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup actor)
        {
            stauts = actor.Stauts;
            base.DoSetup(actor);
            velocity = actor.Velocity;
            equipment = actor.Equipment;
            animator = actor.PlayerAnimator;
            battleFlagger = actor.BattleFlagger;
        }

        public override void DoStart()
        {
            base.DoStart();
            equipment.SetOutWeapon();
            battleFlagger.SetBattleMode(true);

            animator.Animator.SetInteger(animator.AttackAnimationID, (int)NormalAttackState.JumpAttack);
            animator.Animator.SetFloat(animator.ToolLevelAnimationID, 1.0f);

            velocity.Rigidbody.velocity = Vector3.zero;
            velocity.Rigidbody.useGravity = false;

            stauts.DecreaseSP(stauts.SpinAttackUseSP);
        }

        public override void DoExit()
        {
            base.DoExit();
            velocity.Rigidbody.useGravity = true;
        }
    }
}
