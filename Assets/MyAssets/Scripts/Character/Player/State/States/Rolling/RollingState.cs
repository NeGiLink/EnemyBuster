using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class RollingState : PlayerStateBase
    {
        private IPlayerStauts stauts;

        private IMoveInputProvider input;

        private IVelocityComponent velocity;

        private IMovement movement;

        private IRotation rotation;

        private IPlayerAnimator animator;

        private SEHandler seHandler;

        private Timer timer = new Timer();

        [SerializeField]
        private float rollingSpeedRatio = 1.5f;
        [SerializeField]
        private float rollingGravityMultiply;

        [SerializeField]
        private int rollingSp;

        public static readonly string StateKey = "Rolling";

        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BattleMoveState.StateKey)) { re.Add(new IsNotRollingTransition(actor,timer, StateChanger, BattleMoveState.StateKey)); }
            if (StateChanger.IsContain(BattleIdleState.StateKey)) { re.Add(new IsNotRollingTransition(actor,timer, StateChanger, BattleIdleState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            stauts = player.Stauts;
            input = player.gameObject.GetComponent<IMoveInputProvider>();
            velocity = player.Velocity;
            movement = player.Movement;
            rotation = player.Rotation;
            animator = player.PlayerAnimator;
            seHandler = player.SEHandler;
        }


        public override void DoStart()
        {
            base.DoStart();

            seHandler.Play((int)PlayerSETag.Avoid);

            animator.Animator.SetInteger("Rolling", 0);

            stauts.DecreaseSP(rollingSp);

            animator.Animator.SetFloat(animator.VelocityX, input.Horizontal, 0.1f, Time.deltaTime);
            animator.Animator.SetFloat(animator.VelocityZ, input.Vertical, 0.1f, Time.deltaTime);

            velocity.CurrentVelocity = Vector3.zero;

            timer.Start(0.2f);

            rotation.DoUpdate();
        }

        public override void DoUpdate(float time)
        {
            timer.Update(time);
            rotation.DoFixedUpdate();
            base.DoUpdate(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(stauts.BaseSpeed * rollingSpeedRatio);
            velocity.Rigidbody.velocity += Physics.gravity * rollingGravityMultiply * time;
            rotation.DoFixedUpdate();
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger("Rolling", -1);
            velocity.CurrentVelocity = Vector3.zero;
        }
    }
}
