using MyAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class SlimeRotation : IRotation, ICharacterComponent<ISlimeSetup>
    {
        [SerializeField]
        private Transform thisTransform;

        private Quaternion targetRotation;

        private FieldOfView fieldOfView;

        private ISlimeAnimator animator;

        // �ǉ�: �؂�ւ��O�̉�]��ێ�����ϐ�
        [SerializeField]
        private Quaternion previousCameraRotation;

        public void DoSetup(ISlimeSetup slime)
        {
            thisTransform = slime.gameObject.transform;
        }

        public void DoUpdate()
        {
            
        }

        public void DoFixedUpdate()
        {
            
        }


        public void DoLookOnTarget(Vector3 dir)
        {

        }

        public void DoFreeMode()
        {

        }
    }
}
