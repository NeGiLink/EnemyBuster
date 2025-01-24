using UnityEngine;

namespace MyAssets
{

    public interface IMovement
    {
        void Stop();
        void Move(float maxSpeed);

        void RollingMove(float maxSpeed);
        void ForwardMove(float maxSpeed);

        void ForwardLerpMove(Vector3 basePos,float dis);

        void StartClimbStep(Vector3 hitPoint);

        void MoveTo(Vector3 targetPoint, float targetMoveSpeed, float moveSpeedChangeRate, float rotationSpeed, float time);

        void SideMove(float dir, float speed, Vector3 targetPoint, float rotationSpeed, float time);

        void DecreaseMove(float ratio);
    }
}
