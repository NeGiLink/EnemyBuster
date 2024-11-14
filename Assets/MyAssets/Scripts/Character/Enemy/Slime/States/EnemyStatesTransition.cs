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

    public class IsEnemyDamageTransition : CharacterStateTransitionBase
    {
        private readonly IDamageContainer damageContainer;

        public IsEnemyDamageTransition(ISlimeSetup chara, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            damageContainer = chara.DamageContainer;
        }
        public override bool IsTransition() => damageContainer.AttackType != AttackType.None;
    }

    public class IsNotDamageToTransition : CharacterStateTransitionBase
    {

        private readonly IDamageContainer damageContainer;

        private readonly Timer damageTimer;

        public IsNotDamageToTransition(ISlimeSetup chara, Timer t, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            damageTimer = t;
            damageContainer = chara.DamageContainer;
        }
        public override bool IsTransition() => damageTimer.IsEnd();
    }
}
