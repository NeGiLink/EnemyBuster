

using UnityEngine;

namespace MyAssets
{
    //�i�����z���N���X�̃C���^�[�t�F�[�X
    public interface IStepClimberJudgment
    {
        void HandleStepClimbing();

        float MaxStepHeight { get; }

        float StepSmooth {  get; }

        Vector3 StepGolePosition {  get;}
    }
}
