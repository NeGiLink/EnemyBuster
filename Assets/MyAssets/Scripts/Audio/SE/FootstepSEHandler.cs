using UnityEngine;

namespace MyAssets
{
    /*
     * キャラクターの足部分のリグにアタッチするクラス
     * 足音のSEを地面の種類別に再生する
     */
    public class FootstepSEHandler : MonoBehaviour
    {

        private SEHandler   seHandler;
        [SerializeField]
        private LayerMask   groundLayer;
        [SerializeField]
        private float       raycastDistance = 0.2f;
        [SerializeField]
        private bool        wasGrounded = false;

        private void Awake()
        {
            seHandler = GetComponentInParent<SEHandler>();
        }

        private void Update()
        {
            bool isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer);

            // 着地した瞬間に再生
            if (isGrounded && !wasGrounded)
            {
                seHandler.OneShotPlayFootstepSound();
            }

            wasGrounded = isGrounded;
        }
    }
}
