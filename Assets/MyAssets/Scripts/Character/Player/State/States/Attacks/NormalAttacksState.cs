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

        private IDamageContainer damageContainer;

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
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(SecondAttackState.StateKey)) { re.Add(new IsBurstAttackTransition(currentMotionName,maxAttackingTime,actor, StateChanger, SecondAttackState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotAttackTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotAttackToMoveTransition(maxNormalizedTime, actor, StateChanger, MoveState.StateKey)); }
            //if (StateChanger.IsContain(DamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, DamageState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            velocity = player.Velocity;
            movement = player.Movement;
            animator = player.PlayerAnimator;
            transform = player.gameObject.transform;
            damageContainer = player.DamageContainer;
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
            movement.ForwardLerpMove(baseTransform,forwardPower);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.AttacksName, (int)NormalAttackState.None);
        }

        public override void DoTriggerEnter(Collider collider)
        {
            base.DoTriggerEnter(collider);
            AttackObject data = collider.GetComponent<AttackObject>();
            if (data == null) { return; }
            //TODO : 状態を変更せずにダメージを与える処理を追加
            
            damageContainer.SetAttackType(AttackType.Small);
            damageContainer.SetData(data.Power);
            damageContainer.SetAttacker(collider.transform);
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

        private IDamageContainer damageContainer;

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
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(ThirdAttackState.StateKey)) { re.Add(new IsBurstAttackTransition(currentMotionName, maxAttackingTime, actor, StateChanger, ThirdAttackState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotAttackTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotAttackToMoveTransition(maxNormalizedTime, actor, StateChanger, MoveState.StateKey)); }
            //if (StateChanger.IsContain(DamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, DamageState.StateKey)); }
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
            damageContainer = player.DamageContainer;
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
            movement.ForwardLerpMove(baseTransform, forwardPower);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.AttacksName, (int)NormalAttackState.None);
        }

        public override void DoTriggerEnter(Collider collider)
        {
            base.DoTriggerEnter(collider);
            AttackObject data = collider.GetComponent<AttackObject>();
            if (data == null) { return; }
            damageContainer.SetAttackType(AttackType.Small);
            damageContainer.SetData(data.Power);
            damageContainer.SetAttacker(collider.transform);
        }
    }
    [System.Serializable]
    public class ThirdAttackState : PlayerStateBase
    {
        private IVelocityComponent velocity;
        private IMovement movement;
        private IPlayerAnimator animator;
        private Transform transform;
        private IDamageContainer damageContainer;

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
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotAttackTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotAttackToMoveTransition(maxNormalizedTime, actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(FirstAttackState.StateKey)) { re.Add(new IsLoopFirstAttackTransition(maxNormalizedTime,actor, StateChanger, FirstAttackState.StateKey)); }
            //if (StateChanger.IsContain(DamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, DamageState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            velocity = player.Velocity;
            animator = player.PlayerAnimator;
            movement = player.Movement;
            transform = player.gameObject.transform;
            damageContainer = player.DamageContainer;
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
            movement.ForwardLerpMove(baseTransform, forwardPower);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.AttacksName, (int)NormalAttackState.None);
        }
        public override void DoTriggerEnter(Collider collider)
        {
            base.DoTriggerEnter(collider);
            AttackObject data = collider.GetComponent<AttackObject>();
            if (data == null) { return; }
            damageContainer.SetAttackType(AttackType.Small);
            damageContainer.SetData(data.Power);
            damageContainer.SetAttacker(collider.transform);
        }

    }
}
