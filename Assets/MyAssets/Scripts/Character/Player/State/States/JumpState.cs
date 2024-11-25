using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class JumpState : PlayerStateBase
    {
        private IMovement movement;

        private IMoveInputProvider moveInput;

        private IVelocityComponent velocity;

        private IRotation rotation;

        private IPlayerAnimator animator;

        private IGroundCheck groundCheck;

        private IObstacleJudgment cliffJudgment;

        private IDamageContainer damageContainer;

        [SerializeField]
        private float power;
        [SerializeField]
        private float jumpGravityMultiply;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float dashMagnification = 1.5f;

        [SerializeField]
        private float jumpStartCount = 0.25f;
        private Timer jumpStartTimer = new Timer();

        public static readonly string StateKey = "Jump";
        public override string Key => StateKey;
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(LandingState.StateKey)) { re.Add(new IsNotJumpTransition(actor,jumpStartTimer, StateChanger, LandingState.StateKey)); }
            if (StateChanger.IsContain(ClimbState.StateKey)) { re.Add(new IsClimbTransition(actor, StateChanger, ClimbState.StateKey)); }
            if (StateChanger.IsContain(ReadyJumpAttack.StateKey)) { re.Add(new IsReadyJumpAttackTransition(actor, StateChanger, ReadyJumpAttack.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            movement = player.Movement;
            moveInput = player.MoveInput;
            velocity = player.Velocity;
            rotation = player.Rotation;
            animator = player.PlayerAnimator;
            groundCheck = player.GroundCheck;
            cliffJudgment = player.ObstacleJudgment;
            damageContainer = player.DamageContainer;
        }

        public override void DoStart()
        {
            base.DoStart();

            jumpStartTimer.Start(jumpStartCount);

            float p = power;
            if (moveInput.IsMove)
            {
                animator.Animator.SetInteger(animator.JumpTypeName, 1);
            }
            else
            {
                animator.Animator.SetInteger(animator.JumpTypeName, 0);
                p /= 2;
            }

            velocity.Rigidbody.AddForce(Vector3.up * p,ForceMode.Impulse);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            jumpStartTimer.Update(time);

            groundCheck.FallTimeUpdate();
            cliffJudgment.RayCheck();
            rotation.DoUpdate();
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            if (animator.Animator.GetInteger(animator.JumpTypeName) == 1)
            {
                float speed = moveSpeed;
                if (moveInput.Dash > 0)
                {
                    speed *= dashMagnification;
                }
                movement.Move(speed);
            }
            velocity.Rigidbody.velocity += Physics.gravity * jumpGravityMultiply * time;
            rotation.DoFixedUpdate();
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.JumpTypeName, -1);
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
