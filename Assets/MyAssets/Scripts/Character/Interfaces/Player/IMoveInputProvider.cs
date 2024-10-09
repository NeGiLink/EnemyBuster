using UnityEngine;

namespace MyAssets
{
    public interface IMoveInputProvider
    {
        bool IsMove { get; }
        Vector2 Move { get; }
        float Horizontal { get; }
        float Vertical { get; }
        float Dash {  get; }
    }
}
