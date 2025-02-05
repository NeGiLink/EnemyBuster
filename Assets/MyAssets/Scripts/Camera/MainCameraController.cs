using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface IMainCameraProvider
    {
        List<CinemachineVirtualCamera>  VirtualCameras {  get; }

        GameObject                      MainCamera { get; }

        Transform                       TargetTransform { get; }
        ChangeCameraType ChangeCameraType { get; }
    }
    [RequireComponent(typeof(ChangeCameraType))]
    public class MainCameraController : MonoBehaviour, IMainCameraProvider
    {
        //カメラ本体
        [SerializeField]
        private GameObject                      mainCamera;
        public GameObject                       MainCamera => mainCamera;
        //全バーチャルカメラのリスト
        [SerializeField]
        private List<CinemachineVirtualCamera>  virtualCameras = new List<CinemachineVirtualCamera>();
        public List<CinemachineVirtualCamera>   VirtualCameras => virtualCameras;

        private List<int>                       virtualCameraPrioritys = new List<int>();
        //カメラが注目するターゲット
        [SerializeField]
        private Transform                       targetTransform;
        public Transform                        TargetTransform => targetTransform;
        //複数あるカメラを切り替えて処理するクラス
        [SerializeField]
        private AllCameraController             inputControllCamera;


        private ChangeCameraType changeCameraType;
        public ChangeCameraType ChangeCameraType => changeCameraType;

        public void ActivateAllCamera(bool a)
        {
            if (a)
            {
                for(int i = 0; i < virtualCameras.Count; i++)
                {
                    virtualCameras[i].Priority = virtualCameraPrioritys[i];
                }
            }
            else
            {
                foreach(var cam in virtualCameras)
                {
                    cam.Priority = 0;
                }
            }
        }


        private void Awake()
        {
            Camera camera = Camera.main;
            if(camera != null)
            {
                mainCamera = camera.gameObject;
            }


            GameObject player = GameObject.FindGameObjectWithTag("Player");

            changeCameraType = GetComponent<ChangeCameraType>();

            if(player != null)
            {
                targetTransform = player.transform;
            }
            else
            {
                Debug.LogError("cameraUseTransformが取得出来ませんでした。");
            }

            inputControllCamera.Setup(player,this);
        }

        private void Start()
        {
            foreach(var cam in virtualCameras)
            {
                virtualCameraPrioritys.Add(cam.Priority);
            }

            inputControllCamera.DoStart();

            StartCoroutine(CameraUpdate());
        }

        private IEnumerator CameraUpdate()
        {
            while (true)
            {
                yield return null;

                inputControllCamera.DoUpdate();

            }
        }
    }

}
