using UnityEngine;

namespace MyAssets
{
    /*
     * �L�����N�^�[���n�ʂɒ��n�������𒲂ׂĒl���擾����N���X
     */
    [System.Serializable]
    public class GroundCheck : IGroundCheck
    {
        //Transform���擾
        private Transform           transform;
        //SphereCast�̔��a
        [SerializeField]
        private float               groundCheckRadius = 0.4f;
        //SphereCast��transform�Ɂ{����l
        [SerializeField]
        private float               groundCheckOffsetY = 1.0f;
        //SphereCast�̋���
        [SerializeField]
        private float               groundCheckDistance = 0.2f;
        //�n�ʂ̃��C���[
        [SerializeField]
        private LayerMask           groundLayers = 0;

        //���n����
        [SerializeField]
        private bool                landing;

        public bool                 Landing => landing;

        //�������̎��Ԃ�ێ�����ϐ�
        [SerializeField]
        private float               fallCount = 0;
        public float                FallCount => fallCount;

        public bool                 IsFalling => fallCount >= 0.1f;
        //PhysicMaterial�̃X�N���v�^�u���I�u�W�F�N�g
        [SerializeField]
        private PhysicMaterialData  physicMaterialData;
        //������s���R���C�_�[
        private Collider            collider;
        
        private IVelocityComponent  velocity;

        public void SetTransform(Transform _transform) { transform = _transform; }

        public void SetLanding(bool _landing) { landing = _landing; }
        public void Setup(CharacterBaseController controller)
        {
            collider = controller.gameObject.GetComponent<Collider>();
            if(collider != null)
            {
                collider.material = physicMaterialData.Data[(int)PhysicMaterialTag.Landing];
            }
            velocity = controller.Velocity;
        }

        public void DoUpdate()
        {
            CheckGroundStatus();

        }

        /// <summary>
        /// GroundCheck����
        /// SphereCast���g�p����true�Afalse�̏������s��
        /// </summary>
        /// <returns></returns>
        private void CheckGroundStatus()
        {
            if(collider == null) { return; }
            if (!collider.enabled) { return; }
            //�������������擾
            RaycastHit hit;
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
            if(velocity.Rigidbody.velocity.y > 0) { return; }
            fallCount += Time.deltaTime;
        }
    }
}
