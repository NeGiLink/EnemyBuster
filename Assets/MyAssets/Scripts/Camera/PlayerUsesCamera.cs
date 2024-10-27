using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public enum PlayerCameraTag
    {
        Free,
        Target,
        Foucus,
        Count
    }

    [System.Serializable]
    public class PlayerUsesCamera
    {
        [SerializeField]
        private IMainCameraProvider mainCameraController;
        public IMainCameraProvider MainCameraController => mainCameraController;

        private IFocusInputProvider focusInput;

        [SerializeField]
        private FieldOfView foucusTarget;
        public FieldOfView FoucusTarget => foucusTarget;

        [SerializeField]
        private PlayerCameraTag cameraTag;

        private IAllPlayerCamera[] allPlayerCamera = new IAllPlayerCamera[(int)PlayerCameraTag.Count];

        private IAllPlayerCamera usePlayerCamera;

        public void Setup(GameObject _controller, IMainCameraProvider _this)
        {
            focusInput = _controller.gameObject.GetComponent<IFocusInputProvider>();
            mainCameraController = _this;
            foucusTarget = _controller.gameObject.GetComponent<FieldOfView>();

            allPlayerCamera = new IAllPlayerCamera[]
            {
                new FreePlayerCamera(),
                new TargetPlayerCamera(),
                new FoucusPlayerCamera(),
            };

            foreach (var camera in allPlayerCamera)
            {
                camera.Setup(this);
            }

            usePlayerCamera = allPlayerCamera[(int)PlayerCameraTag.Free];
        }

        public void DoStart()
        {
            cameraTag = PlayerCameraTag.Free;
        }

        public void DoUpdate()
        {
            if(focusInput.Foucus > 0&&cameraTag != PlayerCameraTag.Target && cameraTag != PlayerCameraTag.Foucus)
            {
                if(foucusTarget.TargetObject != null)
                {
                    cameraTag = PlayerCameraTag.Target;
                }
                else
                {
                    cameraTag = PlayerCameraTag.Foucus;
                }
            }
            else if(focusInput.Foucus <= 0 &&cameraTag != PlayerCameraTag.Free)
            {
                cameraTag = PlayerCameraTag.Free;
            }


            if(usePlayerCamera != allPlayerCamera[(int)cameraTag])
            {
                usePlayerCamera = allPlayerCamera[(int)cameraTag];
            }

            usePlayerCamera?.CameraUpdate();
        }
    }

    public class FreePlayerCamera : IAllPlayerCamera
    {
        private IMainCameraProvider mainCameraProvider;
        private PlayerUsesCamera playerUsesCamera;
        private FieldOfView fieldOfView;

        public void Setup(PlayerUsesCamera controller)
        {
            fieldOfView = controller.FoucusTarget;
            playerUsesCamera = controller;
            mainCameraProvider = controller.MainCameraController;
        }

        public void CameraUpdate()
        {
            if (mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Free].Priority < 10)
            {
                mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Free].Priority = 10;
                mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Target].Priority = 1;
                mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Foucus].Priority = 1;
                mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Target].LookAt = null;
            }
        }
    }

    public class TargetPlayerCamera : IAllPlayerCamera
    {
        private IMainCameraProvider mainCameraProvider;
        private PlayerUsesCamera playerUsesCamera;
        private FieldOfView fieldOfView;

        public void Setup(PlayerUsesCamera controller)
        {
            fieldOfView = controller.FoucusTarget;
            playerUsesCamera = controller;
            mainCameraProvider = controller.MainCameraController;
        }

        public void CameraUpdate()
        {
            if (mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Target].Priority < 10)
            {
                mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Target].Priority = 10;
                mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Free].Priority = 1;
                mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Foucus].Priority = 1;
            }

            if(fieldOfView.TargetObject?.transform != null&& mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Target].LookAt == null)
            {
                mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Target].LookAt = fieldOfView.TargetObject.transform;
            }
        }
    }
    public class FoucusPlayerCamera : IAllPlayerCamera
    {
        private IMainCameraProvider mainCameraProvider;
        private PlayerUsesCamera playerUsesCamera;
        private FieldOfView fieldOfView;

        public void Setup(PlayerUsesCamera controller)
        {
            fieldOfView = controller.FoucusTarget;
            playerUsesCamera = controller;
            mainCameraProvider = controller.MainCameraController;
        }

        public void CameraUpdate()
        {
            if (mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Foucus].Priority < 10)
            {
                mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Foucus].Priority = 10;
                mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Free].Priority = 1;
                mainCameraProvider.VirtualCameras[(int)PlayerCameraTag.Target].Priority = 1;
            }
        }
    }
}
