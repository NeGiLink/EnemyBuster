using UnityEngine;

namespace MyAssets
{
    public interface ISlimeRotation
    {
        void DoLookOnTarget(Transform target);
    }

    [System.Serializable]
    public class SlimeRotation : IRotation,ISlimeRotation, ICharacterComponent<ISlimeSetup>
    {
        [SerializeField]
        private Transform thisTransform;

        [SerializeField]
        private float rotationSpeed;

        public void DoSetup(ISlimeSetup slime)
        {
            thisTransform = slime.gameObject.transform;
        }

        public void DoUpdate(){}

        public void DoFixedUpdate(){}


        public void DoLookOnTarget(Transform target)
        {
            // �Ώۂւ̕������v�Z
            Vector3 direction = target.position - thisTransform.position;

            // ���݂̉�]����ڕW�̉�]���v�Z
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // ���݂̉�]����ڕW�̉�]�փX���[�Y�ɕ��
            thisTransform.rotation = Quaternion.Slerp(
                thisTransform.rotation, // ���݂̉�]
                targetRotation,     // �ڕW�̉�]
                Time.deltaTime * rotationSpeed // ��]���x�𒲐�
            );
        }

        public void DoFreeMode(){}
    }
}
