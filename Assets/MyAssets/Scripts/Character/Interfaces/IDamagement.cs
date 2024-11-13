using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface IDamagement
    {
        void AddForceMove(Vector3 origin, Vector3 target, float power);
    }
}
