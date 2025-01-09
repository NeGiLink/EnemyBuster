using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class LandingState : PlayerStateBase
    {
        private Timer playerTimer = new Timer();

        private IMovement movement;

        private IVelocityComponent velocity;

        private IPlayerAnimator animator;

        private IGroundCheck groundCheck;

        private IDamageContainer damageContainer;

        [SerializeField]
        private float moveSpeed;

        public static readonly string StateKey = "Landing";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup player)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsTimerAndMoveTransition(player, playerTimer, StateChanger, MoveState.StateKey)); }
            else if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsTimerAndMoveTransition(player, playerTimer, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsTimerAndNotMoveTransition(player, playerTimer, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(player, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(player, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
            animator = actor.PlayerAnimator;
            groundCheck = actor.GroundCheck;
            damageContainer = actor.DamageContainer;
        }

        public override void DoStart()
        {
            base.DoStart();

            const float LandingActionTime = 0.1f;
            playerTimer.Start(LandingActionTime);

            velocity.Rigidbody.velocity = Vector3.zero;

            animator.Animator.SetInteger(animator.LandName, 0);

            LandingDamageChack();
        }

        private void LandingDamageChack()
        {
            if(groundCheck.FallCount < 1) { return; }
            float damage = groundCheck.FallCount * 10f;
            damageContainer.GiveYouDamage((int)damage, DamageType.None, null,CharacterType.Null);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            playerTimer.Update(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(moveSpeed);
        }

        public override void DoExit()
        {
            base.DoExit();
            playerTimer.End();
            animator.Animator.SetInteger(animator.LandName, -1);
        }
    }
}
