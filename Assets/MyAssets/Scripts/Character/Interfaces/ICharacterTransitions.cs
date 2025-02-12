
namespace MyAssets
{
    /*
     * 全キャラクター共通で使う状態遷移クラス一覧
     */

    //視界にターゲットを捉えた時
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

    //視界からターゲットが消えた時
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
    //ターゲットが指定した距離よりも近くに来た時
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
    //主に敵に使用
    //ダメージを受けてから視界にターゲットがいる時
    public class IsTimerTargetInViewTransition : CharacterStateTransitionBase
    {
        private readonly FieldOfView fieldOfView;

        private readonly Timer       damageTimer;
        public IsTimerTargetInViewTransition(ICharacterSetup actor, Timer _t, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            fieldOfView = actor.gameObject.GetComponent<FieldOfView>();
            damageTimer = _t;
        }
        public override bool IsTransition() => fieldOfView.TryGetFirstObject(out var obj) && damageTimer.IsEnd();
    }

    //引数で取得したTriggerクラスのtriggerがtruenなら遷移させるクラス
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

    //死亡した時
    public class IsDeathTransition : CharacterStateTransitionBase
    {

        private readonly IBaseStauts stauts;

        public IsDeathTransition(ICharacterSetup chara, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            stauts = chara.BaseStauts;
        }
        public override bool IsTransition() => stauts.HP <= 0;
    }
}
