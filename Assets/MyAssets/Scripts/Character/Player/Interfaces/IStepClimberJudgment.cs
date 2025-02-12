

using UnityEngine;

namespace MyAssets
{
    //段差乗り越えクラスのインターフェース
    public interface IStepClimberJudgment
    {
        void HandleStepClimbing();

        float MaxStepHeight { get; }

        float StepSmooth {  get; }

        Vector3 StepGolePosition {  get;}
    }
}
