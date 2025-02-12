using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * —­‚ßUŒ‚I—¹ó‘Ô
     */
    [System.Serializable]
    public class PlayerChargeAttackEndState : PlayerStateBase
    {
        private IVelocityComponent          velocity;

        private IMovement                   movement;
        
        private IPlayerAnimator             animator;
        
        private Transform                   transform;
        
        private IFieldOfView                fieldOfView;

        private Timer                       moveTimer = new Timer();

        private readonly string             currentMotionName = "ChargeAttackEnd";

        [SerializeField]
        private float                       attacksGravityMultiply;

        [SerializeField]
        private float                       moveCount;

        public static readonly string       StateKey = "ChargeAttackEnd";
        public override string              Key => StateKey;



        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsPlayerEndMotionTransition(actor, currentMotionName, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsTimerAndMoveTransition(actor, moveTimer, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            velocity = actor.Velocity;
            movement = actor.Movement;
            animator = actor.PlayerAnimator;
            transform = actor.gameObject.transform;
            fieldOfView = actor.FieldOfView;
        }
        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger(animator.ChargeAttackAnimationID, 3);
            velocity.Rigidbody.velocity = Vector3.zero;

            moveTimer.Start(moveCount);

            if (fieldOfView.TargetObject != null)
            {
                Vector3 target = fieldOfView.TargetObject.transform.position;
                target.y = transform.position.y;
                transform.LookAt(target);
            }
        }

        public override void DoUpdate(float time)
        {
            moveTimer.Update(time);
            base.DoUpdate(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Stop();
            velocity.Rigidbody.velocity += Physics.gravity * attacksGravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.AttackAnimationID, -1);
            animator.Animator.SetInteger(animator.ChargeAttackAnimationID, -1);
        }
    }
}
