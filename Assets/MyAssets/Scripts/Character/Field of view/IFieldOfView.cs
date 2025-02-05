using UnityEngine;

namespace MyAssets
{
    public interface IFieldOfView
    {
        GameObject TargetObject { get; }
        Vector3 TargetLastPoint { get; }

        void DoUpdate();
    }
}
