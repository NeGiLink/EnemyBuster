using UnityEngine;
using UnityEngine.Assertions;

namespace MyAssets
{
    /*
     * キャラクターの速度、Rigidbody関係の処理を行うクラス
     */
    [System.Serializable]
    public class VelocityComponent : IVelocityComponent,ICharacterComponent<ICharacterSetup>
    {
        [SerializeField]
        private Rigidbody       thisRigidbody;
        [SerializeField]
        private Collider        collider;
        [SerializeField]
        private Vector3         currentVelocity = Vector3.zero;
        private bool            needUpdateVelocity = true;

        [SerializeField]
        private bool            inheritRigidbodyVelocity = true;
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
        public Vector2          CurrentMove => new Vector2(CurrentVelocity.x, CurrentVelocity.z);
        public float            CurrentMoveSpeed => CurrentMove.magnitude;

        public void DoSetup(ICharacterSetup chara)
        {
            thisRigidbody = chara.gameObject.GetComponent<Rigidbody>();
            collider = chara.gameObject.GetComponent<Collider>();
            Assert.IsNotNull(thisRigidbody);
        }

        public void DeathCollider()
        {
            thisRigidbody.useGravity = false;
            collider.isTrigger = true;
        }

        public Rigidbody        Rigidbody { get { return thisRigidbody; }set { thisRigidbody = value; } }
        public Collider         Collider { get { return collider; } set { collider = value; } }
    }
}
