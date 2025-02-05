using UnityEngine;

namespace MyAssets
{
    public class FootstepSEHandler : MonoBehaviour
    {

        private SEHandler seHandler;
        [SerializeField]
        private LayerMask groundLayer;
        [SerializeField]
        private float raycastDistance = 0.2f;
        [SerializeField]
        private bool wasGrounded = false;

        private void Awake()
        {
            seHandler = GetComponentInParent<SEHandler>();
        }

        private void Update()
        {
            bool isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer);

            // íÖínÇµÇΩèuä‘Ç…çƒê∂
            if (isGrounded && !wasGrounded)
            {
                seHandler.OneShotPlayFootstepSound();
            }

            wasGrounded = isGrounded;
        }
    }
}
