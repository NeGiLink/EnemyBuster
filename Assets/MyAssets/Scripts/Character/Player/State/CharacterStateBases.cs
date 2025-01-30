using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public abstract class CharacterStateBase<TKey, TCharacterSetup> : ICharacterState<TKey, TCharacterSetup>
    where TCharacterSetup : ICharacterSetup
    {
        public abstract TKey Key { get; }
        public override string ToString() => Key.ToString();

        public IStateChanger<TKey> StateChanger { protected get; set; }

        private List<ICharacterStateTransition<TKey>> transitionList = new List<ICharacterStateTransition<TKey>>();

        public abstract List<ICharacterStateTransition<TKey>> CreateTransitionList(TCharacterSetup actor);

        public virtual void DoSetup(TCharacterSetup actor)
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

        public virtual void DoLateUpdate(float time) { }

        public virtual void DoAnimatorIKUpdate() { }

        public virtual void DoExit() { }

        public virtual void DoTriggerEnter(GameObject thisObject, Collider collider) { }

        public virtual void DoTriggerStay(GameObject thisObject, Collider collider) { }

        public virtual void DoTriggerExit(GameObject thisObject, Collider collider) { }
    }
    public abstract class PlayerStateBase : CharacterStateBase<string, IPlayerSetup>, IPlayerState<string>
    {
        public override abstract string Key { get; }

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IPlayerSetup actor)
        {
            // PlayerState専用のトランジションリスト作成処理を実装
            return new List<ICharacterStateTransition<string>>();
        }
    }

    public abstract class EnemyStateBase : CharacterStateBase<string, IEnemySetup>, IEnemyState<string>
    {
        public override abstract string Key { get; }

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IEnemySetup actor)
        {
            // SlimeState専用のトランジションリスト作成処理を実装
            return new List<ICharacterStateTransition<string>>();
        }
    }

    public abstract class SlimeStateBase : CharacterStateBase<string, ISlimeSetup>, ISlimeState<string>
    {
        public override abstract string Key { get; }

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup actor)
        {
            // SlimeState専用のトランジションリスト作成処理を実装
            return new List<ICharacterStateTransition<string>>();
        }
    }

    public abstract class MushroomStateBase : CharacterStateBase<string, IMushroomSetup>, IMushroomState<string>
    {
        public override abstract string Key { get; }

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IMushroomSetup actor)
        {
            // SlimeState専用のトランジションリスト作成処理を実装
            return new List<ICharacterStateTransition<string>>();
        }
    }
    public abstract class BullTankStateBase : CharacterStateBase<string, IBullTankSetup>, IBullTankState<string>
    {
        public override abstract string Key { get; }

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IBullTankSetup actor)
        {
            // SlimeState専用のトランジションリスト作成処理を実装
            return new List<ICharacterStateTransition<string>>();
        }
    }

    public abstract class GolemStateBase : CharacterStateBase<string, IGolemSetup>, IGolemState<string>
    {
        public override abstract string Key { get; }

        public override List<ICharacterStateTransition<string>> CreateTransitionList(IGolemSetup actor)
        {
            // SlimeState専用のトランジションリスト作成処理を実装
            return new List<ICharacterStateTransition<string>>();
        }
    }

    public abstract class NPCStateBase : CharacterStateBase<string, INPCSetup>, INPCState<string>
    {
        public override abstract string Key { get; }

        public override List<ICharacterStateTransition<string>> CreateTransitionList(INPCSetup actor)
        {
            // SlimeState専用のトランジションリスト作成処理を実装
            return new List<ICharacterStateTransition<string>>();
        }
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
