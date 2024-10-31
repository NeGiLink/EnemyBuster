using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class NormalAttacksState
    {
        [SerializeField]
        private FirstAttackState firstAttackState;
        public FirstAttackState FirstAttackState => firstAttackState;

        [SerializeField]
        private SecondAttackState secondAttackState;
        public SecondAttackState SecondAttackState => secondAttackState;
    }

    public enum NormalAttackState
    {
        None = -1,
        First,
        Second,
        Third
    }

    [System.Serializable]
    public class FirstAttackState : PlayerStateBase
    {
        private IVelocityComponent velocity;
        private IMovement movement;
        private IPlayerAnimator animator;
        private Transform transform;

        [SerializeField]
        private float attacksGravityMultiply;

        [SerializeField]
        private float maxAttackingTime;

        [SerializeField]
        private float maxNormalizedTime;

        [SerializeField]
        private float forwardPower;

        private Vector3 baseTransform;

        public static readonly string StateKey = "FirstAttack";
        public override string Key => StateKey;

        private readonly string currentMotionName = "FirstAttack";
        public override List<IPlayerStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<IPlayerStateTransition<string>> re = new List<IPlayerStateTransition<string>>();
            if (StateChanger.IsContain(SecondAttackState.StateKey)) { re.Add(new IsBurstAttackTransition(currentMotionName,maxAttackingTime,actor, StateChanger, SecondAttackState.StateKey)); }
            if (StateChanger.IsContain(IdleState.StateKey)) { re.Add(new IsNotAttackTransition(actor, StateChanger, IdleState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotAttackToMoveTransition(maxNormalizedTime, actor, StateChanger, MoveState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            velocity = player.Velocity;
            movement = player.Movement;
            animator = player.PlayerAnimator;
            transform = player.gameObject.transform;
        }
        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger(animator.AttacksName, (int)NormalAttackState.First);
            velocity.Rigidbody.velocity = Vector3.zero;
            baseTransform = transform.position;
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            ForwardMove();
            velocity.Rigidbody.velocity += Physics.gravity * attacksGravityMultiply * time;
        }

        private void ForwardMove()
        {
            AnimatorStateInfo aniInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            if(aniInfo.normalizedTime > maxAttackingTime) { return; }
            movement.ForwardMove(baseTransform,forwardPower);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.AttacksName, (int)NormalAttackState.None);
        }
    }
    [System.Serializable]
    public class SecondAttackState : PlayerStateBase
    {
        private IVelocityComponent velocity;
        private IMovement movement;
        private IPlayerAnimator animator;

        private IAttackInputProvider inputTimer;

        private Transform transform;

        [SerializeField]
        private float attacksGravityMultiply;

        [SerializeField]
        private float maxAttackingTime;

        [SerializeField]
        private float maxNormalizedTime;

        [SerializeField]
        private float forwardPower;

        private Vector3 baseTransform;

        public static readonly string StateKey = "SecondAttack";
        public override string Key => StateKey;

        private readonly string currentMotionName = "SecondAttack";
        public override List<IPlayerStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<IPlayerStateTransition<string>> re = new List<IPlayerStateTransition<string>>();
            if (StateChanger.IsContain(ThirdAttackState.StateKey)) { re.Add(new IsBurstAttackTransition(currentMotionName, maxAttackingTime, actor, StateChanger, ThirdAttackState.StateKey)); }
            if (StateChanger.IsContain(IdleState.StateKey)) { re.Add(new IsNotAttackTransition(actor, StateChanger, IdleState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotAttackToMoveTransition(maxNormalizedTime, actor, StateChanger, MoveState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            velocity = player.Velocity;
            animator = player.PlayerAnimator;
            inputTimer = player.AttackInput;
            transform = player.gameObject.transform;
            movement = player.Movement;
        }
        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger(animator.AttacksName, (int)NormalAttackState.Second);
            velocity.Rigidbody.velocity = Vector3.zero;
            baseTransform = transform.position;
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            ForwardMove();
            velocity.Rigidbody.velocity += Physics.gravity * attacksGravityMultiply * time;
        }
        private void ForwardMove()
        {
            AnimatorStateInfo aniInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            if (aniInfo.normalizedTime > maxAttackingTime) { return; }
            movement.ForwardMove(baseTransform, forwardPower);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.AttacksName, (int)NormalAttackState.None);
        }
    }
    [System.Serializable]
    public class ThirdAttackState : PlayerStateBase
    {
        private IVelocityComponent velocity;
        private IMovement movement;
        private IPlayerAnimator animator;
        private Transform transform;
        [SerializeField]
        private float attacksGravityMultiply;

        [SerializeField]
        private float maxAttackingTime;

        [SerializeField]
        private float maxNormalizedTime;

        [SerializeField]
        private float forwardPower;

        private Vector3 baseTransform;


        public static readonly string StateKey = "ThirsAttack";
        public override string Key => StateKey;
        public override List<IPlayerStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<IPlayerStateTransition<string>> re = new List<IPlayerStateTransition<string>>();
            if (StateChanger.IsContain(IdleState.StateKey)) { re.Add(new IsNotAttackTransition(actor, StateChanger, IdleState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotAttackToMoveTransition(maxNormalizedTime, actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(FirstAttackState.StateKey)) { re.Add(new IsLoopFirstAttackTransition(maxNormalizedTime,actor, StateChanger, FirstAttackState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            velocity = player.Velocity;
            animator = player.PlayerAnimator;
            movement = player.Movement;
            transform = player.gameObject.transform;
        }
        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger(animator.AttacksName, (int)NormalAttackState.Third);
            velocity.Rigidbody.velocity = Vector3.zero;
            baseTransform = transform.position;
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            ForwardMove();
            velocity.Rigidbody.velocity += Physics.gravity * attacksGravityMultiply * time;
        }

        private void ForwardMove()
        {
            AnimatorStateInfo aniInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            if (aniInfo.normalizedTime > maxAttackingTime) { return; }
            movement.ForwardMove(baseTransform, forwardPower);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.AttacksName, (int)NormalAttackState.None);
        }
    }
}
