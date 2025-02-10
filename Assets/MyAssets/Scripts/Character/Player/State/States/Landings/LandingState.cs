using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class LandingState : PlayerStateBase
    {
        private IPlayerStauts stauts;

        private Timer playerTimer = new Timer();

        private IMovement movement;

        private IVelocityComponent velocity;

        private IPlayerAnimator animator;

        private IGroundCheck groundCheck;

        private IDamageContainer damageContainer;

        [SerializeField]
        private float moveSpeedRatio;

        [SerializeField]
        private float LandingActionTime = 0.1f;

        public static readonly string StateKey = "Landing";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsTimerAndMoveTransition(actor, playerTimer, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsTimerAndNotMoveTransition(actor, playerTimer, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            stauts = actor.Stauts;
            movement = actor.Movement;
            velocity = actor.Velocity;
            animator = actor.PlayerAnimator;
            groundCheck = actor.GroundCheck;
            damageContainer = actor.DamageContainer;
        }

        public override void DoStart()
        {
            base.DoStart();

            playerTimer.Start(LandingActionTime);

            velocity.Rigidbody.velocity = Vector3.zero;

            animator.Animator.SetInteger(animator.LandName, 0);

            LandingDamageChack();
        }

        private void LandingDamageChack()
        {
            if(groundCheck.FallCount < 1) { return; }
            float damage = groundCheck.FallCount * 10f;
            damageContainer.GiveDamage((int)damage,0, DamageType.None, null,CharacterType.Null);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            playerTimer.Update(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(stauts.BaseSpeed * moveSpeedRatio);
        }

        public override void DoExit()
        {
            base.DoExit();
            playerTimer.End();
            animator.Animator.SetInteger(animator.LandName, -1);
        }
    }
}
