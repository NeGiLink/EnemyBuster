using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public enum NormalAttackState
    {
        None = -1,
        First,
        Second,
        SecondDer,
        Third,
        JumpAttack,
        Counter,
        ChargeAttack
    }
    /// <summary>
    /// プレイヤーの通常時の攻撃をまとめたクラス
    /// 現在は三段攻撃
    /// </summary>
    [System.Serializable]
    public class FirstAttackState : PlayerStateBase
    {
        private IVelocityComponent      velocity;

        private IMovement               movement;
        
        private IPlayerAnimator         animator;
        
        private Transform               transform;

        private IFieldOfView            fieldOfView;

        private SwordController         sword;

        [SerializeField]
        private float                   attacksGravityMultiply;

        [SerializeField]
        private float                   secondVer1ToTransitionTime;
        [SerializeField]
        private float                   secondVer2ToTransitionTime;

        [SerializeField]
        private float                   maxNormalizedTime;

        [SerializeField]
        private float                   forwardPower;

        [SerializeField]
        private float                   startColliderCount = 0.15f;

        [SerializeField]
        private float                   endColliderCount = 0.6f;

        private Vector3                 baseTransform;

        private readonly string         currentMotionName = "FirstAttack";

        public static readonly string   StateKey = "FirstAttack";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(SecondDerivationAttack2State.StateKey)) { re.Add(new IsSecondAttackTransition(currentMotionName, secondVer2ToTransitionTime,maxNormalizedTime, actor, StateChanger, SecondDerivationAttack2State.StateKey)); }
            if (StateChanger.IsContain(SecondAttackState.StateKey)) { re.Add(new IsBurstAttackTransition(currentMotionName, secondVer1ToTransitionTime, actor, StateChanger, SecondAttackState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotAttackTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotAttackToMoveTransition(maxNormalizedTime, actor, StateChanger, MoveState.StateKey)); }
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
            sword.SetAttackType(AttackType.Normal, SwordSEType.Slash1);
            sword.Slash();
            animator.Animator.SetInteger(animator.AttackAnimationID, (int)NormalAttackState.First);
            velocity.Rigidbody.velocity = Vector3.zero;
            baseTransform = transform.position;

            if (fieldOfView.TargetObject != null)
            {
                Vector3 target = fieldOfView.TargetObject.transform.position;
                target.y = transform.position.y;
                transform.LookAt(target);
            }
        }

        public override void DoUpdate(float time)
        {
            sword.EnabledCollider(startColliderCount, endColliderCount, false);
            base.DoUpdate(time);
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
            if(aniInfo.normalizedTime > secondVer1ToTransitionTime) { return; }
            movement.ForwardLerpMove(baseTransform,forwardPower);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.AttackAnimationID, (int)NormalAttackState.None);
            sword.NotEnabledCollider();
        }
    }
    /*
     * プレイヤーの三段攻撃の二段目攻撃状態
     */
    [System.Serializable]
    public class SecondAttackState : PlayerStateBase
    {
        private IVelocityComponent          velocity;

        private IMovement                   movement;

        private IPlayerAnimator             animator;

        private Transform                   transform;

        private IFieldOfView                fieldOfView;

        private SwordController             sword;

        [SerializeField]
        private float                       attacksGravityMultiply;

        [SerializeField]
        private float                       maxAttackingTime;

        [SerializeField]
        private float                       maxNormalizedTime;

        [SerializeField]
        private float                       forwardPower;

        [SerializeField]
        private float                       startColliderCount = 0.15f;

        [SerializeField]
        private float                       endColliderCount = 0.6f;

        private Vector3                     baseTransform;

        private readonly string             currentMotionName = "SecondAttack";
        
        public static readonly string       StateKey = "SecondAttack";
        public override string              Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(ThirdAttackState.StateKey)) { re.Add(new IsBurstAttackTransition(currentMotionName, maxAttackingTime, actor, StateChanger, ThirdAttackState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotAttackTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotAttackToMoveTransition(maxNormalizedTime, actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            velocity = actor.Velocity;
            animator = actor.PlayerAnimator;
            transform = actor.gameObject.transform;
            movement = actor.Movement;
            fieldOfView = actor.FieldOfView;
            sword = actor.Equipment.HaveWeapon.GetComponent<SwordController>();
        }
        public override void DoStart()
        {
            base.DoStart();
            sword.SetAttackType(AttackType.Normal,SwordSEType.Slash1);
            sword.Slash();
            sword.SetRatioPower(1.2f);
            animator.Animator.SetInteger(animator.AttackAnimationID, (int)NormalAttackState.Second);
            velocity.Rigidbody.velocity = Vector3.zero;
            baseTransform = transform.position;

            if (fieldOfView.TargetObject != null)
            {
                Vector3 target = fieldOfView.TargetObject.transform.position;
                target.y = transform.position.y;
                transform.LookAt(target);
            }
        }

        public override void DoUpdate(float time)
        {
            sword.EnabledCollider(startColliderCount, endColliderCount, false);
            base.DoUpdate(time);
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
            animator.Animator.SetInteger(animator.AttackAnimationID, (int)NormalAttackState.None);
            sword.NotEnabledCollider();
            sword.SetRatioPower(1.0f);
        }


    }
    /*
     * プレイヤーの三段攻撃の派生二段攻撃状態
     */
    [System.Serializable]
    public class SecondDerivationAttack2State : PlayerStateBase
    {
        private IVelocityComponent          velocity;

        private IMovement                   movement;
        
        private IPlayerAnimator             animator;

        private Transform                   transform;

        private IFieldOfView                fieldOfView;

        private SwordController             sword;

        private SEHandler                   seHandler;

        private Timer                       jumpStartTimer = new Timer();

        [SerializeField]
        private float                       attacksGravityMultiply;

        [SerializeField]
        private float                       maxAttackingTime;

        [SerializeField]
        private float                       forwardPower;

        private Vector3                     baseTransform;

        [SerializeField]
        private float                       power;

        [SerializeField]
        private float                       jumpStartCount = 0.25f;

        [SerializeField]
        private float                       startColliderCount = 0.1f;

        [SerializeField]
        private float                       endColliderCount = 0.5f;

        private readonly string             currentMotionName2 = "SecondDerivationAttack2End";

        public static readonly string       StateKey = "SecondDerivationAttack2";
        public override string              Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(FallState.StateKey)) { re.Add(new IsJumpToFallTransition(actor, StateChanger, FallState.StateKey)); }
            if (StateChanger.IsContain(LandingState.StateKey)) { re.Add(new IsNotJumpTransition(actor, jumpStartTimer, StateChanger, LandingState.StateKey)); }
            if (StateChanger.IsContain(ReadyJumpAttack.StateKey)) { re.Add(new IsSecondAttackVer2ToReadyJumpAttackTransition(actor, currentMotionName2, StateChanger, ReadyJumpAttack.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            velocity = actor.Velocity;
            animator = actor.PlayerAnimator;
            transform = actor.gameObject.transform;
            movement = actor.Movement;
            fieldOfView = actor.FieldOfView;
            sword = actor.Equipment.HaveWeapon.GetComponent<SwordController>();
            seHandler = actor.SEHandler;
        }
        public override void DoStart()
        {
            base.DoStart();

            seHandler.Play((int)PlayerSETag.Jump);
            sword.SetAttackType(AttackType.Normal, SwordSEType.Slash1);
            sword.Slash();
            sword.SetRatioPower(1.2f);
            animator.Animator.SetInteger(animator.AttackAnimationID, (int)NormalAttackState.SecondDer);
            velocity.Rigidbody.velocity = Vector3.zero;
            baseTransform = transform.position;

            jumpStartTimer.Start(jumpStartCount);
            velocity.Rigidbody.AddForce(Vector3.up * power, ForceMode.Impulse);

            if (fieldOfView.TargetObject != null)
            {
                Vector3 target = fieldOfView.TargetObject.transform.position;
                target.y = transform.position.y;
                transform.LookAt(target);
            }
        }

        public override void DoUpdate(float time)
        {
            sword.EnabledCollider(startColliderCount, endColliderCount, false);
            base.DoUpdate(time);
            jumpStartTimer.Update(time);
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
            baseTransform.y = transform.position.y;
            movement.ForwardLerpMove(baseTransform, forwardPower);
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.AttackAnimationID, (int)NormalAttackState.None);
            sword.NotEnabledCollider();
            sword.SetRatioPower(1.0f);
        }


    }
    /*
     * プレイヤーの三段攻撃の三段攻撃状態
     */
    [System.Serializable]
    public class ThirdAttackState : PlayerStateBase
    {
        private IVelocityComponent          velocity;

        private IMovement                   movement;
        
        private IPlayerAnimator             animator;
        
        private Transform                   transform;

        private IFieldOfView                fieldOfView;

        private SwordController             sword;

        [SerializeField]
        private float                       attacksGravityMultiply;

        [SerializeField]
        private float                       maxAttackingTime;

        [SerializeField]
        private float                       maxNormalizedTime;

        [SerializeField]
        private float                       forwardPower;

        [SerializeField]
        private float                       startColliderCount = 0.1f;

        [SerializeField]
        private float                       endColliderCount = 0.6f;

        private Vector3                     baseTransform;


        public static readonly string       StateKey = "ThirsAttack";
        public override string              Key => StateKey;
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotAttackTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(FirstAttackState.StateKey)) { re.Add(new IsLoopFirstAttackTransition(maxNormalizedTime,actor, StateChanger, FirstAttackState.StateKey)); }
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsNotAttackToMoveTransition(maxNormalizedTime, actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            velocity = actor.Velocity;
            animator = actor.PlayerAnimator;
            movement = actor.Movement;
            transform = actor.gameObject.transform;
            fieldOfView = actor.FieldOfView;
            sword = actor.Equipment.HaveWeapon.GetComponent<SwordController>();
        }
        public override void DoStart()
        {
            base.DoStart();
            sword.SetAttackType(AttackType.Normal, SwordSEType.Slash1);
            sword.Slash();
            sword.SetRatioPower(1.4f);
            animator.Animator.SetInteger(animator.AttackAnimationID, (int)NormalAttackState.Third);
            velocity.Rigidbody.velocity = Vector3.zero;
            baseTransform = transform.position;

            if (fieldOfView.TargetObject != null)
            {
                Vector3 target = fieldOfView.TargetObject.transform.position;
                target.y = transform.position.y;
                transform.LookAt(target);
            }
        }

        public override void DoUpdate(float time)
        {
            sword.EnabledCollider(startColliderCount, endColliderCount,false);
            base.DoUpdate(time);
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
            animator.Animator.SetInteger(animator.AttackAnimationID, (int)NormalAttackState.None);
            sword.NotEnabledCollider();
            sword.SetRatioPower(1.0f);
        }


    }
}
