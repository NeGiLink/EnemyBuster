

namespace MyAssets
{
    public interface IAllPlayerCamera
    {
        void Setup(AllCameraController controller);

        void Start();
        void CameraTransition();
        void CameraUpdate();
        void Exit();
    }
}
