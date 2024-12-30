using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface IMainCameraProvider
    {
        List<CinemachineVirtualCamera> VirtualCameras {  get; }

        GameObject MainCamera { get; }

        Transform TargetTransform { get; }
    }
    public class MainCameraController : MonoBehaviour, IMainCameraProvider
    {
        [SerializeField]
        private GameObject mainCamera;
        public GameObject MainCamera => mainCamera;

        [SerializeField]
        private List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();
        public List<CinemachineVirtualCamera> VirtualCameras => virtualCameras;

        private List<int> virtualCameraPrioritys = new List<int>();

        [SerializeField]
        private Transform targetTransform;
        public Transform TargetTransform => targetTransform;

        [SerializeField]
        private InputControllCamera inputControllCamera;

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


            if(player != null)
            {
                targetTransform = player.transform;
            }
            else
            {
                Debug.LogError("cameraUseTransform‚ªŽæ“¾o—ˆ‚Ü‚¹‚ñ‚Å‚µ‚½B");
            }

            inputControllCamera.Setup(player,this);
        }
        // Start is called before the first frame update
        void Start()
        {
            foreach(var cam in virtualCameras)
            {
                virtualCameraPrioritys.Add(cam.Priority);
            }

            inputControllCamera.DoStart();
        }

        // Update is called once per frame
        void Update()
        {
            inputControllCamera.DoUpdate();
        }
    }

}
