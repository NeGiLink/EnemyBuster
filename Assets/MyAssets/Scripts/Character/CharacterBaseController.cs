using UnityEngine;

namespace MyAssets
{
    public class CharacterBaseController : MonoBehaviour
    {
        [SerializeField]
        protected VelocityComponent velocity;

        public IVelocityComponent Velocity => velocity;

        [SerializeField]
        protected DamageContainer damageContainer;
        public IDamageContainer DamageContainer => damageContainer;

        [SerializeField]
        protected Damagement damagement;
        public IDamagement Damagement => damagement;

        [SerializeField]
        protected GroundCheck groundCheck;
        public IGroundCheck GroundCheck => groundCheck;

        protected virtual void Awake()
        {
        }

        protected virtual void Start()
        {
        }

        protected virtual void Update()
        {
        }

        protected virtual void FixedUpdate()
        { 
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
        }

        protected virtual void OnTriggerStay(Collider other)
        {
        }

        protected virtual void OnTriggerExit(Collider other)
        {   
        }
    }
}
