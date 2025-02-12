using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * —­‚ßUŒ‚ó‘Ô
     */
    [System.Serializable]
    public class PlayerChargeAttackState : PlayerStateBase
    {
        private IPlayerStauts           stauts;

        private IVelocityComponent      velocity;

        private IMovement               movement;
        
        private IPlayerAnimator         animator;
        
        private Transform               transform;

        private IFieldOfView            fieldOfView;

        private PlayerEffectController  effectController;

        private SwordController         sword;

        private Vector3                 baseTransform;

        [SerializeField]
        private float                   attacksGravityMultiply;

        [SerializeField]
        private float                   moveTime;

        [SerializeField]
        private float                   forwardPower;

        [SerializeField]
        private float                   startColliderCount = 0.5f;

        [SerializeField]
        private float                   endColliderCount = 0.7f;


        public static readonly string   StateKey = "ChargeAttack";
        public override string          Key => StateKey;
        private readonly string         currentMotionName = "ChargeAttack";

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();

            if (StateChanger.IsContain(PlayerChargeAttackEndState.StateKey)) { re.Add(new IsPlayerEndMotionTransition(actor,currentMotionName, StateChanger, PlayerChargeAttackEndState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup actor)
        {
            stauts = actor.Stauts;
            base.DoSetup(actor);
            velocity = actor.Velocity;
            movement = actor.Movement;
            animator = actor.PlayerAnimator;
            transform = actor.gameObject.transform;
            fieldOfView = actor.FieldOfView;
            sword = actor.Equipment.HaveWeapon.GetComponent<SwordController>();
            effectController = actor.gameObject.GetComponent<PlayerEffectController>();
        }
        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger(animator.ChargeAttackAnimationID, 2);
            animator.Animator.SetInteger(animator.AttackAnimationID, (int)NormalAttackState.ChargeAttack);
            velocity.Rigidbody.velocity = Vector3.zero;
            baseTransform = transform.position;

            sword.SetAttackType(AttackType.Charge,SwordSEType.Slash2);
            sword.Slash();

            effectController.Create(PlayerEffectType.ChargeSlash,effectController.ChargeSlashRotation);

            stauts.DecreaseSP(stauts.SpinAttackUseSP);

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
            if (aniInfo.normalizedTime > moveTime) { return; }
            if(fieldOfView.TargetObject != null)
            {
                movement.TargetLerpMove(baseTransform,fieldOfView.TargetLastPoint, forwardPower);
            }
            else
            {
                movement.ForwardLerpMove(baseTransform, forwardPower);
            }
        }

        public override void DoExit()
        {
            base.DoExit();
            sword.NotEnabledCollider();
            sword.SetRatioPower(1.0f);
        }
    }
}
