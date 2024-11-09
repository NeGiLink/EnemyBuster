using UnityEngine;

namespace MyAssets
{
    public class IsPatrolTransition : CharacterStateTransitionBase
    {
        private readonly Timer timer;

        public IsPatrolTransition(ISlimeSetup enemy,Timer _timer, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            timer = _timer;
        }
        public override bool IsTransition() => timer.IsEnd();
    }

    public class IsNotPatrolTransition : CharacterStateTransitionBase
    {
        private readonly PatrplPointContainer container;

        public IsNotPatrolTransition(ISlimeSetup enemy,IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            container = enemy.gameObject.GetComponent<PatrplPointContainer>();
        }
        public override bool IsTransition() => container.IsStop;
    }
}
