using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface IFieldOfView
    {
        GameObject TargetObject { get; }

        void DoUpdate();
    }
}
