

using UnityEngine;

namespace MyAssets
{
    public interface IStepClimberJudgment
    {
        void HandleStepClimbing();

        float MaxStepHeight { get; }

        float StepSmooth {  get; }

        Vector3 StepGolePosition {  get;}
    }
}
