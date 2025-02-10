using UnityEngine;

namespace MyAssets
{
    /*
     * 移動関連のインターフェース
     */
    public interface IMoveInputProvider
    {
        //動いているかのフラグ
        bool    IsMove { get; }
        //前後左右の移動入力
        Vector2 Move { get; }
        //左右の移動入力
        float   Horizontal { get; }
        void    SetHorizontal(float horizontalRatio);
        //前後の移動入力
        float   Vertical { get; }
        void    SetVertical(float verticalRatio);
        //加速のフラグ
        float   Dash {  get; }
    }
}
