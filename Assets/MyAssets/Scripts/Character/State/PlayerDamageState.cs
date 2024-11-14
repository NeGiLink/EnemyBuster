using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class PlayerDamageState : PlayerStateBase
    {
        private Transform thisTransform;

        private IVelocityComponent velocity;

        private IMovement movement;

        private IPlayerAnimator  animator;

        private IDamageContainer damageContainer;

        private IDamagement damageMove;

        private IGroundCheck groundCheck;

        private Timer damageTimer = new Timer();

        private Timer invincibilityTimer = new Timer();

        [SerializeField]
        private float knockBack = 5.0f;

        [SerializeField]
        private float decreaseForce = 0.9f;

        [SerializeField]
        private float damageGravityMultiply = 2.0f;

        public static readonly string StateKey = "Damage";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(GetUpState.StateKey)) { re.Add(new IsPlayerDamageToGetUpTransition(actor, damageTimer, StateChanger, GetUpState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotPlayerDamageToTransition(actor,damageTimer, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotPlayerDamageToTransition(actor, damageTimer, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(BattleIdleState.StateKey)) { re.Add(new IsNotPlayerDamageToBattleTransition(actor, damageTimer, StateChanger, BattleIdleState.StateKey)); }
            if (StateChanger.IsContain(BattleMoveState.StateKey)) { re.Add(new IsNotPlayerDamageToBattleTransition(actor, damageTimer, StateChanger, BattleMoveState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            thisTransform = player.gameObject.transform;
            velocity = player.Velocity;
            movement = player.Movement;
            animator = player.PlayerAnimator;
            groundCheck = player.GroundCheck;
            damageContainer = player.DamageContainer;
            damageMove = player.Damagement;
        }

        public override void DoStart()
        {
            base.DoStart();
            velocity.Rigidbody.velocity = Vector3.zero;

            AttackType type = damageContainer.AttackType;
            int damageType = -1;
            if (groundCheck.Landing)
            {
                switch (type)
                {
                    case AttackType.Small:
                        damageType = -1;
                        Debug.Log("���_���[�W�ʂ���");
                        break;
                    case AttackType.Middle:
                        damageType = 0;
                        damageMove.AddForceMove(thisTransform.position, damageContainer.Attacker.position, knockBack * 1.5f);
                        damageTimer.Start(1.0f);
                        break;
                    case AttackType.Big:
                        damageType = 1;
                        damageMove.AddForceMove(thisTransform.position, damageContainer.Attacker.position, knockBack * 4.0f);
                        damageTimer.Start(1.5f);
                        break;
                }
            }
            else
            {
                damageType = 1;
                damageMove.AddForceMove(thisTransform.position, damageContainer.Attacker.position, knockBack * 2.0f);
                damageTimer.Start(1.5f);
            }
            animator.Animator.SetInteger("Impact", damageType);

        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            damageTimer.Update(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            //AddForce�ŗ^������������������
            Vector3 v = velocity.Rigidbody.velocity;
            velocity.Rigidbody.velocity = v * decreaseForce;
            velocity.Rigidbody.velocity += Physics.gravity * damageGravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            damageContainer.SetData(0);
            damageContainer.SetAttacker(null);
            damageContainer.SetAttackType(AttackType.None);
            animator.Animator.SetInteger("Impact", -1);
        }
    }
}
