using UnityEngine;

namespace MyAssets
{
    public interface IMovement
    {
        void Move(float maxSpeed);

        void ForwardMove(float maxSpeed);

        void StartClimbStep(Vector3 hitPoint);
    }
}
