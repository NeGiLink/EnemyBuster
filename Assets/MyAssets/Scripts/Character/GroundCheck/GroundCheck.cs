using UnityEngine;

namespace MyAssets
{
    /*
     * キャラクターが地面に着地したかを調べて値を取得するクラス
     */
    [System.Serializable]
    public class GroundCheck : IGroundCheck
    {
        //Transformを取得
        private Transform           transform;
        //SphereCastの半径
        [SerializeField]
        private float               groundCheckRadius = 0.4f;
        //SphereCastをtransformに＋する値
        [SerializeField]
        private float               groundCheckOffsetY = 1.0f;
        //SphereCastの距離
        [SerializeField]
        private float               groundCheckDistance = 0.2f;
        //地面のレイヤー
        [SerializeField]
        private LayerMask           groundLayers = 0;

        //着地判定
        [SerializeField]
        private bool                landing;

        public bool                 Landing => landing;

        //落下中の時間を保持する変数
        [SerializeField]
        private float               fallCount = 0;
        public float                FallCount => fallCount;

        public bool                 IsFalling => fallCount >= 0.1f;
        //PhysicMaterialのスクリプタブルオブジェクト
        [SerializeField]
        private PhysicMaterialData  physicMaterialData;
        //判定を行うコライダー
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
        /// GroundCheck処理
        /// SphereCastを使用してtrue、falseの処理を行う
        /// </summary>
        /// <returns></returns>
        private void CheckGroundStatus()
        {
            if(collider == null) { return; }
            if (!collider.enabled) { return; }
            //当たった情報を取得
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
