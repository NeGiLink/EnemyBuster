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

            //var list = GetComponentsInChildren<CinemachineVirtualCamera>();

            //virtualCameras = list;


            if(player != null)
            {
                cameraUseTransform = player.transform;
            }
            else
            {
                Debug.LogError("cameraUseTransformÇ™éÊìæèoóàÇ‹ÇπÇÒÇ≈ÇµÇΩÅB");
            }

            playerUsesCamera.Setup(player,this);
        }
        // Start is called before the first frame update
        void Start()
        {
            playerUsesCamera.DoStart();
        }

        // Update is called once per frame
        void Update()
        {
            playerUsesCamera.DoUpdate();
        }
    }

}
