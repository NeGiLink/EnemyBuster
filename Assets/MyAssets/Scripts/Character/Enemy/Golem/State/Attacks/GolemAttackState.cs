using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * ƒS[ƒŒƒ€‚ÌUŒ‚1ó‘Ô
     */
    [System.Serializable]
    public class GolemAttackState : GolemStateBase
    {
        private IMovement               movement;

        private IVelocityComponent      velocity;
        
        private Transform               thisTransform;

        private IGolemAnimator          animator;

        private GolemFistController     fist;

        [SerializeField]
        private float                   attackMoveSpeed;

        [SerializeField]
        private float                   attackStartCount;
        [SerializeField]
        private float                   attackEndCount;

        [SerializeField]
        private float                   gravityMultiply;

        [SerializeField]
        private float                   count = 1.8f;

        private Timer                   timer = new Timer();

        public static readonly string   StateKey = "Attack";
        public override string          Key => StateKey;

        private readonly string         motionName = "SA_Golem_Hit";

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IGolemSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(GolemIdleState.StateKey)) { re.Add(new IsNotGolemAttackTransition(actor, motionName, StateChanger, GolemIdleState.StateKey)); }
            if (StateChanger.IsContain(GolemDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(actor, StateChanger, GolemDamageState.StateKey)); }
            if (StateChanger.IsContain(GolemDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, GolemDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IGolemSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
            thisTransform = actor.gameObject.transform;
            animator = actor.GolemAnimator;
            fist = actor.FistController;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger(animator.AttackAnimationID, 0);

            fist.SetAttackType(AttackType.Normal);
            timer.Start(count);
            timer.OnEnd += ActivationSE;
        }

        private void ActivationSE()
        {
            fist.AttackSE();
            
        }

        public override void DoUpdate(float time)
        {
            timer.Update(time);
            fist.EnabledCollider(attackStartCount, attackEndCount, false);
            base.DoUpdate(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            if (animInfo.normalizedTime > 0.5f&& animInfo.normalizedTime < 0.6f)
            {
                movement.ForwardLerpMove(thisTransform.position, attackMoveSpeed);
            }
            else
            {
                movement.Stop();
            }
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger(animator.AttackAnimationID, -1);
            movement.Stop();
            fist.NotEnabledCollider();
            timer.OnEnd -= ActivationSE;
        }
    }
}
