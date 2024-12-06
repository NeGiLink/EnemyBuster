using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public interface IMainCameraProvider
    {
        List<CinemachineVirtualCamera> VirtualCameras {  get; }

        GameObject MainCamera { get; }
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

        [SerializeField]
        private Transform cameraUseTransform;

        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private PlayerUsesCamera playerUsesCamera;

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
                cameraUseTransform = player.transform;
            }
            else
            {
                Debug.LogError("cameraUseTransform‚ªŽæ“¾o—ˆ‚Ü‚¹‚ñ‚Å‚µ‚½B");
            }

            playerUsesCamera.Setup(player,this);
        }
        // Start is called before the first frame update
        void Start()
        {
            foreach(var cam in virtualCameras)
            {
                virtualCameraPrioritys.Add(cam.Priority);
            }

            playerUsesCamera.DoStart();
        }

        // Update is called once per frame
        void Update()
        {
            playerUsesCamera.DoUpdate();
        }
    }

}
