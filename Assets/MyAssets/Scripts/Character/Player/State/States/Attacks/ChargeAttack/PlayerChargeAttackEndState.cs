using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class PlayerChargeAttackEndState : PlayerStateBase
    {
        private IVelocityComponent velocity;
        private IMovement movement;
        private IPlayerAnimator animator;
        private Transform transform;

        private IFieldOfView fieldOfView;

        private SwordController sword;

        [SerializeField]
        private float attacksGravityMultiply;

        [SerializeField]
        private float secondVer1ToTransitionTime;
        [SerializeField]
        private float secondVer2ToTransitionTime;

        [SerializeField]
        private float maxNormalizedTime;

        [SerializeField]
        private float forwardPower;

        [SerializeField]
        private float moveCount;

        private Vector3 baseTransform;

        public static readonly string StateKey = "ChargeAttackEnd";
        public override string Key => StateKey;

        private Timer moveTimer = new Timer();

        private readonly string currentMotionName = "ChargeAttackEnd";

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
            sword = actor.Equipment.HaveWeapon.GetComponent<SwordController>();
        }
        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger("ChargeAttack", 3);
            velocity.Rigidbody.velocity = Vector3.zero;
            baseTransform = transform.position;

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
            animator.Animator.SetInteger(animator.AttacksName, -1);
            animator.Animator.SetInteger("ChargeAttack", -1);
        }
    }
}
