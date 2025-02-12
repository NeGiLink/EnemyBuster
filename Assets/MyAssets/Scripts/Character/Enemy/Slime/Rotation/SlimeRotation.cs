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
            // ‘ÎÛ‚Ö‚Ì•ûŒü‚ğŒvZ
            Vector3 direction = target.position - thisTransform.position;

            // Œ»İ‚Ì‰ñ“]‚©‚ç–Ú•W‚Ì‰ñ“]‚ğŒvZ
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Œ»İ‚Ì‰ñ“]‚©‚ç–Ú•W‚Ì‰ñ“]‚ÖƒXƒ€[ƒY‚É•âŠÔ
            thisTransform.rotation = Quaternion.Slerp(
                thisTransform.rotation, // Œ»İ‚Ì‰ñ“]
                targetRotation,     // –Ú•W‚Ì‰ñ“]
                Time.deltaTime * rotationSpeed // ‰ñ“]‘¬“x‚ğ’²®
            );
        }

        public void DoFreeMode(){}
    }
}
