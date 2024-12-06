using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// 地面にいるキャラクターを地面にあった位置に設定するだけのクラス
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
