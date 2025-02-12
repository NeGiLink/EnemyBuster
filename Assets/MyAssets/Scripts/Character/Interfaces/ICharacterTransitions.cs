
namespace MyAssets
{
    /*
     * �S�L�����N�^�[���ʂŎg����ԑJ�ڃN���X�ꗗ
     */

    //���E�Ƀ^�[�Q�b�g�𑨂�����
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

    //���E����^�[�Q�b�g����������
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
    //�^�[�Q�b�g���w�肵�����������߂��ɗ�����
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
    //��ɓG�Ɏg�p
    //�_���[�W���󂯂Ă��王�E�Ƀ^�[�Q�b�g�����鎞
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

    //�����Ŏ擾����Trigger�N���X��trigger��truen�Ȃ�J�ڂ�����N���X
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

    //���S������
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
