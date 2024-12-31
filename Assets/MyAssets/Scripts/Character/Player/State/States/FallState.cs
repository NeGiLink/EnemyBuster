using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class FallState : PlayerStateBase
    {
        private IMovement movement;
        private IVelocityComponent velocity;
        private IMoveInputProvider input;
        private IRotation rotation;
        private IObstacleJudgment cliffJudgment;
        private IPlayerAnimator animator;
        private IGroundCheck groundCheck;
        private IDamageContainer damageContainer;

        [SerializeField]
        float moveSpeed;
        [SerializeField]
        float fallGravityMultiply;

        public static readonly string StateKey = "Fall";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(LandingState.StateKey)) { re.Add(new IsGroundTransition(actor, StateChanger, LandingState.StateKey)); }
            if (StateChanger.IsContain(ClimbState.StateKey)) { re.Add(new IsClimbTransition(actor, StateChanger, ClimbState.StateKey)); }
            if (StateChanger.IsContain(ReadyJumpAttack.StateKey)) { re.Add(new IsReadyJumpAttackTransition(actor, StateChanger, ReadyJumpAttack.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }


        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            movement = player.Movement;
            velocity = player.Velocity;
            rotation = player.Rotation;
            cliffJudgment = player.ObstacleJudgment;
            animator = player.PlayerAnimator;
            groundCheck = player.GroundCheck;
            damageContainer = player.DamageContainer;
        }
        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger(animator.FallName, 1);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            groundCheck.FallTimeUpdate();
            cliffJudgment.RayCheck();
            rotation.DoUpdate();
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(moveSpeed);
            velocity.Rigidbody.velocity += Physics.gravity * fallGravityMultiply * time;
            rotation.DoFixedUpdate();
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.FallName, 0);
        }

        public override void DoTriggerEnter(GameObject thisObject,Collider collider)
        {
            base.DoTriggerEnter(thisObject,collider);
            AttackObject data = collider.GetComponent<AttackObject>();
            if (data == null) { return; }
            damageContainer.GiveYouDamage(data.Power, data.Type, collider.transform);
        }
    }
}
