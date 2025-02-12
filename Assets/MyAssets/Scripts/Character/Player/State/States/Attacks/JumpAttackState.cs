using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * プレイヤーのジャンプ攻撃動作
     */
    [System.Serializable]
    public class JumpAttackState : PlayerStateBase
    {
        private IVelocityComponent      velocity;

        private SwordController         sword;

        [SerializeField]
        private float                   jumpAttackGravityMultiply = 1.5f;

        [SerializeField]
        private float                   startColliderCount = 0.0f;

        [SerializeField]
        private float                   endColliderCount = 1.0f;

        public static readonly string   StateKey = "JumpAttack";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(JumpAttackLandingState.StateKey)) { re.Add(new IsGroundTransition(actor, StateChanger, JumpAttackLandingState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            velocity = actor.Velocity;
            sword = actor.Equipment.HaveWeapon?.GetComponent<SwordController>();
        }

        public override void DoStart()
        {
            base.DoStart();
            sword.SetAttackType(AttackType.Succession,SwordSEType.Succession);
            sword.SetRatioPower(0.75f);
        }

        public override void DoUpdate(float time)
        {
            sword.EnabledCollider(startColliderCount, endColliderCount, true);
            sword.SpinSlash();
            base.DoUpdate(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            velocity.Rigidbody.velocity += Physics.gravity * jumpAttackGravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            sword.NotEnabledCollider();
            sword.SetRatioPower(1.0f);
        }
    }
}
