using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public abstract class PlayerStateBase : IPlayerState<string>
    {
        public abstract string Key { get; }
        public override string ToString() => Key;

        public IStateChanger<string> StateChanger { protected get; set; }

        List<ICharacterStateTransition<string>> transitionList = new List<ICharacterStateTransition<string>>();

        public abstract List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor);
        public virtual void DoSetup(IPlayerSetup actor)
        {
            transitionList = CreateTransitionList(actor);
        }

        public virtual void DoStart() { }

        public virtual void TransitionCheck()
        {
            foreach (var check in transitionList)
            {
                if (check.IsTransition())
                {
                    check.DoTransition();
                    break;
                }
            }
        }
        public virtual void DoUpdate(float time)
        {
            TransitionCheck();
        }

        public virtual void DoFixedUpdate(float time) { }

        public virtual void DoAnimatorIKUpdate() { }
        public virtual void DoExit() { }
    }

    public abstract class SlimeStateBase : ISlimeState<string>
    {
        public abstract string Key { get; }
        public override string ToString() => Key;

        public IStateChanger<string> StateChanger { protected get; set; }

        List<ICharacterStateTransition<string>> transitionList = new List<ICharacterStateTransition<string>>();

        public abstract List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup actor);
        public virtual void DoSetup(ISlimeSetup slime)
        {
            transitionList = CreateTransitionList(slime);
        }

        public virtual void DoStart() { }

        public virtual void TransitionCheck()
        {
            foreach (var check in transitionList)
            {
                if (check.IsTransition())
                {
                    check.DoTransition();
                    break;
                }
            }
        }
        public virtual void DoUpdate(float time)
        {
            TransitionCheck();
        }

        public virtual void DoFixedUpdate(float time) { }

        public virtual void DoAnimatorIKUpdate() { }
        public virtual void DoExit() { }
    }

    public abstract class CharacterStateTransitionBase : ICharacterStateTransition<string>
    {
        readonly IStateChanger<string> stateChanger;
        readonly string changeKey;
        protected CharacterStateTransitionBase(IStateChanger<string> stateChanger, string changeKey)
        {
            this.stateChanger = stateChanger;
            this.changeKey = changeKey;
        }
        public abstract bool IsTransition();
        public void DoTransition() => stateChanger.ChangeState(changeKey);
    }
}
