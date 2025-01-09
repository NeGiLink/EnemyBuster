using UnityEngine;

namespace MyAssets
{
    public interface IVelocityComponent
    {
        Vector2 CurrentMove {  get;}
        float CurrentMoveSpeed {  get;}
        Vector3 CurrentVelocity { get; set; }
        Rigidbody Rigidbody { get; set; }

        void DeathCollider();
    }
}
