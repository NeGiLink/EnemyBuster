using UnityEngine;

namespace MyAssets
{
    public enum SurfaceType
    {
        Soil,
        Rock
    }
    public class FootstepSurface : MonoBehaviour
    {
        [SerializeField]
        private SurfaceType surfaceType = SurfaceType.Soil;
        public SurfaceType SurfaceType => surfaceType;
    }
}
