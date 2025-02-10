using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * ブルタンクの横移動状態
     */
    [System.Serializable]
    public class BullTankSideMoveState : BullTankStateBase
    {
        private IMovement               movement;
        private IVelocityComponent      velocity;

        private FieldOfView             fieldOfView;



        private IBullTankAnimator       animator;

        [SerializeField]
        private float                   moveSpeed;


        [SerializeField]
        private float                   rotationSpeed = 8;

        [SerializeField]
        private float                   gravityMultiply;

        private int                     right;

        private Timer                   sideMoveTimer = new Timer();
        [SerializeField]
        private float                   count = 3f;

        public static readonly string   StateKey = "SideMove";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BullTankActionDecisionState.StateKey)) { re.Add(new IsTimerTransition(actor,sideMoveTimer, StateChanger, BullTankActionDecisionState.StateKey)); }
            if (StateChanger.IsContain(BullTankDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(actor, StateChanger, BullTankDamageState.StateKey)); }
            if (StateChanger.IsContain(BullTankDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, BullTankDeathState.StateKey)); }
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
            movement.Stop();
            sideMoveTimer.Start(count);
            int random = Random.Range(0, 2);
            if(random == 0)
            {
                right = 1;
            }
            else
            {
                right = -1;
            }
            animator.Animator.SetFloat(animator.SideMoveAnimationID, right);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            sideMoveTimer.Update(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);

            movement.SideMove(right, moveSpeed,fieldOfView.TargetLastPoint,rotationSpeed,time);
            //重力を加算
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetFloat(animator.SideMoveAnimationID, 0);
            movement.Stop();
        }
    }
}
