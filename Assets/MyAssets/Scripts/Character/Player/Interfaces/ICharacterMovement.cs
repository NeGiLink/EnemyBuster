using UnityEngine;

namespace MyAssets
{

    public interface ICharacterMovement
    {
        void Move(float maxSpeed);
        void ForwardMove(float maxSpeed);

        void ForwardLerpMove(Vector3 basePos,float dis);

        void StartClimbStep(Vector3 hitPoint);

        void MoveTo(Vector3 targetPoint, float targetMoveSpeed, float moveSpeedChangeRate, float rotationSpeed, float time);
    }
}
