using UnityEngine;

namespace MyAssets
{
    public interface IJumpInputProvider
    {
        float Jump { get; }
        bool IsJumpPush {  get; }
    }
}

