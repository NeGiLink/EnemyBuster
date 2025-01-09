using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public enum CameraTag
    {
        Free,
        Target,
        OverTheShoulder,
        Count
    }

    /// <summary>
    /// プレイヤーが自由に操作できるカメラを処理するクラス
    /// </summary>
    [System.Serializable]
    public class InputControllCamera
    {
        [SerializeField]
        private IMainCameraProvider mainCameraController;
        public IMainCameraProvider MainCameraController => mainCameraController;

        private IFocusInputProvider focusInput;

        [SerializeField]
        private FieldOfView foucusTarget;
        public FieldOfView FoucusTarget => foucusTarget;

        [SerializeField]
        private CameraTag cameraTag;

        private IAllPlayerCamera[] allPlayerCamera = new IAllPlayerCamera[(int)CameraTag.Count];

        private IAllPlayerCamera usePlayerCamera;

        private Quaternion fixedCamRotation;
        public Quaternion FixedCamRotation => fixedCamRotation;
        public void SetFixedCamRotation(Quaternion rotation) { fixedCamRotation = rotation; }

        public void Setup(GameObject _controller, IMainCameraProvider _this)
        {
            focusInput = _controller.gameObject.GetComponent<IFocusInputProvider>();
            mainCameraController = _this;
            foucusTarget = _controller.gameObject.GetComponent<FieldOfView>();

            allPlayerCamera = new IAllPlayerCamera[]
            {
                new FreeCamera(),
                new TargetCamera(),
                new OverTheShoulderCamera(),
            };

            foreach (var camera in allPlayerCamera)
            {
                camera.Setup(this);
            }

            usePlayerCamera = allPlayerCamera[(int)CameraTag.Free];
        }

        public void DoStart()
        {
            cameraTag = CameraTag.Free;

            usePlayerCamera.Start();
        }

        public void DoUpdate()
        {
            if(focusInput.Foucus > 0)
            {
                if(foucusTarget.TargetObject != null)
                {
                    cameraTag = CameraTag.Target;
                }
                else
                {
                    cameraTag = CameraTag.OverTheShoulder;
                }
            }
            else if(focusInput.Foucus <= 0)
            {
                cameraTag = CameraTag.Free;
            }


            CheckCameraType();

            usePlayerCamera?.CameraUpdate();
        }

        private void CheckCameraType()
        {
            if (usePlayerCamera != allPlayerCamera[(int)cameraTag])
            {
                usePlayerCamera.Exit();
                usePlayerCamera = allPlayerCamera[(int)cameraTag];
                usePlayerCamera.Start();
            }
        }
    }

    public class FreeCamera : IAllPlayerCamera
    {
        private IMainCameraProvider mainCameraProvider;
        private InputControllCamera inputControllCamera;

        private CinemachinePOV      povComponent;
        public CinemachinePOV       PovComponent => povComponent;

        private Vector3             inputEulerRotation;
        public Vector3              InputEulerRotation => inputEulerRotation;

        public void Setup(InputControllCamera controller)
        {
            inputControllCamera = controller;
            mainCameraProvider = controller.MainCameraController;
            // POVカメラのPOVコンポーネントを取得
            povComponent = mainCameraProvider.VirtualCameras[(int)CameraTag.Free].GetCinemachineComponent<CinemachinePOV>();

            inputControllCamera.SetFixedCamRotation(mainCameraProvider.TargetTransform.rotation);
        }
        public void Start()
        {
            mainCameraProvider.VirtualCameras[(int)CameraTag.Free].Priority = 10;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Target].Priority = 1;
            mainCameraProvider.VirtualCameras[(int)CameraTag.OverTheShoulder].Priority = 1;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Target].LookAt = null;
            // Quaternionをオイラー角に変換し、POVコンポーネントの軸に反映
            Vector3 eulerRotation = inputControllCamera.FixedCamRotation.eulerAngles;
            povComponent.m_VerticalAxis.Value = eulerRotation.x;
            povComponent.m_HorizontalAxis.Value = eulerRotation.y;
        }

        public void Exit()
        {
            inputEulerRotation = inputControllCamera.FixedCamRotation.eulerAngles;
        }

        public void CameraUpdate()
        {
        }
    }

    public class TargetCamera : IAllPlayerCamera
    {
        private IMainCameraProvider mainCameraProvider;
        private InputControllCamera playerUsesCamera;
        private FieldOfView fieldOfView;

        public void Setup(InputControllCamera controller)
        {
            fieldOfView = controller.FoucusTarget;
            playerUsesCamera = controller;
            mainCameraProvider = controller.MainCameraController;
        }
        public void Start()
        {
            mainCameraProvider.VirtualCameras[(int)CameraTag.Target].Priority = 10;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Free].Priority = 5;
            mainCameraProvider.VirtualCameras[(int)CameraTag.OverTheShoulder].Priority = 1;
            // POVカメラのPOVコンポーネントを取得
            /*
            var povComponent = mainCameraProvider.VirtualCameras[(int)CameraTag.Free].GetCinemachineComponent<CinemachinePOV>();

            // Quaternionをオイラー角に変換し、POVコンポーネントの軸に反映
            Vector3 eulerRotation = playerUsesCamera.FixedCamRotation.eulerAngles;
            //povComponent.m_VerticalAxis.Value = eulerRotation.x;
            povComponent.m_HorizontalAxis.Value = eulerRotation.y;
             */
        }

        public void Exit()
        {
            GameObject camera = mainCameraProvider.VirtualCameras[(int)CameraTag.Target].gameObject;
            playerUsesCamera.SetFixedCamRotation(camera.transform.rotation);
        }

        public void CameraUpdate()
        {

            if(fieldOfView.FindTarget && mainCameraProvider.VirtualCameras[(int)CameraTag.Target].LookAt == null)
            {
                mainCameraProvider.VirtualCameras[(int)CameraTag.Target].LookAt = fieldOfView.TargetObject.transform;
            }
        }
    }
    public class OverTheShoulderCamera : IAllPlayerCamera
    {
        private IMainCameraProvider mainCameraProvider;
        private InputControllCamera playerUsesCamera;
        private FieldOfView fieldOfView;

        public void Setup(InputControllCamera controller)
        {
            fieldOfView = controller.FoucusTarget;
            playerUsesCamera = controller;
            mainCameraProvider = controller.MainCameraController;
        }
        public void Start()
        {
            mainCameraProvider.VirtualCameras[(int)CameraTag.OverTheShoulder].Priority = 10;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Free].Priority = 5;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Target].Priority = 1;
        }

        public void Exit()
        {
            GameObject camera = mainCameraProvider.MainCamera;
            playerUsesCamera.SetFixedCamRotation(camera.transform.rotation);

            // POVカメラのPOVコンポーネントを取得
            var povComponent = mainCameraProvider.VirtualCameras[(int)CameraTag.Free].GetCinemachineComponent<CinemachinePOV>();

            // Quaternionをオイラー角に変換し、POVコンポーネントの軸に反映
            Vector3 eulerRotation = mainCameraProvider.VirtualCameras[(int)CameraTag.OverTheShoulder].transform.rotation.eulerAngles;
            povComponent.m_VerticalAxis.Value = eulerRotation.x;
            povComponent.m_HorizontalAxis.Value = eulerRotation.y;
        }

        public void CameraUpdate()
        {
        }
    }
}
