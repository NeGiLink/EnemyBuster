using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// �n�ʂɂ���L�����N�^�[��n�ʂɂ������ʒu�ɐݒ肷�邾���̃N���X
    /// </summary>
    public class AppearPoint : MonoBehaviour
    {

        [SerializeField]
        private LayerMask layer;

        [SerializeField]
        private SlimeController controller;



        private void Update()
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.interpolation = RigidbodyInterpolation.None;
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, layer))
            {
                transform.position = hit.point;
            }
            Vector3 v = transform.position;
            //rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            Destroy(this);
        }
    }
}
