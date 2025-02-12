
namespace MyAssets
{
    /*
     * 登り動作処理クラスのインターフェース
     */
    public interface IClimb
    {
        void DoClimbStart();
        void DoClimb();

        void DoClimbExit();

        bool IsClimbEnd {  get; }
    }
}

