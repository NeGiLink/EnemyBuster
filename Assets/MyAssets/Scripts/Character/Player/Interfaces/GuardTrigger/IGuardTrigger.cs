namespace MyAssets
{
    /*
     * �h��̔�����܂Ƃ߂��N���X�̃C���^�[�t�F�[�X
     */
    public interface IGuardTrigger
    {
        bool IsGuard { get; }

        void SetGuardFlag(bool guard);
    }
}

