using UnityEngine;

namespace MyAssets
{
    public interface IMoveInputProvider
    {
        bool IsMove { get; }
        Vector2 Move { get; }
        float Horizontal { get; }
        void SetHorizontal(float horizontalRatio);
        float Vertical { get; }
        void SetVertical(float verticalRatio);
        float Dash {  get; }
    }
}
