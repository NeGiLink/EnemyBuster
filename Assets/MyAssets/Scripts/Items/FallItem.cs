using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MyAssets
{
    public class FallItem : MonoBehaviour
    {
        [SerializeField]
        private LayerMask layerMask;
        [SerializeField]
        private float raycastDistance = 1.0f;
        [SerializeField]
        private bool wasGrounded = false;
        [SerializeField]
        private float gravityMultiply;

        private new Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            bool isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastDistance, layerMask);

            // íÖínÇµÇΩèuä‘Ç…çƒê∂
            if (isGrounded&& wasGrounded)
            {
                rigidbody.useGravity = false;
            }
            else if(!isGrounded && !wasGrounded)
            {
                rigidbody.velocity += Physics.gravity * gravityMultiply * Time.deltaTime;
            }

            wasGrounded = isGrounded;
        }
    }
}
