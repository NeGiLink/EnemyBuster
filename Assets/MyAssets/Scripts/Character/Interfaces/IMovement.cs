using UnityEngine;

namespace MyAssets
{
    public interface IMovement
    {
        void Move(float maxSpeed);

        void ForwardMove(Vector3 basePos,float dis);

        void StartClimbStep(Vector3 hitPoint);
    }
}
