using UnityEngine;

namespace MyAssets
{
    public class ChangeCameraType : MonoBehaviour
    {
        [SerializeField]
        private CameraTag cameraTag;

        public CameraTag CameraTag => cameraTag;
    }
}
