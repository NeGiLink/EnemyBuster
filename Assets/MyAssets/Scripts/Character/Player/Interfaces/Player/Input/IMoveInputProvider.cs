using UnityEngine;

namespace MyAssets
{
    /*
     * �ړ��֘A�̃C���^�[�t�F�[�X
     */
    public interface IMoveInputProvider
    {
        //�����Ă��邩�̃t���O
        bool    IsMove { get; }
        //�O�㍶�E�̈ړ�����
        Vector2 Move { get; }
        //���E�̈ړ�����
        float   Horizontal { get; }
        void    SetHorizontal(float horizontalRatio);
        //�O��̈ړ�����
        float   Vertical { get; }
        void    SetVertical(float verticalRatio);
        //�����̃t���O
        float   Dash {  get; }
    }
}
