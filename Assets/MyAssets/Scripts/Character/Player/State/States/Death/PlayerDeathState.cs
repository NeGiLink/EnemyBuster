using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace MyAssets
{
    [System.Serializable]
    public class PlayerDeathState : PlayerStateBase
    {
        private PlayerController playerController;

        private IPlayerAnimator animator;

        private IMovement movement;

        public static readonly string StateKey = "Death";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            
            return re;
        }
        public override void DoSetup(IPlayerSetup player)
        {
            base.DoSetup(player);
            playerController = player.gameObject.GetComponent<PlayerController>();
            animator = player.PlayerAnimator;
            movement = player.Movement;
        }

        public override void DoStart()
        {
            base.DoStart();
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
