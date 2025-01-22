using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class CounterAttackState : PlayerStateBase
    {
        private Transform transform;

        private IPlayerStauts stauts;

        private IMoveInputProvider input;

        private IVelocityComponent velocity;

        private IMovement movement;

        private IRotation rotation;

        private IChangingState changingState;

        private IPlayerAnimator animator;

        private IFieldOfView fieldOfView;

        private SwordController sword;

        private IEquipment equipment;

        [SerializeField]
        private float gravityMultiply;

        [SerializeField]
        private int counterAttackSP;
        [SerializeField]
        private float maxNormalizeCount = 0.5f;

        private string currentMotionName = "CounterAttack";

        public static readonly string StateKey = "CounterAttack";

        public override string Key => StateKey;
        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(FirstAttackState.StateKey)) { re.Add(new IsBurstAttackTransition(currentMotionName,maxNormalizeCount,actor, StateChanger, FirstAttackState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsNotAttackTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            if (StateChanger.IsContain(FallState.StateKey)) { re.Add(new IsNotGroundTransition(actor, StateChanger, FallState.StateKey)); }
            if (StateChanger.IsContain(PlayerDamageState.StateKey)) { re.Add(new IsDamageTransition(actor, StateChanger, PlayerDamageState.StateKey)); }
            if (StateChanger.IsContain(PlayerDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, PlayerDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            transform = player.gameObject.transform;
            stauts = player.Stauts;
            movement = player.Movement;
            velocity = player.Velocity;
            rotation = player.Rotation;
            changingState = player.ChangingState;
            input = player.gameObject.GetComponent<IMoveInputProvider>();
            animator = player.PlayerAnimator;
            fieldOfView = player.FieldOfView;
            sword = player.Equipment.HaveWeapon.GetComponent<SwordController>();
            equipment = player.Equipment;
        }

        public override void DoStart()
        {
            base.DoStart();
            if (!changingState.IsBattleMode)
            {
                equipment.SetOutWeapon();
                changingState.SetBattleMode(true);
                animator.Animator.SetFloat(animator.ToolLevel, 1.0f);
            }
            sword.SetAttackType(AttackType.Single);
            animator.Animator.SetInteger(animator.AttacksName, (int)NormalAttackState.Counter);

            stauts.DecreaseSP(counterAttackSP);

            if (fieldOfView.TargetObject != null)
            {
                Vector3 target = fieldOfView.TargetObject.transform.position;
                target.y = transform.position.y;
                transform.LookAt(target);
            }
        }

        public override void DoUpdate(float time)
        {
            sword.EnabledCollider(0.15f, 0.6f, false);
            base.DoUpdate(time);

            rotation.DoUpdate();
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            //ForwardMove();
            velocity.Rigidbody.velocity += Physics.gravity * gravityMultiply * time;
        }

        //private void ForwardMove()
        //{
        //    AnimatorStateInfo aniInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
        //    if (aniInfo.normalizedTime > secondVer1ToTransitionTime) { return; }
        //    movement.ForwardLerpMove(baseTransform, forwardPower);
        //}

        public override void DoExit()
        {
            base.DoExit();
            sword.NotEnabledCollider();
            animator.Animator.SetInteger(animator.AttacksName, -1);
        }
    }
}
