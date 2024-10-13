using UnityEngine;

namespace MyAssets
{
    public interface IJumpInputProvider
    {
        bool Jump { get; }
        bool IsJumpPush {  get; }
    }
}

