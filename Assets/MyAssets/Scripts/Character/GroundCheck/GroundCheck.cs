using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class GroundCheck : IGroundCheck
    {
        private Transform transform;
        public void SetTransform(Transform _transform) { transform = _transform; }

        [SerializeField]
        private float groundCheckRadius = 0.4f;
        [SerializeField]
        private float groundCheckOffsetY = 1.0f;
        [SerializeField]
        private float groundCheckDistance = 0.2f;
        [SerializeField]
        private LayerMask groundLayers = 0;

        private RaycastHit hit;
        [SerializeField]
        private bool landing;

        public bool Landing => landing;

        public void SetLanding(bool _landing) { landing = _landing; }
        /// <summary>
        /// GroundCheck処理
        /// SphereCastを使用してtrue、falseの処理を行う
        /// </summary>
        /// <returns></returns>
        public void CheckGroundStatus()
        {
            landing = Physics.SphereCast(transform.position + groundCheckOffsetY * Vector3.up,
                groundCheckRadius, Vector3.down, out hit, groundCheckDistance, groundLayers,
                QueryTriggerInteraction.Ignore);
        }
        /*
        public bool CheckGroundStatus()
        {
            return Physics.SphereCast(transform.position + groundCheckOffsetY * Vector3.up,
                groundCheckRadius, Vector3.down, out hit, groundCheckDistance, groundLayers,
                QueryTriggerInteraction.Ignore);
        }
         */
    }
}
