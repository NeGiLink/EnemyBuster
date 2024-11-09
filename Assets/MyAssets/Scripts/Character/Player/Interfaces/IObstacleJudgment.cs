

namespace MyAssets
{
    public interface IObstacleJudgment
    {
        void RayCheck();

        void InitRay();
        bool IsClimbStart { get;}
        //bool IsHurdleJumping {  get;}
    }
}
