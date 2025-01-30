
namespace MyAssets
{
    public class IsTargetInViewTransition : CharacterStateTransitionBase
    {
        readonly FieldOfView fieldOfView;
        public IsTargetInViewTransition(ICharacterSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
        }
        public override bool IsTransition() => fieldOfView.Find;
    }

    public class IsNoTargetInViewTransition : CharacterStateTransitionBase
    {
        readonly FieldOfView fieldOfView;
        public IsNoTargetInViewTransition(ICharacterSetup actor, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
        }
        public override bool IsTransition() => !fieldOfView.Find;
    }

    public class IsMinDistanceTransition : CharacterStateTransitionBase
    {
        readonly FieldOfView fieldOfView;

        readonly float distance;
        public IsMinDistanceTransition(ICharacterSetup actor,float dis, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
            distance = dis;
        }
        public override bool IsTransition() => fieldOfView.GetSubDistance.magnitude < distance;
    }

    public class IsTimerTargetInViewTransition : CharacterStateTransitionBase
    {
        readonly FieldOfView fieldOfView;

        private Timer damageTimer;
        public IsTimerTargetInViewTransition(ICharacterSetup actor, Timer _t, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
            damageTimer = _t;
        }
        public override bool IsTransition() => fieldOfView.TryGetFirstObject(out var obj) && damageTimer.IsEnd();
    }
    /*
     * 引数で取得したTriggerクラスのtriggerがtruenなら遷移させるクラス
     */
    public class IsTriggerTransition : CharacterStateTransitionBase
    {
        private readonly Trigger trigger;

        public IsTriggerTransition(ICharacterSetup actor, Trigger _t, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            trigger = _t;
        }
        public override bool IsTransition() => trigger.IsTrigger;
    }
}
