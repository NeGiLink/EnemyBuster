using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * ブルタンクのラッシュアタック待機中の状態
     */
    [System.Serializable]
    public class ReadyRushAttackLoopState : BullTankStateBase
    {
        private IMovement               movement;
        private IVelocityComponent      velocity;
        private FieldOfView             fieldOfView;

        private IBullTankAnimator       animator;

        [SerializeField]
        private float                   gravityMultiply;

        private Timer                   timer = new Timer();

        [SerializeField]
        private float                   count;

        public static readonly string   StateKey = "ReadyRushLoop";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(RushAttackStartState.StateKey)) { re.Add(new IsTimerTransition(actor,timer, StateChanger, RushAttackStartState.StateKey)); }
            return re;
        }

        public override void DoSetup(IBullTankSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
            animator = actor.BullTankAnimator;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetInteger(animator.ReadyAttackAnimationID, -1);
            timer.Start(count);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            timer.Update(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.MoveTo(fieldOfView.TargetLastPoint, 0, 0, 8, time);
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            movement.Stop();
        }
    }
}
