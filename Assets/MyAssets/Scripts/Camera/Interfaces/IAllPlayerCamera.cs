

namespace MyAssets
{
    /*
     * ��������J�����Ɏ�������C���^�[�t�F�[�X
     * ��������J�������܂Ƃ߂ď������邽�߂Ɏ�������
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
