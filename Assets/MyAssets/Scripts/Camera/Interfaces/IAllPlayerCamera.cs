

namespace MyAssets
{
    /*
     * 複数あるカメラに実装するインターフェース
     * 複数あるカメラをまとめて処理するために実装する
     */
    public interface IAllPlayerCamera
    {
        void Setup(AllCameraController controller);

        void Start();
        void CameraTransition();
        void CameraUpdate();
        void Exit();
    }
}
