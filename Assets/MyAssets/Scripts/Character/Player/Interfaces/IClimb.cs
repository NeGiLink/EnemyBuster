
namespace MyAssets
{
    /*
     * �o�蓮�쏈���N���X�̃C���^�[�t�F�[�X
     */
    public interface IClimb
    {
        void DoClimbStart();
        void DoClimb();

        void DoClimbExit();

        bool IsClimbEnd {  get; }
    }
}

