using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class Damagement : IDamagement,ICharacterComponent
    {
        private Transform thisTransform;

        private IVelocityComponent velocity;


        public void DoSetup(ICharacterSetup chara)
        {
            thisTransform = chara.gameObject.transform;
            velocity = chara.Velocity;
        }

        public void AddForceMove(Vector3 origin, Vector3 target, float power)
        {
            Vector3 sub = origin - target;
            velocity.Rigidbody.AddForce(sub * power, ForceMode.VelocityChange);
        }
    }
}
