using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class ClimbState : PlayerStateBase
    {

        private IPlayerAnimator animator;

        private IMoveInputProvider moveInput;

        private IClimb climb;

        public static readonly string StateKey = "Climb";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(MoveState.StateKey)) { re.Add(new IsEndClimbTransition(actor, StateChanger, MoveState.StateKey)); }
            if (StateChanger.IsContain(PlayerIdleState.StateKey)) { re.Add(new IsEndClimbTransition(actor, StateChanger, PlayerIdleState.StateKey)); }
            return re;
        }

        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            animator = player.PlayerAnimator;
            climb = player.Climb;
            moveInput = player.MoveInput;
        }

        public override void DoStart()
        {
            base.DoStart();
            animator.Animator.SetBool(animator.ClimbName, true);
            climb.DoClimbStart();
            Debug.Log("“o‚èŠJŽn");
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            climb.DoClimb();
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
        }

        public override void DoExit()
        {
            base.DoExit();
            climb.DoClimbExit();
            animator.Animator.SetBool(animator.ClimbName, false);
        }
    }
}
