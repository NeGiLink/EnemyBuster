using UnityEngine;

namespace MyAssets
{
    public enum SurfaceType
    {
        Soil,
        Rock
    }
    /*
     * Groundオブジェクトにアタッチするクラス
     * enumで地面の種類を判別する
     */
    public class FootstepSurface : MonoBehaviour
    {
        [SerializeField]
        private SurfaceType surfaceType = SurfaceType.Soil;
        public SurfaceType SurfaceType => surfaceType;
    }
}
