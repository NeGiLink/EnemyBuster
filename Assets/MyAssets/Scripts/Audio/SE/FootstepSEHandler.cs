using UnityEngine;

namespace MyAssets
{
    /*
     * �L�����N�^�[�̑������̃��O�ɃA�^�b�`����N���X
     * ������SE��n�ʂ̎�ޕʂɍĐ�����
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

            // ���n�����u�ԂɍĐ�
            if (isGrounded && !wasGrounded)
            {
                seHandler.OneShotPlayFootstepSound();
            }

            wasGrounded = isGrounded;
        }
    }
}
