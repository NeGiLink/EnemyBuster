using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class RollingState : PlayerStateBase
    {
        private IMoveInputProvider input;

        private IVelocityComponent velocity;

        private ICharacterMovement movement;

        private IRotation rotation;

        private IPlayerAnimator animator;

        private Transform thisTransform;

        [SerializeField]
        private float rollingSpeed = 4.0f;
        [SerializeField]
        private float rollingGravityMultiply;

        [SerializeField]
        private Vector3 direction;

        public static readonly string StateKey = "Rolling";

        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BattleMoveState.StateKey)) { re.Add(new IsNotRollingTransition(actor, StateChanger, BattleMoveState.StateKey)); }
            if (StateChanger.IsContain(BattleIdleState.StateKey)) { re.Add(new IsNotRollingTransition(actor, StateChanger, BattleIdleState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            input = player.gameObject.GetComponent<IMoveInputProvider>();
            velocity = player.Velocity;
            movement = player.Movement;
            rotation = player.Rotation;
            animator = player.PlayerAnimator;
            thisTransform = player.gameObject.transform;
        }


        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger("Rolling", 0);
            direction = Vector3.zero;
            if(input.Horizontal > 0)
            {
                direction += thisTransform.right;
            }
            else if(input.Horizontal < 0)
            {
                direction += -thisTransform.right;
            }

            if(input.Vertical > 0)
            {
                direction += thisTransform.forward;
            }
            else if(input.Vertical < 0)
            {
                direction += -thisTransform.forward;
            }

            animator.Animator.SetFloat(animator.VelocityX, input.Horizontal, 0.1f, Time.deltaTime);
            animator.Animator.SetFloat(animator.VelocityZ, input.Vertical, 0.1f, Time.deltaTime);

            velocity.CurrentVelocity = Vector3.zero;
        }
        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Move(rollingSpeed);
            //rotation.DoLookOnTarget(direction);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger("Rolling", -1);
            velocity.CurrentVelocity = Vector3.zero;
        }
    }
}
