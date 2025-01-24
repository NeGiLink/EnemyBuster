
using System.Collections.Generic;

namespace MyAssets
{
    [System.Serializable]
    public class PlayerDeathState : PlayerStateBase
    {
        private PlayerController playerController;

        private IPlayerAnimator animator;

        private IMovement movement;

        private SEHandler seHandler;

        public static readonly string StateKey = "Death";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            
            return re;
        }
        public override void DoSetup(IPlayerSetup actor)
        {
            base.DoSetup(actor);
            playerController = actor.gameObject.GetComponent<PlayerController>();
            animator = actor.PlayerAnimator;
            movement = actor.Movement;
            seHandler = actor.SEHandler;
        }

        public override void DoStart()
        {
            base.DoStart();
            seHandler.Play((int)PlayerSETag.Damage);
            animator.Animator.SetInteger("Impact", 2);

            playerController.gameObject.layer = 0;
            GameController.Instance.SetGameResultType(GameResultType.GameOver);
            GameUIController.Instance.CreateFadeResultTextUI();
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            movement.Stop();
        }
    }
}
