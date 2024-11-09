using UnityEngine;
using UnityEngine.Assertions;

namespace MyAssets
{
    [System.Serializable]
    public class VelocityComponent : IVelocityComponent,ICharacterComponent
    {
        [SerializeField]
        private Rigidbody thisRigidbody;
        [SerializeField]
        private Vector3 currentVelocity = Vector3.zero;
        private bool needUpdateVelocity = true;

        [SerializeField]
        private bool inheritRigidbodyVelocity = true;
        public Vector3 CurrentVelocity
        {
            get
            {
                if (needUpdateVelocity && inheritRigidbodyVelocity)
                {
                    currentVelocity = thisRigidbody.velocity;
                    needUpdateVelocity = false;
                }
                return currentVelocity;
            }
            set
            {
                needUpdateVelocity = false;
                currentVelocity = value;
            }
        }
        public Vector2 CurrentMove => new Vector2(CurrentVelocity.x, CurrentVelocity.z);
        public float CurrentMoveSpeed => CurrentMove.magnitude;

        public void DoSetup(ICharacterSetup chara)
        {
            thisRigidbody = chara.gameObject.GetComponent<Rigidbody>();
            Assert.IsNotNull(thisRigidbody);
        }
        /*
        public void RefreshVelocity()
        {
            if (needUpdateVelocity && inheritRigidbodyVelocity)
            {
                currentVelocity = thisRigidbody.velocity;
            }
            thisRigidbody.velocity = currentVelocity;
            needUpdateVelocity = true;
        }
        */

        public Rigidbody Rigidbody { get { return thisRigidbody; }set { thisRigidbody = value; } }
    }
}
