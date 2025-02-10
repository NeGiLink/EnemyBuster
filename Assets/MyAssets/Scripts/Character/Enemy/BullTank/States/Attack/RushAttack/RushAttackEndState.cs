using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * ブルタンクのラッシュアタック終了状態
     */
    [System.Serializable]
    public class RushAttackEndState : BullTankStateBase
    {
        private IMovement                       movement;
        private IVelocityComponent              velocity;

        private IBullTankAnimator               animator;

        private BullTankHeadAttackController    headWeapon;

        [SerializeField]
        private float                           decreaseSpeed;

        [SerializeField]
        private float                           gravityMultiply;

        public static readonly string           StateKey = "RushEnd";
        public override string                  Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BullTankIdleState.StateKey)) { re.Add(new IsNotBullTankAttackTransition(actor, "Attack02_End", StateChanger, BullTankIdleState.StateKey)); }
            return re;
        }

        public override void DoSetup(IBullTankSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
            animator = actor.BullTankAnimator;
            headWeapon = actor.HeadAttackObject;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger(animator.AttackAnimationID, 2);
        }

        public override void DoUpdate(float time)
        {
            headWeapon.EnabledCollider(0, 0, true);
            base.DoUpdate(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.DecreaseMove(decreaseSpeed);
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            movement.Stop();
            animator.Animator.SetInteger(animator.AttackAnimationID, -1);
            headWeapon.NotEnabledCollider();
        }
    }
}
