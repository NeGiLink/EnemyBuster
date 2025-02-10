using UnityEngine;

namespace MyAssets
{
    /*
     * ���x�֌W�̐錾���܂Ƃ߂��C���^�[�t�F�[�X
     */
    public interface IVelocityComponent
    {
        Vector2     CurrentMove {  get;}
        float       CurrentMoveSpeed {  get;}
        Vector3     CurrentVelocity { get; set; }
        Rigidbody   Rigidbody { get; set; }

        void DeathCollider();
    }
}
