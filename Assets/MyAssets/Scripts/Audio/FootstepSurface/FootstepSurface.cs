using UnityEngine;

namespace MyAssets
{
    public enum SurfaceType
    {
        Soil,
        Rock
    }
    /*
     * Ground�I�u�W�F�N�g�ɃA�^�b�`����N���X
     * enum�Œn�ʂ̎�ނ𔻕ʂ���
     */
    public class FootstepSurface : MonoBehaviour
    {
        [SerializeField]
        private SurfaceType surfaceType = SurfaceType.Soil;
        public SurfaceType SurfaceType => surfaceType;
    }
}
