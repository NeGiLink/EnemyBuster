
namespace MyAssets
{
    /*
     * �n�ʒ��n����N���X�̃C���^�[�t�F�[�X
     */
    public interface IGroundCheck
    {
        bool Landing {  get;}

        void SetLanding(bool b);

        void FallTimeUpdate();

        bool IsFalling { get;}

        float FallCount {  get;}
    }
}
