using UnityEngine;

namespace MyAssets
{
    /*
     * FieldOfView�̃C���^�[�t�F�[�X
     * �O������FieldOfView�̐錾���擾���������̂������ɐ錾
     */
    public interface IFieldOfView
    {
        GameObject  TargetObject { get; }
        Vector3     TargetLastPoint { get; }

        void DoUpdate();
    }
}
