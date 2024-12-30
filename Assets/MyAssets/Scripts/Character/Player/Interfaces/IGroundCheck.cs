
namespace MyAssets
{
    public interface IGroundCheck
    {
        bool Landing {  get;}

        void SetLanding(bool b);

        void FallTimeUpdate();

        bool IsFalling { get;}

        float FallCount {  get;}
    }
}
