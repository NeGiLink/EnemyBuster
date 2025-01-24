using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class BullTankSideMoveState : BullTankStateBase
    {
        private IMovement movement;
        private IVelocityComponent velocity;
        private Transform thisTransform;
        private FieldOfView fieldOfView;

        private IDamageContainer damageContainer;

        private IBullTankAnimator animator;

        [SerializeField]
        private float moveSpeed;


        [SerializeField]
        private float rotationSpeed = 8;
        /*
        [SerializeField]
        private float highMoveSpeed;
        [SerializeField]
        private float moveSpeedChangeRate = 8;

        [SerializeField]
        private float minChaseDistance = 2.5f;

        [SerializeField]
        private float maxDistance = 5f;
         */

        [SerializeField]
        private float gravityMultiply;

        private int right;

        private Timer sideMoveTimer = new Timer();
        [SerializeField]
        private float count = 3f;

        public static readonly string StateKey = "SideMove";
        public override string Key => StateKey;

        public static readonly int MoveAnimationID = Animator.StringToHash("Move");

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(BullTankChaseState.StateKey)) { re.Add(new IsTimerTransition(actor,sideMoveTimer, StateChanger, BullTankChaseState.StateKey)); }
            if (StateChanger.IsContain(BullTankDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(actor, StateChanger, BullTankDamageState.StateKey)); }
            if (StateChanger.IsContain(BullTankDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, BullTankDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IBullTankSetup actor)
        {
            base.DoSetup(actor);
            movement = actor.Movement;
            velocity = actor.Velocity;
            thisTransform = actor.gameObject.transform;
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
            animator = actor.BullTankAnimator;
            damageContainer = actor.DamageContainer;
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
            animator.Animator.SetFloat("SideMove", right);
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
            //èdóÕÇâ¡éZ
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetFloat("SideMove", 0);
            movement.Stop();
        }
    }
}
