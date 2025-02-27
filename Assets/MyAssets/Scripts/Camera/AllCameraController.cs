using Cinemachine;
using UnityEngine;

namespace MyAssets
{
    public enum CameraTag
    {
        Free,
        Target,
        Option,
        Result
    }

    /// <summary>
    /// 全カメラを切り替えて処理するクラス
    /// </summary>
    [System.Serializable]
    public class AllCameraController
    {
        [SerializeField]
        private IMainCameraProvider     mainCameraController;
        public IMainCameraProvider      MainCameraController => mainCameraController;

        private IFocusInputProvider     focusInput;

        [SerializeField]
        private FieldOfView             foucusTarget;
        public FieldOfView              FoucusTarget => foucusTarget;

        [SerializeField]
        private CameraTag               cameraTag;

        private IAllPlayerCamera[]      allPlayerCamera;

        private IAllPlayerCamera        usePlayerCamera;

        private Quaternion              fixedCamRotation;
        public Quaternion               FixedCamRotation => fixedCamRotation;
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
                new OptionCamera(),
                new ResultCamera()
            };

            foreach (var camera in allPlayerCamera)
            {
                camera.Setup(this);
            }
        }

        public void DoStart()
        {
            cameraTag = mainCameraController.CameraTag;
            usePlayerCamera = allPlayerCamera[(int)cameraTag];
            usePlayerCamera.Start();
        }

        public void DoUpdate()
        {
            //カメラ切り替え処理
            if(GameManager.Instance.SceneList == SceneList.Game)
            {

                if(Time.timeScale > 0)
                {
                    if(focusInput.Foucus > 0)
                    {
                        if(foucusTarget.TargetObject != null)
                        {
                            cameraTag = CameraTag.Target;
                        }
                    }
                    else if(focusInput.Foucus <= 0)
                    {
                        cameraTag = CameraTag.Free;
                    }
                }
                else
                {
                    cameraTag = CameraTag.Option;
                }
            }
            else if(GameManager.Instance.SceneList == SceneList.Result)
            {
                cameraTag = CameraTag.Result;
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
        private AllCameraController allCameraController;

        private CinemachinePOV      povComponent;
        public CinemachinePOV       PovComponent => povComponent;

        private Vector3             inputEulerRotation;
        public Vector3              InputEulerRotation => inputEulerRotation;

        public void Setup(AllCameraController controller)
        {
            allCameraController = controller;
            mainCameraProvider = controller.MainCameraController;
            // POVカメラのPOVコンポーネントを取得
            povComponent = mainCameraProvider.VirtualCameras[(int)CameraTag.Free].GetCinemachineComponent<CinemachinePOV>();

            allCameraController.SetFixedCamRotation(mainCameraProvider.TargetTransform.rotation);
        }
        public void Start()
        {
            mainCameraProvider.VirtualCameras[(int)CameraTag.Free].Priority = 10;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Target].Priority = 1;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Target].LookAt = null;
            // Quaternionをオイラー角に変換し、POVコンポーネントの軸に反映
            Vector3 eulerRotation = allCameraController.FixedCamRotation.eulerAngles;
            povComponent.m_VerticalAxis.Value = eulerRotation.x;
            povComponent.m_HorizontalAxis.Value = eulerRotation.y;
        }
        public void CameraTransition()
        {

        }

        public void Exit()
        {
            inputEulerRotation = allCameraController.FixedCamRotation.eulerAngles;
        }

        public void CameraUpdate()
        {
            if(InputManager.GetDeviceInput() == DeviceInput.Key)
            {
                povComponent.m_VerticalAxis.m_MaxSpeed = 0.1f;
                povComponent.m_HorizontalAxis.m_MaxSpeed = 0.1f;
            }
            else
            {
                povComponent.m_VerticalAxis.m_MaxSpeed = 1f;
                povComponent.m_HorizontalAxis.m_MaxSpeed = 1f;
            }
        }
    }

    public class TargetCamera : IAllPlayerCamera
    {
        private IMainCameraProvider mainCameraProvider;
        private AllCameraController allCameraController;
        private FieldOfView fieldOfView;

        public void Setup(AllCameraController controller)
        {
            fieldOfView = controller.FoucusTarget;
            allCameraController = controller;
            mainCameraProvider = controller.MainCameraController;
        }
        public void Start()
        {
            mainCameraProvider.VirtualCameras[(int)CameraTag.Target].Priority = 10;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Free].Priority = 5;
        }
        public void CameraTransition()
        {

        }

        public void Exit()
        {
            GameObject camera = mainCameraProvider.VirtualCameras[(int)CameraTag.Target].gameObject;
            allCameraController.SetFixedCamRotation(camera.transform.rotation);
        }

        public void CameraUpdate()
        {
            if (fieldOfView.Find)
            {
                if(fieldOfView.FindTarget && mainCameraProvider.VirtualCameras[(int)CameraTag.Target].LookAt == null)
                {
                    mainCameraProvider.VirtualCameras[(int)CameraTag.Target].LookAt = fieldOfView.TargetObject.transform;
                }
            }
            else
            {
                if (fieldOfView.FindTarget && mainCameraProvider.VirtualCameras[(int)CameraTag.Target].LookAt == null)
                {
                    mainCameraProvider.VirtualCameras[(int)CameraTag.Target].LookAt = fieldOfView.transform;
                }
            }
        }
    }

    public class OptionCamera : IAllPlayerCamera
    {
        private IMainCameraProvider mainCameraProvider;
        private AllCameraController allCameraController;

        private CinemachinePOV povComponent;
        public CinemachinePOV PovComponent => povComponent;

        private Vector3 inputEulerRotation;
        public Vector3 InputEulerRotation => inputEulerRotation;

        public void Setup(AllCameraController controller)
        {
            allCameraController = controller;
            mainCameraProvider = controller.MainCameraController;
            // POVカメラのPOVコンポーネントを取得
            povComponent = mainCameraProvider.VirtualCameras[(int)CameraTag.Free].GetCinemachineComponent<CinemachinePOV>();

            allCameraController.SetFixedCamRotation(mainCameraProvider.TargetTransform.rotation);
        }

        public void CameraTransition()
        {

        }
        public void Start()
        {
            mainCameraProvider.VirtualCameras[(int)CameraTag.Option].Priority = 10;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Free].Priority = 1;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Target].Priority = 1;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Result].Priority = 1;
        }

        public void Exit()
        {
            inputEulerRotation = allCameraController.FixedCamRotation.eulerAngles;
        }

        public void CameraUpdate()
        {
        }
    }

    public class ResultCamera : IAllPlayerCamera
    {
        private IMainCameraProvider mainCameraProvider;
        private AllCameraController allCameraController;

        private CinemachineTrackedDolly trackedDolly;

        private Vector3 inputEulerRotation;
        public Vector3 InputEulerRotation => inputEulerRotation;

        private float rotSpeed = 0.25f;

        public void Setup(AllCameraController controller)
        {
            allCameraController = controller;
            mainCameraProvider = controller.MainCameraController;
            trackedDolly = mainCameraProvider.VirtualCameras[(int)CameraTag.Result].GetCinemachineComponent<CinemachineTrackedDolly>();
            allCameraController.SetFixedCamRotation(mainCameraProvider.TargetTransform.rotation);
        }

        public void CameraTransition(){}

        public void Start()
        {
            mainCameraProvider.VirtualCameras[(int)CameraTag.Result].Priority = 10;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Free].Priority = 1;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Target].Priority = 1;
            mainCameraProvider.VirtualCameras[(int)CameraTag.Option].Priority = 1;

            trackedDolly.m_PathPosition = 0;

            mainCameraProvider.VirtualCameras[(int)CameraTag.Result].LookAt = mainCameraProvider.GetResultCameraLookAtTransform();
        }

        public void Exit()
        {
            inputEulerRotation = allCameraController.FixedCamRotation.eulerAngles;
        }

        public void CameraUpdate()
        {
            trackedDolly.m_PathPosition += rotSpeed * Time.deltaTime;
        }
    }
}
