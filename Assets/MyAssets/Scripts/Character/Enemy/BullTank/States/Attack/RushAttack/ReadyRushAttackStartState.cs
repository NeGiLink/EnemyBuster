using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * ラッシュアタック開始状態
     */
    [System.Serializable]
    public class ReadyRushAttackStartState : BullTankStateBase
    {
        private IMovement               movement;
        private IVelocityComponent      velocity;
        private FieldOfView             fieldOfView;

        private IBullTankAnimator       animator;

        [SerializeField]
        private float                   gravityMultiply;

        public static readonly string   StateKey = "ReadyRushStart";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(ReadyRushAttackLoopState.StateKey)) { re.Add(new IsNotBullTankAttackTransition(actor, "Attack02_Charge_Start", StateChanger, ReadyRushAttackLoopState.StateKey)); }
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
            animator.Animator.SetInteger(animator.ReadyAttackAnimationID, 0);
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
            animator.Animator.SetInteger(animator.ReadyAttackAnimationID, -1);
            movement.Stop();
        }
    }
}
