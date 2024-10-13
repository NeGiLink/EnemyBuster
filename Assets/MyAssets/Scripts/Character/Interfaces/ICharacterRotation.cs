using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface ICharacterRotation
    {
        void DoUpdate();

        void DoFixedUpdate(Vector3 velocity);
    }
}
