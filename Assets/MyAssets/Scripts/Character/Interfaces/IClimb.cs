
namespace MyAssets
{
    public interface IClimb
    {
        void DoClimbStart();
        void DoClimb();

        void DoClimbExit();

        bool IsClimbEnd {  get; }
    }
}

