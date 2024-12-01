
namespace MyAssets
{
    public class IsNPCMoveTransition : CharacterStateTransitionBase
    {
        private readonly INPCCommandPanel commandPanel;

        public IsNPCMoveTransition(INPCSetup npc, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            commandPanel = npc.CommandPanel;
        }
        public override bool IsTransition() => !commandPanel.IsFixedflag;
    }

    public class IsNPCNotMoveTransition : CharacterStateTransitionBase
    {
        private readonly INPCCommandPanel commandPanel;

        public IsNPCNotMoveTransition(INPCSetup npc, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            commandPanel = npc.CommandPanel;
        }
        public override bool IsTransition() => commandPanel.IsFixedflag;
    }
}
