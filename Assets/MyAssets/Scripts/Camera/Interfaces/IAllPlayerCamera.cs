using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface IAllPlayerCamera
    {
        void Setup(PlayerUsesCamera controller);

        void Start();
        void CameraUpdate();
        void Exit();
    }
}
