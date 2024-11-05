using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class IdleState : PlayerStateBase
    {
        private IMoveInputProvider input;
        
        private IFocusInputProvider focusInputProvider;
        
        private IVelocityComponent velocity;

        private IMovement movement;
        
        private IRotation rotation;

        private IObstacleJudgment cliffJudgment;

        private IPlayerAnimator animator;

        private IFootIK footIK;
        
        private FieldOfView fieldOfView;

        private IEquipment equipment;

        [SerializeField]
        private float moveSpeed = 4.0f;
        [SerializeField]
        private float idleGravityMultiply;

        public static readonly string StateKey = "Idle";
        public override string Key => StateKey;

        public override List<IPlayerStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<IPlayerStateTransition<string>> re = new List<IPlayerStateTransition<string>>();
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsMoveTransition(actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(BattleIdleState.StateKey)) { re.Add(new IsBattleModeTransition(actor, StateChanger, BattleIdleState.StateKey)); }
            if (StateChanger.IsContain(JumpState.StateKey)) { re.Add(new IsJumpPushTransition(actor, StateChanger, JumpState.StateKey)); }
            if (StateChanger.IsContain(FallState.StateKey)) { re.Add(new IsNotGroundTransition(actor, StateChanger, FallState.StateKey)); }
            if (StateChanger.IsContain(ClimbState.StateKey)) { re.Add(new IsClimbTransition(actor, StateChanger, ClimbState.StateKey)); }
            if (StateChanger.IsContain(FirstAttackState.StateKey)) { re.Add(new IsFirstAttackTransition(actor, StateChanger, FirstAttackState.StateKey)); }
            if (StateChanger.IsContain(WeaponOutState.StateKey)) { re.Add(new IsWeaponOutTransition(actor, StateChanger, WeaponOutState.StateKey)); }
            if (StateChanger.IsContain(WeaponInState.StateKey)) { re.Add(new IsWeaponInTransition(actor, StateChanger, WeaponInState.StateKey)); }
            return re;
        }
        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            focusInputProvider = player.gameObject.GetComponent<IFocusInputProvider>();
            input = player.gameObject.GetComponent<IMoveInputProvider>();
            velocity = player.Velocity;
            movement = player.Movement;
            rotation = player.Rotation;
            cliffJudgment = player.ObstacleJudgment;
            footIK = player.FootIK;
            animator = player.PlayerAnimator;
            fieldOfView = player.gameObject.GetComponent<FieldOfView>();
            equipment = player.gameObject.GetComponent<IEquipment>();
        }

        public override void DoStart()
        {
            base.DoStart();
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            AnimationUpdate();
            cliffJudgment.RayCheck();
            rotation.DoUpdate();
        }

        private void AnimationUpdate()
        {
            animator.Animator.SetFloat(animator.MoveName, velocity.CurrentVelocity.magnitude, 0.1f, Time.deltaTime);

            animator.UpdateWeight();

            if (fieldOfView.TargetObject != null)
            {
                animator.Animator.SetFloat(animator.AlertLevelName, 1.0f,0.1f,Time.deltaTime);
            }
            else
            {
                animator.Animator.SetFloat(animator.AlertLevelName, 0.0f, 0.1f, Time.deltaTime);
            }
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);

            movement.Move(moveSpeed);
            
            velocity.Rigidbody.velocity += Physics.gravity * idleGravityMultiply * time;
            
            rotation.DoFixedUpdate();
        }

        public override void DoAnimatorIKUpdate()
        {
            base.DoAnimatorIKUpdate();
            footIK.DoUpdate();
        }
    }
}
