using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class GolemStampAttackState : GolemStateBase
    {
        private IMovement               movement;

        private IVelocityComponent      velocity;
        
        private Transform               thisTransform;

        private IGolemAnimator          animator;

        private GolemFistController     fist;

        private GolemEffectHandler      effectHandler;

        private Timer                   timer = new Timer();

        [SerializeField]
        private float                   attackMoveSpeed;

        [SerializeField]
        private float                   attackStartCount;
        [SerializeField]
        private float                   attackEndCount;

        [SerializeField]
        private float                   gravityMultiply;
        [SerializeField]
        private float                   effectCount = 1.8f;

        public static readonly string   StateKey = "StampAttack";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IGolemSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(GolemIdleState.StateKey)) { re.Add(new IsNotGolemAttackTransition(actor, "SA_Golem_Hammer", StateChanger, GolemIdleState.StateKey)); }
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
            effectHandler = actor.EffectHandler;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger("Attack", 1);

            fist.SetAttackType(AttackType.Normal);
            timer.Start(effectCount);
            timer.OnEnd += ActivationEffect;
        }

        private void ActivationEffect()
        {
            fist.StampSE();
            Vector3 pos = thisTransform.position;
            pos += thisTransform.forward * 2;
            effectHandler.EffectLedger.SetPosAndRotCreate((int)GolemEffectType.GroundImpact, pos, effectHandler.EffectLedger[(int)GolemEffectType.GroundImpact].transform.rotation);
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
            if (animInfo.normalizedTime > 0.5f && animInfo.normalizedTime < 0.6f)
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
            animator.Animator.SetInteger("Attack", -1);
            movement.Stop();
            fist.NotEnabledCollider();
            timer.OnEnd -= ActivationEffect;
        }
    }
}
