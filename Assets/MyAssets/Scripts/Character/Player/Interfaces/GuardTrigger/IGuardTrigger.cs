using UnityEngine;

namespace MyAssets
{
    public interface IGuardTrigger
    {
        bool IsGuard { get; }

        void SetGuardFlag(bool guard);
    }
}

