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

        [SerializeField]
        private float fallCount = 0;

        public bool IsFalling => fallCount >= 0.1f;

        [SerializeField]
        private PhysicMaterialData physicMaterialData;

        private Collider collider;

        public void Setup(CharacterBaseController controller)
        {
            collider = controller.gameObject.GetComponent<Collider>();
            if(collider != null)
            {
                collider.material = physicMaterialData.Data[(int)PhysicMaterialTag.Landing];
            }
        }

        public void DoUpdate()
        {
            CheckGroundStatus();

        }

        /// <summary>
        /// GroundCheck処理
        /// SphereCastを使用してtrue、falseの処理を行う
        /// </summary>
        /// <returns></returns>
        private void CheckGroundStatus()
        {
            bool land = false;
            land = Physics.SphereCast(transform.position + groundCheckOffsetY * Vector3.up,
                groundCheckRadius, Vector3.down, out hit, groundCheckDistance, groundLayers,
                QueryTriggerInteraction.Ignore);
            if(land != landing)
            {
                if (landing)
                {
                    fallCount = 0;
                    collider.material = physicMaterialData.Data[(int)PhysicMaterialTag.NoLanding];
                }
                else
                {
                    collider.material = physicMaterialData.Data[(int)PhysicMaterialTag.Landing];
                }
                landing = land;
            }
        }

        public void FallTimeUpdate()
        {
            fallCount += Time.deltaTime;
        }
    }
}
