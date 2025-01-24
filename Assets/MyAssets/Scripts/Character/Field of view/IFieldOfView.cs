using UnityEngine;

namespace MyAssets
{
    public interface IFieldOfView
    {
        GameObject TargetObject { get; }

        void DoUpdate();
    }
}
