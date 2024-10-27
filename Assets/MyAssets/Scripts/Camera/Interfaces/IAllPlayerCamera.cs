using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface IAllPlayerCamera
    {
        void Setup(PlayerUsesCamera controller);

        void CameraUpdate();
    }
}
