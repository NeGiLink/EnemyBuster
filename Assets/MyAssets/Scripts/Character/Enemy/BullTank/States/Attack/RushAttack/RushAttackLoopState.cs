using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * ブルタンクのラッシュアタック中の状態
     */
    [System.Serializable]
    public class RushAttackLoopState : BullTankStateBase
    {
        private IMovement                       movement;
        private IVelocityComponent              velocity;

        private IBullTankAnimator               animator;

        private BullTankHeadAttackController    headWeapon;

        [SerializeField]
        private float                           moveSpeed;

        [SerializeField]
        private float                           gravityMultiply;

        private Timer                           timer = new Timer();

        [SerializeField]
        private float                           count;

        [SerializeField]
        private float                           startColliderCount = 0.0f;

        [SerializeField]
        private float                           endColliderCount = 1.0f;

        public static readonly string           StateKey = "RushLoop";
        public override string                  Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(RushAttackEndState.StateKey)) { re.Add(new IsTimerTransition(actor, timer, StateChanger, RushAttackEndState.StateKey)); }
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
            animator.Animator.SetInteger(animator.AttackAnimationID, -1);
            timer.Start(count);
        }

        public override void DoUpdate(float time)
        {
            headWeapon.EnabledCollider(startColliderCount, endColliderCount, true);
            base.DoUpdate(time);
            timer.Update(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.ForwardMove(moveSpeed);
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            movement.Stop();
            animator.Animator.SetInteger(animator.AttackAnimationID, 2);
        }
    }
}
